using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateSkillGroup : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    
    [Header("OTHERS")]
    [Space(30)]
    [SerializeField] private TMP_Text errorText;
    
    public void ApplyCreateSkillGroup()
    { 
        var buttonPressed = 0;
        string text;
        string firstName = null;
        string lastName = null;
        for (var i = 0; i < gameObject.transform.childCount; i++)
        {
            var color = gameObject.transform.GetChild(i).GetComponent<Image>().color;
            if (color == Color.red)
            {
                buttonPressed++;
                text = gameObject.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text;
                firstName = text.Split(' ')[0];
                lastName = text.Split(' ')[1];
            }
        }

        switch (buttonPressed)
        {
            case 1:
                StartCoroutine(CSkillGroup(firstName, lastName));
                break;
            case 0:
                errorText.text = "you need to chose a supervisor for your skillGroup";
                errorText.transform.parent.gameObject.SetActive(true);
                Debug.Log("you need to chose a supervisor for your skillGroup");
                break;
            default:
                errorText.text = "you can't chose more than one supervisor for your skillGroup";
                errorText.transform.parent.gameObject.SetActive(true);
                Debug.Log("you can't chose more than one supervisor for your skillGroup");
                break;
        }
    }

    IEnumerator CSkillGroup(string firstName, string lastName)//TODO testing stage
    {
        WWWForm form = new WWWForm();
        form.AddField("skillGroupName",nameInputField.text);
        form.AddField("supervisorFirstName",firstName);
        form.AddField("supervisorLastName",lastName);
        WWW www = new WWW("http://localhost/sql/createSkillGroup.php", form);
        yield return www;
        if (www.text == "0")
        {
            Debug.Log("SkillGroup created successfully");
            errorText.text = "SkillGroup created successfully";
            errorText.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            errorText.text = "Error : " + www.text;
            errorText.transform.parent.gameObject.SetActive(true);
            Debug.Log(www.text);
        }
    }
}
