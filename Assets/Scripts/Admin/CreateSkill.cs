using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreateSkill : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_InputField descriptionInputField;

    public void ApplyCreateSkill()
    {
        StartCoroutine(CSkill(nameInputField.text, descriptionInputField.text));
    }
    
    IEnumerator CSkill(string skillName, string skillDescription)
    {
        WWWForm form = new WWWForm();
        
        form.AddField("skillName",skillName);
        form.AddField("skillDescription",skillDescription);
        WWW www = new WWW("http://localhost/sql/createSkill.php", form);
        yield return www;
        if (www.text == "0")
        {
            Debug.Log("the skill : "+ skillName +" has been created successfully");
        }
        else
        {
            Debug.Log("ERROR : " +www.text);
        }
    }
}
