using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using UnityEngine;
using MySql.Data.MySqlClient;
using TMPro;
//using UnityEditor.MemoryProfiler;
using UnityEngine.UI;


public static class DataBaseManager 
{

    public static string UserName;

    public static string UserId;

    public static string Role;
    
    public static string UserGroupNameToEdit;

    public static string SkillGroupNameToEdit;
    
    public static bool LoggedIn { get {return UserName != null;} }

    public static void LogOut()
    {
        UserName = null;
        UserId = null;
        Role = null;
        UserGroupNameToEdit = null;
        SkillGroupNameToEdit = null;
    }

    public static string ChosenUserGroup;

    public static string ChosenSkillGroup;

    public static string ChosenSkillSkillGroup;




    /*********************************************************** tha old version of the DataBase with the online server ******************************************************/
    
    /* [Header("MANAGERS")]
    [Space(30)]
    public static LoginManager LoginManager;*/
    // information needed for the connection with the DataBase
    //[Header("THE CONNECTION TO THE DATABASE")]

    /*public string Host; 
    public string DataBase;
    public string Username;
    public string Password;
    public Text State;
    private MySqlConnection connection;*/
    
    /*
     
    public struct User
    {
        public int ID;
        public string FirstName;
        public string LastName;
        public string Mail;
        public int Phone;
        public string Password;
        public Type Type;
    }

    public enum Type
    {
        Student,
        Supervisor,
        Admin,
    }
    */
    
    //User ConnectedUser;

    //constructor 
    //public DataBaseManager(){}

    /*public void ConnectDB()
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
     }*/

    /*void OnApplicationQuit()
    {
        Debug.Log("Shutdown connection");
        if (connection != null && connection.State.ToString() != "Closed")
            connection.Close();
    }*/

    /*public void Register(string firstName, string lastName, string mail, string phone, string password)
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
        }
        
        
        //the registration of the the user
        if (!exist)
        {
            String commandInsert =
                "INSERT INTO `user`(`firstName`, `lastName`, `mail`, `phone`, `password`, `type`) VALUES ('"+firstName+"','"+lastName+"','"+mail+"','"+phone+"','"+password+"','"+Type.Student+"')";
        
            MySqlCommand cmdInsert = new MySqlCommand(commandInsert, connection);
            try
            {
                cmdInsert.ExecuteReader();
                State.text = "Register successful";
                LoginManager.SignInActivate();
            }
            catch (IOException e)
            {
                State.text = e.ToString();
            }
            cmdInsert.Dispose();
        }
        connection.Close();*/



    /*public void SignIn(string name, string password) 
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
                pass = myRider["hash"].ToString();
                if (pass == password)
                {
                    ConnectedUser.ID = (int) myRider["id"];
                    ConnectedUser.Phone = (int) myRider["phone"];
                    ConnectedUser.FirstName = myRider["firstName"].ToString();
                    ConnectedUser.LastName = myRider["lastName"].ToString();
                    ConnectedUser.Mail = myRider["mail"].ToString();
                    ConnectedUser.Password = myRider["hash"].ToString();
                    ConnectedUser.Type =  (Type) Enum.Parse(typeof(Type), myRider["type"].ToString());

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
    }*/

    /*public List<User> RegexSearch(String regex, string tableName, string fieldName)
    {
        List<User> results = new List<User>();
        ConnectDB();
        try {
        
            if (regex.Length >= 1) {
                //String sqlRequest = "SELECT * FROM user WHERE firstName REGEXP '^" + regex + "'";
                String sqlRequest = "SELECT * FROM "+ tableName +" WHERE "+ fieldName +" REGEXP '^" + regex + "'";
                
                String commandSelect = sqlRequest;
                MySqlCommand cmdSelect = new MySqlCommand(commandSelect, connection);
                MySqlDataReader myRider = cmdSelect.ExecuteReader();

                while (myRider.Read())
                {
                    if (tableName == "user")
                    {
                        
                    }else if (tableName == "skill")
                    {
                        
                    }else if (tableName == "userGroup")
                    {
                        
                    }else if (tableName == "skillGroup")
                    {
                        
                    }
                    /*
                    User u;
                    u.FirstName = myRider["firstName"].ToString();
                    u.LastName = myRider["lastName"].ToString();
                    u.ID =  (int) myRider["id"];
                    u.Phone = (int) myRider["Phone"];
                    u.Mail = myRider["mail"].ToString();
                    u.Password = myRider["password"].ToString();
                    u.Type = (Type) Enum.Parse(typeof(Type), myRider["type"].ToString());

                    results.Add(u);
                    
                }
                cmdSelect.Dispose();
                myRider.Close();
            }
        }
        catch (IOException e)
        {
            State.text = e.ToString();
        }

        connection.Close();

        return results;
    }*/

    /*public List<Tuple<int, string, string>> GetUsers()
    {
        var res = new List <Tuple<int, string, string>>();
        ConnectDB();
        try
        {
            String sqlRequest = "SELECT * FROM user WHERE type = '" + Type.Supervisor + "'";
            String commandSelect = sqlRequest;
            MySqlCommand cmdSelect = new MySqlCommand(commandSelect, connection);
            MySqlDataReader myRider = cmdSelect.ExecuteReader();

            while (myRider.Read())
            {
                var sup = new Tuple<int, string, string>((int) myRider["id"], myRider["firstName"].ToString(), myRider["LastName"].ToString());
                res.Add(sup);
            }
            cmdSelect.Dispose();
            myRider.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        connection.Close();
        
        return res;
    }*/

    /*public void CreateSkillGroup(int idSupervisor, string tableName)
    {
        String commandInsert = "";
        MySqlCommand cmdInsert = new MySqlCommand(commandInsert, connection);
        
        try
        {
            cmdInsert.ExecuteReader();
            State.text = "Register successful";
            LoginManager.SignInActivate();
        }
        catch (IOException e)
        {
            State.text = e.ToString();
        }
        cmdInsert.Dispose();
        connection.Close();
    }*/
}
