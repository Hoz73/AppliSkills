using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModifyPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField newNameUserGroupInputField;
    [SerializeField] private TMP_InputField newNameSkillGroupInputField;
    
    [SerializeField] private GameObject listOfStudentToAdd;
    [SerializeField] private GameObject listOfStudentToDelete;
    
    [SerializeField] private GameObject listOfSkillToAdd;
    [SerializeField] private GameObject listOfSkillToDelete;
    
    

    
    public void ApplyChangesUserGroup()
    {
        var add = ChangesUserGroup(listOfStudentToAdd,"add");
        var delete = ChangesUserGroup(listOfStudentToDelete,"delete");

        if (add + delete == 0)
            Debug.Log("no changes to do");
        
    }

    public void ApplyChangesSkillGroup()
    {
        var add = ChangesSkillGroup(listOfSkillToAdd, "add");
        var delete = ChangesSkillGroup(listOfSkillToDelete, "delete");
        
        if(add + delete ==0)
            Debug.Log("no changes to do");
    }

    public int ChangesSkillGroup(GameObject list, string edit)
    {
        var buttonPressed = 0;
        string text;
        string skillName = null;
        var skillsList = new List<string >();
        
        for (var i = 0; i < list.transform.childCount; i++)
        {
            var color = list.transform.GetChild(i).GetComponent<Image>().color;
            if (color == Color.red)
            {
                buttonPressed++;
                text = list.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text;
                skillName = text.Split('\t')[0];
                skillsList.Add(skillName);
            }
        }
        
        if (buttonPressed == 0)
            return 0;
        else
        {
            if (edit == "add")
                for (var i = 0; i < buttonPressed; i++)
                    StartCoroutine(AddSkillToSkillGroup(skillsList[i]));
                
            if(edit == "delete")
                for (var i = 0; i < buttonPressed; i++)
                    StartCoroutine(DeleteSkillFromSkillGroup(skillsList[i]));
            return 1;
        }
    }

    public int ChangesUserGroup(GameObject list, string edit)
    {
        var buttonPressed = 0;
        string text;
        string firstName = null;
        string lastName = null;
        var studentsList = new List<Tuple<string, string>>();
        
        for (var i = 0; i < list.transform.childCount; i++)
        {
            var color = list.transform.GetChild(i).GetComponent<Image>().color;
            if (color == Color.red)
            {
                buttonPressed++;
                text = list.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text;
                firstName = text.Split(' ')[0];
                lastName = text.Split(' ')[1];
                var student = new Tuple<string,string>(firstName,lastName);
                studentsList.Add(student);
            }
        }

        if (buttonPressed == 0)
            return 0;
        else
        {
            if (edit == "add")
                for (var i = 0; i < buttonPressed; i++)
                    StartCoroutine(AddStudentToUserGroup(studentsList[i].Item1, studentsList[i].Item2));
                
            if(edit == "delete")
                for (var i = 0; i < buttonPressed; i++)
                    StartCoroutine(DeleteStudentFromUserGroup(studentsList[i].Item1, studentsList[i].Item2));
            return 1;
        }
    }

    IEnumerator AddSkillToSkillGroup(string skillName)
    {
        WWWForm form = new WWWForm();
        form.AddField("skillGroupName", DataBaseManager.SkillGroupNameToEdit);
        form.AddField("newSkillGroupName",newNameSkillGroupInputField.text); //TODO later
        form.AddField("skillName",skillName);
        WWW www = new WWW("http://localhost/sql/addSkillToSkillGroup.php", form);
        yield return www;
        if (www.text == "0")
        {
            Debug.Log("the skill ( "+ skillName +" ) has been added successfully to the group :" + DataBaseManager.SkillGroupNameToEdit);
        }
        else
        {
            Debug.Log("ERROR : " +www.text);
        }
    }

    IEnumerator AddStudentToUserGroup(string firstName, string lastName)
    {
        WWWForm form = new WWWForm();
        form.AddField("userGroupName", DataBaseManager.UserGroupNameToEdit);
        form.AddField("newUserGroupName",newNameUserGroupInputField.text); //TODO later
        form.AddField("studentFirstName",firstName);
        form.AddField("studentLastName",lastName);
        WWW www = new WWW("http://localhost/sql/addStudentToUserGroup.php", form);
        yield return www;
        if (www.text == "0")
        {
            Debug.Log("the student ( "+ firstName +" "+ lastName +" ) has been added successfully to the group :" + DataBaseManager.UserGroupNameToEdit);
        }
        else
        {
            Debug.Log("ERROR : " +www.text);
        }
    }

    IEnumerator DeleteSkillFromSkillGroup(string skillName)
    {
        WWWForm form = new WWWForm();
        form.AddField("skillGroupName", DataBaseManager.SkillGroupNameToEdit);
        form.AddField("newSkillGroupName",newNameSkillGroupInputField.text); //TODO later
        form.AddField("skillName",skillName);
        WWW www = new WWW("http://localhost/sql/deleteSkillFromSkillGroup.php", form);
        yield return www;
        if (www.text == "0")
        {
            Debug.Log("the skill ( "+ skillName +" ) has been deleted successfully from the group :" + DataBaseManager.SkillGroupNameToEdit);
        }
        else
        {
            Debug.Log("ERROR : " +www.text);
        }
    }

    IEnumerator DeleteStudentFromUserGroup(string firstName, string lastName)
    {
        WWWForm form = new WWWForm();
        form.AddField("userGroupName", DataBaseManager.UserGroupNameToEdit);
        form.AddField("newUserGroupName",newNameUserGroupInputField.text); //TODO later
        form.AddField("studentFirstName",firstName);
        form.AddField("studentLastName",lastName);
        WWW www = new WWW("http://localhost/sql/deleteStudentToUserGroup.php", form);
        yield return www;
        if (www.text == "0")
        {
            Debug.Log("the student ( "+ firstName +" "+ lastName +" ) has been deleted successfully from the group :" + DataBaseManager.UserGroupNameToEdit);
        }
        else
        {
            Debug.Log("ERROR : " +www.text);
        }
    }
    


    
    
}
