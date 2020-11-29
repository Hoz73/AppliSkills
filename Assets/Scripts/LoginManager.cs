using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    
    [Header("OTHERS")]
    [Space(30)]
    [SerializeField] private TMP_Text errorText;

    private bool _teacher;

    void Awake()
    {
        _teacher = false;
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
        if (CorrectInformationToSignIn())
            StartCoroutine(LogIn());
        else
        {
            errorText.text = "The information is not correct";
            errorText.transform.parent.gameObject.SetActive(true);
            Debug.Log("the information are not correct");  
        }
             
    }

    public void InformationToVerifyToSignUp()
    {
        if (CorrectInformationToSignUp())
            StartCoroutine(Registers());
        else
        {
            errorText.text = "The information is not correct";
            errorText.transform.parent.gameObject.SetActive(true);
            Debug.Log("the information are not correct");
        }
            
    }

    public void ToggleChanged()
    {
        _teacher = !_teacher;
    }

    IEnumerator LogIn()
    {
        WWWForm form = new WWWForm();
        form.AddField("mail",signInMail.text);
        form.AddField("password",signInPassword.text);
        WWW www = new WWW("http://localhost/sql/logIn.php", form);
        yield return www;
        if(www.text[0] =='0')
        {
            DataBaseManager.UserName = www.text.Split('\t')[1];
            DataBaseManager.Role = www.text.Split('\t')[2];
            DataBaseManager.UserId = www.text.Split('\t')[3];
            
            if (DataBaseManager.Role == "admin")
            {
                SceneManager.LoadScene("AdminInterface");
            }
            else if (DataBaseManager.Role == "teacher")
            {
                SceneManager.LoadScene("SupervisorInterface");
            }
            else
            {
                SceneManager.LoadScene("StudentInterface");
            }
                
        }
        else
        {
            errorText.text = "logging in failed, error : "+ www.text;
            errorText.transform.parent.gameObject.SetActive(true);
            Debug.Log("logging in failed, error : "+ www.text);
        }
    }

    IEnumerator Registers()
    {
        WWWForm form = new WWWForm();
        form.AddField("firstName",firstName.text);
        form.AddField("lastName",lastName.text);
        form.AddField("mail",signUpMail.text);
        form.AddField("phone",phoneNumber.text);
        form.AddField("password",signUpPassword.text);
        form.AddField("type", _teacher ? "teacher" : "student");

        WWW www = new WWW("http://localhost/sql/register.php", form);
        yield return www;
        if(www.text =="0")
        {
            Debug.Log("User created successfully");
            SignInActivate();
        }
        else
        {
            errorText.text = "User creation failed, error :"+ www.text;
            errorText.transform.parent.gameObject.SetActive(true);
            Debug.Log("User creation failed, error : "+ www.text);
        }
    }

    public void back()
    {
        _signUpPanel.SetActive(false);
        _signInPanel.SetActive(true);
    }
    
    
    
}
