using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using MySql.Data.MySqlClient;
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
    void Start()
    {
        string con = "Server=" + Host +
                        ";DATABASE=" + DataBase +
                        ";User ID=" + Username +
                        ";Password=" + Password +
                        ";Pooling=true;Charset=utf8;";
        try {
            connection = new MySqlConnection(con);
            connection.Open();
            
        }
        catch (Exception e) {
            State.text = e.ToString();
        }
    }

    void Update()
    {
        State.text = connection.State.ToString();
    }
}
