using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;


[Serializable]
public class LoginManager : MonoBehaviour
{
    
    
    
    //information needed to sign in     
    [SerializeField] private TMP_InputField signInMail;
    [SerializeField] private TMP_InputField signInPassword;
    
    //information needed to sign up
    [SerializeField] private TMP_InputField firstName;
    [SerializeField] private TMP_InputField lastName;
    [SerializeField] private TMP_InputField phoneNumber;
    [SerializeField] private TMP_InputField signUpMail;
    [SerializeField] private TMP_InputField signUpConfermationMail;
    [SerializeField] private TMP_InputField signUpPassword;
    [SerializeField] private TMP_InputField signUpConfermationPassword;
    

    private GameObject _signUpPanel;
    void Awake()
    {
        //_signUpPanel.SetActive(false);
    }

    public void InformationToVerifyToSignIn()
    {
       /* Debug.Log(mail.text);
        Debug.Log(password.text);*/
       if (signInMail.text == "admin" && signInPassword.text == "admin")
       {
           Debug.Log("yes");
       }
       else
       {
        Debug.Log("Vous ne passerez pas !");
       }
    }

    public void SignUpActivate()
    {
        _signUpPanel.SetActive(true);
    }
    
    public void InformationToVerifyToSignUp()
    {
        /*Debug.Log(mail.text);
        Debug.Log(password.text);*/
    }
}
