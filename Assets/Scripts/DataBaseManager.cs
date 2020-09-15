using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using UnityEngine;
using MySql.Data.MySqlClient;
using TMPro;
using UnityEngine.UI;


public class DataBaseManager : MonoBehaviour
{
    // information needed for the connection with the DataBase
    public string Host; 
    public string DataBase;
    public string Username;
    public string Password;
    public Text State;
    private MySqlConnection connection;

    struct User
    {
        public int ID;
        public string FirstName;
        public string LastName;
        public string Mail;
        public int Phone;
        public string Password;
    }
    User ConnectedUser;

    public void ConnectDB()
    {
        string con = "Server=" + Host +
                     ";DATABASE=" + DataBase + 
                     ";User ID=" + Username + 
                     ";Password=" + Password + 
                     ";Pooling=true;Charset=utf8;";
        try
        {
            connection = new MySqlConnection(con);
            connection.Open();
            State.text = connection.State.ToString();
        }catch (IOException e)
        {
            State.text = e.ToString();
        }
    }

    void OnApplicationQuit()
    {
        Debug.Log("Shutdown connection");
        if (connection != null && connection.State.ToString() != "Closed")
            connection.Close();
    }
    
    public void Register(string firstName, string lastName, string mail, string phone, string password)
    {
        bool exist = false;
        
        //connect to the DataBase
        ConnectDB();
        
        // verify if the mail is already used
        try
        {
            String commandSelect = "SELECT mail FROM user WHERE mail = '" + mail + "'";
            MySqlCommand cmdSelect = new MySqlCommand(commandSelect, connection);
            MySqlDataReader myRider = cmdSelect.ExecuteReader();
        
            while(myRider.Read())
                if (myRider["mail"].ToString() != "")
                {
                    State.text = "This mail is already used";
                    exist = true;
                }
            cmdSelect.Dispose();
            myRider.Close();
        }
        catch (IOException e)
        {
            State.text = e.ToString();
            throw;
        }
        
        
        //the registration of the the user
        if (!exist)
        {
            String commandInsert =
                "INSERT INTO `user`(`firstName`, `lastName`, `mail`, `phone`, `password`) VALUES ('"+firstName+"','"+lastName+"','"+mail+"','"+phone+"','"+password+"')";
        
            MySqlCommand cmdInsert = new MySqlCommand(commandInsert, connection);
            try
            {
                cmdInsert.ExecuteReader();
                State.text = "Register successful";
            }
            catch (IOException e)
            {
                State.text = cmdInsert.ToString();
            }
            cmdInsert.Dispose();
        }
        connection.Close();
    }

    public void SignIn(string mail, string password) //TODO not done yet
    {
        ConnectDB();
        string pass = null;

        try
        {
            String commandSelect = "SELECT * FROM user WHERE mail = '" + mail + "'";;
            MySqlCommand cmdSelect = new MySqlCommand(commandSelect, connection);
            MySqlDataReader myRider = cmdSelect.ExecuteReader();

            while (myRider.Read())
            {
                pass = myRider["password"].ToString();
                if (pass == password)
                {
                    ConnectedUser.ID = (int) myRider["id"];
                    ConnectedUser.Phone = (int) myRider["phone"];
                    ConnectedUser.FirstName = myRider["firstName"].ToString();
                    ConnectedUser.LastName = myRider["lastName"].ToString();
                    ConnectedUser.Mail = myRider["mail"].ToString();
                    ConnectedUser.Password = myRider["password"].ToString();

                    State.text = "Welcome " + ConnectedUser.FirstName +" "+ ConnectedUser.LastName + " !";
                }
                else 
                    State.text = "invalid information";
            }
            if (pass == null)
                State.text = "the account doesn't exist";
                
            cmdSelect.Dispose();
            myRider.Close();
        }
        catch (IOException e)
        {
            State.text = e.ToString();
        }
        connection.Close();
    }
}
