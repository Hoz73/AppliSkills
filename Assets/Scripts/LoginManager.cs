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
    [Header("THE SIGNIN INFORMATION")]
    [SerializeField] private TMP_InputField signInMail;
    [SerializeField] private TMP_InputField signInPassword;
    
    //information needed to sign up
    [Header("THE SIGNUP INFORMATION")]
    [Space(30)]
    [SerializeField] private TMP_InputField firstName;
    [SerializeField] private TMP_InputField lastName;
    [SerializeField] private TMP_InputField signUpMail;
    [SerializeField] private TMP_InputField signUpConfermationMail;
    [SerializeField] private TMP_InputField phoneNumber;
    [SerializeField] private TMP_InputField signUpPassword;
    [SerializeField] private TMP_InputField signUpConfermationPassword;
    
    //Login panels
    [Header("LOGIN PANELS")]
    [Space(30)]
    [SerializeField]private GameObject _signUpPanel;
    [SerializeField] private GameObject _signInPanel;
    
    //DataBase instance
    private DataBaseManager dataBase;

    void Awake()
    {
        dataBase = new DataBaseManager();
    }

    public bool CorrectInformationToSignUp()
    {
        return (firstName.text !="" && lastName.text !="" && signUpMail.text != "" &&
                signUpPassword.text != "" &&
                signUpConfermationMail.text != "" && phoneNumber.text != "" &&
                signUpConfermationPassword.text != "" &&
                signUpPassword.text == signUpConfermationPassword.text &&
                signUpConfermationMail.text == signUpMail.text);
    }

    public bool CorrectInformationToSignIn()
    {
        return (signInMail.text != "" && signInPassword.text !="");
    }
    
    public void SignUpActivate()
    {
        _signUpPanel.SetActive(true);
        _signInPanel.SetActive(false);
    }
    public void SignInActivate()
    {
        _signInPanel.SetActive(true);
        _signUpPanel.SetActive(false);
    }
    
    public void InformationToVerifyToSignIn()
    {
        if(CorrectInformationToSignIn())
            dataBase.SignIn(signInMail.text, signInPassword.text);
        else
            Debug.Log("the information are not correct");   
    }

    public void InformationToVerifyToSignUp()
    {
        if (CorrectInformationToSignUp())
            dataBase.Register(firstName.text, lastName.text, signUpMail.text, phoneNumber.text, signUpPassword.text);
        else
            Debug.Log("the information are not correct");    
    }
    
    
    
}
