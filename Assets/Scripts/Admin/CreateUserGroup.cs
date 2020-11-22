using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateUserGroup : MonoBehaviour
{ 
    [SerializeField] private TMP_InputField nameInputField;
    
    [Header("OTHERS")]
    [Space(30)]
    [SerializeField] private TMP_Text errorText;
    public void ApplyCreateUserGroup()
    { 
        var buttonPressed = 0;
        string text;
        string firstName = null;
        string lastName = null;
        var studentsList = new List<Tuple<string, string>>();
        
        for (var i = 0; i < gameObject.transform.childCount; i++)
        {
            var color = gameObject.transform.GetChild(i).GetComponent<Image>().color;
            if (color == Color.red)
            {
                buttonPressed++;
                text = gameObject.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text;
                firstName = text.Split(' ')[0];
                lastName = text.Split(' ')[1];
                var student = new Tuple<string,string>(firstName,lastName);
                studentsList.Add(student);
            }
        }

        if (buttonPressed == 0)
        {
            errorText.text = "you need to add some students to your studentGroup";
            errorText.transform.parent.gameObject.SetActive(true);
            Debug.Log("you need to add some students to your studentGroup");
        }
        else
        {
            for (var i = 0; i < buttonPressed; i++)
            {
                StartCoroutine(CUserGroup(studentsList[i].Item1, studentsList[i].Item2, i));
            }
        }
    }

    IEnumerator CUserGroup(string firstName, string lastName, int i)
    {
        WWWForm form = new WWWForm();
        form.AddField("createUserGroup",i);
        form.AddField("userGroupName",nameInputField.text);
        form.AddField("studentFirstName",firstName);
        form.AddField("studentLastName",lastName);
        WWW www = new WWW("http://localhost/sql/createUserGroup.php", form);
        yield return www;
        if (www.text == "0")
        {
            Debug.Log("userGroup/student has been created/added successfully");
            errorText.text = "userGroup/student has been created/added successfully";
            errorText.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            errorText.text = "ERROR : " +www.text;
            errorText.transform.parent.gameObject.SetActive(true);
            Debug.Log("ERROR : " +www.text);
        }
    }
}
