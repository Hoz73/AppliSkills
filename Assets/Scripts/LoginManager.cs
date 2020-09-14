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
    [SerializeField] private TMP_InputField signUpMail;
    [SerializeField] private TMP_InputField signUpConfermationMail;
    [SerializeField] private TMP_InputField phoneNumber;
    [SerializeField] private TMP_InputField signUpPassword;
    [SerializeField] private TMP_InputField signUpConfermationPassword;
    
    public DataBaseManager dataBase;

    private GameObject _signUpPanel;
    void Awake()
    {
        //_signUpPanel.SetActive(false);
        
    }

    public bool CorrectInformation()
    {
        return (firstName.text != null && lastName.text != null && signUpMail.text != null &&
                signUpPassword.text != null &&
                signUpConfermationMail.text != null && phoneNumber.text != null &&
                signUpConfermationPassword.text != null &&
                signUpPassword.text == signUpConfermationPassword.text &&
                signUpConfermationMail.text == signUpMail.text);
    }
    public void InformationToVerifyToSignIn()
    { 
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
        if (CorrectInformation())
        {
            dataBase.Register(firstName.text, lastName.text, signUpMail.text, phoneNumber.text, signUpPassword.text);
        }
        else
            Debug.Log("the information are not correct");    
    }
    
}
