using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using UnityEngine.SceneManagement;

public class SupervisorSearchManagerScript : MonoBehaviour
{
    //[SerializeField] private GameObject userGroupSearchResultsPanel;
    [SerializeField] private GameObject skillGroupSearchResultsPanel;
    [SerializeField] private GameObject skillSkillGroupSearchResultsPanel;
    [SerializeField] private GameObject userSkillSearchResultsPanel;

    //[SerializeField] private TMP_InputField userGroupSearchInputField;
    [SerializeField] private TMP_InputField skillGroupSearchInputField;
    [SerializeField] private TMP_InputField skillSkillGroupSearchInputField;
    [SerializeField] private TMP_InputField userSkillSearchInputField;

    [SerializeField] private GameObject button;

    [SerializeField] GameObject BrowseBySkillBySkillGroupPanel;
    [SerializeField] GameObject BrowseByUserSkillPanel;
    [SerializeField] GameObject[] panels;

    [SerializeField] private TMP_Text lookedSkillGroup;
    [SerializeField] private TMP_Text lookedSkill;
    
    [Header("OTHERS")]
    [Space(30)]
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private TMP_Text userConnectedText;

    //public void OnEventSearchInputFieldSkillInputField()
    //{
    //    StartCoroutine(RegexSkillInputField(skillSearchResultsPanel, searchInputFieldSkill.text));
    //}

    void Awake()
    {
        userConnectedText.text = "Connected as supervisor: " + DataBaseManager.UserName;
    }
    public void OnEventSearchInputFieldSkillGroupInputField()
    {
        StartCoroutine(RegexSkillGroupInputField(skillGroupSearchResultsPanel, skillGroupSearchInputField.text));
    }

    // public void OnEventSearchInputFieldUserGroupInputField()
    // {
    //     StartCoroutine(RegexUserGroup(userGroupSearchResultsPanel, userGroupSearchInputField.text));
    // }

    public void OnEventSearchInputFieldskillSkillGroupInputField()
    {
        StartCoroutine(RegexskillSkillGroupInputField(skillSkillGroupSearchResultsPanel, skillSkillGroupSearchInputField.text));
    }

    public void OnEventSearchInputFieldUserSkillInputField()
    {
        StartCoroutine(RegexUserSkillInputField(userSkillSearchResultsPanel, userSkillSearchInputField.text));
    }

    IEnumerator RegexSkillGroupInputField(GameObject resultPanel, string inputField)
    {
        //DataBaseManager.UserId = "5";

        WWWForm form = new WWWForm();
        form.AddField("regex", inputField);
        form.AddField("userID", DataBaseManager.UserId);
    
        WWW www = new WWW("http://localhost/sql/supervisor/search.php", form);
        yield return www;
        if (www.text[0] == '0')
        {
            var searchResultsList = new List<Tuple<string, GameObject>>();
            searchResultsList.Clear();

            for (int i = 0; i < resultPanel.transform.childCount; i++)
            {
                Destroy(resultPanel.transform.GetChild(i).gameObject);
            }

            var size = int.Parse(www.text.Split('\t')[1]);
            for (var i = 2; i < size + 2; i++)
            {
                var skillInfo = www.text.Split('\t')[i];

                var go = Instantiate(button, resultPanel.transform);
                go.GetComponentInChildren<Text>().text = skillInfo;
                var skillPrefab = new Tuple<string, GameObject>(skillInfo, go);
                searchResultsList.Add(skillPrefab);
            }
        }
        else
        {
            Debug.Log("skillgroup search has failed, error : " + www.text);
        }
    }

    IEnumerator RegexskillSkillGroupInputField(GameObject resultPanel, string inputField)
    {

        WWWForm form = new WWWForm();
        form.AddField("regex", inputField);
        form.AddField("skillGroupName", DataBaseManager.ChosenSkillGroup);
    
        WWW www = new WWW("http://localhost/sql/supervisor/skillSkillGroupSearch.php", form);
        yield return www;
        if (www.text[0] == '0')
        {
            var searchResultsList = new List<Tuple<string, GameObject>>();
            searchResultsList.Clear();

            for (int i = 0; i < resultPanel.transform.childCount; i++)
            {
                Destroy(resultPanel.transform.GetChild(i).gameObject);
            }

            var size = int.Parse(www.text.Split('\t')[1]);
            for (var i = 2; i < size + 2; i++)
            {
                var skillInfo = www.text.Split('\t')[i];

                var go = Instantiate(button, resultPanel.transform);
                go.GetComponentInChildren<Text>().text = skillInfo;
                var skillPrefab = new Tuple<string, GameObject>(skillInfo, go);
                searchResultsList.Add(skillPrefab);
            }
        }
        else
        {
            Debug.Log("skills search has failed, error : " + www.text);
        }
    }

