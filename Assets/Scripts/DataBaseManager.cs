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
        {
            connection.Close();
        }
    }
    
    public void Register(string firstName, string lastName, string mail, string phone, string password)
    {
        ConnectDB();
        String command =
            "INSERT INTO `user`(`firstName`, `lastName`, `mail`, `phone`, `password`) VALUES ('"+firstName+"','"+lastName+"','"+mail+"','"+phone+"','"+password+"')";
        
        MySqlCommand cmd = new MySqlCommand(command, connection);
        try
        {
            cmd.ExecuteReader();
            State.text = "Register successful";
        }
        catch (IOException e)
        {
            State.text = cmd.ToString();
        }
        cmd.Dispose();
        connection.Close();
    }
}
