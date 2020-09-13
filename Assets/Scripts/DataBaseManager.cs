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
    
    // Start is called before the first frame update


    void ConnectDB()
    {
        string con = "Server=" + Host +
                     ";DATABASE=" + DataBase + 
                     ";User ID=" + Username + 
                     ";Password=" + Password + 
                     ";Pooling=true;Charset=utf8;";
        try {
            connection = new MySqlConnection(con);
            connection.Open();
            State.text = connection.State.ToString();
            Debug.Log("try connection ");
        }catch (IOException e) {
            State.text = e.ToString();
            Debug.Log("catch connection ");
        }
    }

    void Update()
    {
        
    }

    void OnApplicationQuit()
    {
        Debug.Log("Shutdown connection");
        if (connection != null && connection.State.ToString() != "Closed")
        {
            connection.Close();
        }
            
        
    }
    
    

    public void Register(TMP_InputField firstName, TMP_InputField lastName, TMP_InputField mail,
                         TMP_InputField phone, TMP_InputField password)
    {
        ConnectDB();
        
        Debug.Log("yes ");
        String command = "INSERT INTO user VALUES(default','"+ firstName.text +"','" + lastName.text+ 
                         "','" + mail.text +"','"+ phone.text +"','" +password.text +")";
        
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