    IEnumerator RegexUserSkillInputField(GameObject resultPanel, string inputField)
    {

        WWWForm form = new WWWForm();
        form.AddField("regex", inputField);
        form.AddField("skillName", DataBaseManager.ChosenSkillSkillGroup);
    
        WWW www = new WWW("http://localhost/sql/supervisor/userSearch.php", form);
        yield return www;
        Debug.Log(www.text);
        if (www.text[0] == '0')
        {
            var searchResultsList = new List<Tuple<string, GameObject>>();
            searchResultsList.Clear();

            for (int i = 0; i < resultPanel.transform.childCount; i++)
            {
                Destroy(resultPanel.transform.GetChild(i).gameObject);
            }

            var size = int.Parse(www.text.Split('\t')[1]);
            for (var i = 2; i < size + 2; i++)
            {
                var skillInfo = www.text.Split('\t')[i];

                var go = Instantiate(button, resultPanel.transform);
                go.GetComponentInChildren<Text>().text = skillInfo;
                var skillPrefab = new Tuple<string, GameObject>(skillInfo, go);
                searchResultsList.Add(skillPrefab);
            }
        }
        else
        {
            Debug.Log("user search has failed, error : " + www.text);
        }
    }

    public void PollSkillGroup(GameObject panel)
    {
        var searchResultPanel = panel;
        int counter = 0;
        for (var i = 0; i < searchResultPanel.transform.childCount; i++)
        {
            if (searchResultPanel.transform.GetChild(i).GetComponent<Image>().color == Color.red)
            {
                var text = searchResultPanel.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text;
                var skillGroup = text.Split(' ')[0];
                DataBaseManager.ChosenSkillGroup = skillGroup;
                counter++;
            }
        }
        if (counter != 1)
        {
            Debug.Log("invalid action");
            errorText.text = "you can't chose more than one skillGroup";
            errorText.transform.parent.gameObject.SetActive(true);
            DataBaseManager.ChosenSkillGroup = null;
        }
        else{
            //turn off every panel
            foreach (var p in panels)
            {
                p.SetActive(false);
            }
            BrowseBySkillBySkillGroupPanel.SetActive(true);
            lookedSkillGroup.text = "Skills in : "+ DataBaseManager.ChosenSkillGroup;    
        }
    }

    public void PollSkillSkillGroup(GameObject panel)
    {
        var searchResultPanel = panel;
        int counter = 0;
        for (var i = 0; i < searchResultPanel.transform.childCount; i++)
        {
            if (searchResultPanel.transform.GetChild(i).GetComponent<Image>().color == Color.red)
            {
                var skillGroup = searchResultPanel.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text;
                DataBaseManager.ChosenSkillSkillGroup = skillGroup;
                counter++;
            }
        }
        if (counter != 1)
        {
            Debug.Log("invalid action");
            errorText.text = "you can't chose more than one Skill";
            errorText.transform.parent.gameObject.SetActive(true);
            DataBaseManager.ChosenSkillSkillGroup = null;
        }
        else{
            //turn off every panel
            foreach (var p in panels)
            {
                p.SetActive(false);
            }
            BrowseByUserSkillPanel.SetActive(true);
            lookedSkill.text = "Users in : "+ DataBaseManager.ChosenSkillSkillGroup;    
        }
    }

    public void SwitchState()
    {
        for (var i = 0; i < userSkillSearchResultsPanel.transform.childCount; i++)
        {
            if (userSkillSearchResultsPanel.transform.GetChild(i).GetComponent<Image>().color == Color.red)
            {
                var text = userSkillSearchResultsPanel.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text;   
                string[] afterSplit = text.Split(' ');
                string id = afterSplit[0];
                string userName = afterSplit[1];
                string userLastName = afterSplit[2];
                string state = afterSplit[afterSplit.Length - 1];
            
                StartCoroutine(UpdateState(id,userName, userLastName, state));
                //ClearResultPanels();
            }
        }
    }

    
    IEnumerator UpdateState(string userID, string userName, string UserLastName, string state)
    {
        string newState = null; 
        switch (state)
        {
            case "selfValidated":
                newState = "valid";
                break;
            case "valid":
                newState = "selfValidated";
                break;
        }
        if (newState != null)
        {
            WWWForm form = new WWWForm();
            form.AddField("state",newState);
            form.AddField("skillName",DataBaseManager.ChosenSkillSkillGroup);
            form.AddField("userID", userID);
            
            WWW www = new WWW("http://localhost/sql/supervisor/updateSkillState.php", form);
            yield return www;
            if (www.text[0] == '0')
            {
                errorText.text = " you have switched user : "+ userName + " " + UserLastName + "  to : " + newState;
                errorText.transform.parent.gameObject.SetActive(true);
                Debug.Log(" you have switched user : "+ userName + " " + UserLastName + "  to : " + newState);
                ClearResultPanels();
            }
            else
            {
                errorText.text = "error trying to update skill state" + www.text;
                errorText.transform.parent.gameObject.SetActive(true);
                Debug.Log("error trying to update skill state" + www.text);
            }
        }
        else
        {
            Debug.Log("Error validating skill");
            //errorText.text = "you can't invalid a skill validated by the teacher" ;
            //errorText.transform.parent.gameObject.SetActive(true);
        }        
    }

    public void ClearResultPanels()
    {
        for (var i = 0; i < userSkillSearchResultsPanel.transform.childCount; i++)
        {
            Destroy(userSkillSearchResultsPanel.transform.GetChild(i).gameObject);
        }
    }

    public void Disconnect()
    {
        DataBaseManager.LogOut();
        SceneManager.LoadScene("LogIn");
    }
}
 