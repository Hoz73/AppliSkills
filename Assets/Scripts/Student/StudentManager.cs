using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StudentManager : MonoBehaviour
{
    [Header("STUDENT PANELS")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject skillGroupPanel;
    [SerializeField] private GameObject allSkillGroupPanel;
    [SerializeField] private GameObject skillPanel;
    
    
    [Header("SEARCH INPUTS")]
    [Space(30)]
    [SerializeField] private TMP_InputField skillGroupSearchInputField;
    [SerializeField] private TMP_InputField allSkillGroupSearchInputField;
    [SerializeField] private TMP_InputField skillSearchInputField;
    
    
    [Header("SEARCH RESULT PANELS")]
    [Space(30)]
    [SerializeField] private GameObject skillGroupResultPanel;
    [SerializeField] private GameObject allSkillGroupResultPanel;
    [SerializeField] private GameObject skillResultPanel;
    
    [Header("PREFABS")]
    [Space(30)]
    [SerializeField] private GameObject button;
    
    [Header("OTHERS")]
    [Space(30)]
    [SerializeField] private TMP_Text userConnectedText;
    [SerializeField] private TMP_Text errorText;



    void Awake()
    {
        userConnectedText.text = "Connected as : " + DataBaseManager.UserName;
    }
    
    public void SkillGroupPanelActivate()
    {
        skillGroupPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void AllSkillsGroupPanelActivate()
    {
        allSkillGroupPanel.SetActive(true);
        mainPanel.SetActive(false);
    }
    
    public void SkillPanelActivate()
    {
                
        for (var i = 0; i < skillGroupResultPanel.transform.childCount; i++)
        {
            if (skillGroupResultPanel.transform.GetChild(i).GetComponent<Image>().color == Color.red)
            {
                var text = skillGroupResultPanel.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text;
                var skillGroupName = text.Split(' ')[0];
                DataBaseManager.SkillGroupNameToEdit = skillGroupName;
            }
        }
        Debug.Log(DataBaseManager.SkillGroupNameToEdit);
        skillPanel.SetActive(true);
        skillGroupPanel.SetActive(false);
    }
    
    public void ReturnToMainPanel()
    {
        mainPanel.SetActive(true);
        
        skillPanel.SetActive(false);
        skillGroupPanel.SetActive(false);
        allSkillGroupPanel.SetActive(false);
        
        ClearResultPanels();
    }

    public void Disconnect()
    {
        SceneManager.LoadScene("Login");
        DataBaseManager.LogOut();
    }


    public void SwitchState()
    {
        for (var i = 0; i < skillResultPanel.transform.childCount; i++)
        {
            if (skillResultPanel.transform.GetChild(i).GetComponent<Image>().color == Color.red)
            {
                var text = skillResultPanel.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text;
                string[] afterSplit = text.Split(' ');
                string state = afterSplit[afterSplit.Length - 1];
                
                string skillName = null;
                for (int j = 0; j < afterSplit.Length-1; j++)
                {
                    skillName += afterSplit[j];
                    if (j < afterSplit.Length - 2) skillName += ' ';
                }

                StartCoroutine(UpdateState(skillName,state));
                ClearResultPanels();
            }
        }
    }
    
    public void AddSkillGroupToStudent()
    {
        var groupList =new List<string>();
        for (var i = 0; i < allSkillGroupResultPanel.transform.childCount; i++)
        {
            if (allSkillGroupResultPanel.transform.GetChild(i).GetComponent<Image>().color == Color.red)
            {
                var text = allSkillGroupResultPanel.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text;
                var skillGroupName = text.Split(' ')[0];
                groupList.Add(skillGroupName);
            }
        }

        if (groupList.Count > 0) StartCoroutine(AddSkillGroupToUser(groupList));
    }


    IEnumerator AddSkillGroupToUser(List<string> groupList)
    {
        foreach (var groupName in groupList)
        {
            WWWForm form = new WWWForm();
            form.AddField("idUser",DataBaseManager.UserId);
            form.AddField("groupName", groupName);
            WWW www = new WWW("http://localhost/sql/student/addSkillGroupToStudent.php", form);
            yield return www;
            if (www.text[0] == '0')
            {
                Debug.Log("yes !!!!!!");
            }
            else
            {
                Debug.Log("NOOOOOON !!!!!!" + www.text);
                errorText.text = "Error : " + www.text;
                errorText.transform.parent.gameObject.SetActive(true);
            }
        }
        
    }
    
    // search functions
    public void RegexSkillGroup()
    {
        StartCoroutine(SkillGroup(skillGroupResultPanel, skillGroupSearchInputField.text));
    }

    public void RegexSkill()
    {
        StartCoroutine(Skill(skillResultPanel, skillSearchInputField.text));
    }
    
    public void RegexAllSkillGroup()
    {
        StartCoroutine(AllSkill(allSkillGroupResultPanel, allSkillGroupSearchInputField.text));
    }

    public void ClearResultPanels()
    {
        for (var i = 0; i < skillGroupResultPanel.transform.childCount; i++)
        {
            Destroy(skillGroupResultPanel.transform.GetChild(i).gameObject);
        }
        
        for (var i = 0; i < allSkillGroupResultPanel.transform.childCount; i++)
        {
            Destroy(allSkillGroupResultPanel.transform.GetChild(i).gameObject);
        }
        
        for (var i = 0; i < skillResultPanel.transform.childCount; i++)
        {
            Destroy(skillResultPanel.transform.GetChild(i).gameObject);
        }
    }

    IEnumerator SkillGroup(GameObject resultPanel, string inputField)
    {
        WWWForm form = new WWWForm();
        form.AddField("regex",inputField);
        form.AddField("idUser", DataBaseManager.UserId);
        
        WWW www = new WWW("http://localhost/sql/student/searchSkillGroup.php", form);
        yield return www;
        if (www.text[0] == '0')
        {
            var searchResultsList = new List<Tuple<string, GameObject>>();
            searchResultsList.Clear();
            Debug.Log(resultPanel.transform.childCount);
            
            for (var i = 0; i < resultPanel.transform.childCount; i++)
            {
                Debug.Log(resultPanel.transform.name);
                Destroy(resultPanel.transform.GetChild(i).gameObject);
            }
            
            var size = int.Parse(www.text.Split('\t')[1]);
            for (var i = 2; i < size+2 ; i++)
            {
                var skillGroupInfo = www.text.Split('\t')[i];

                var go = Instantiate(button, resultPanel.transform);
                go.GetComponentInChildren<Text>().text = skillGroupInfo;
                var skillGroupPrefab= new Tuple<string, GameObject>(skillGroupInfo, go);
                searchResultsList.Add(skillGroupPrefab);
            }
        }
        else
        {
            Debug.Log("search skillGroup has failed, error : "+ www.text);
            errorText.text = "No match : " + www.text;
            errorText.transform.parent.gameObject.SetActive(true);
        }
    }

    IEnumerator Skill(GameObject resultPanel, string inputField)
    {
        WWWForm form = new WWWForm();
        form.AddField("regex",inputField);
        form.AddField("skillGroupName", DataBaseManager.SkillGroupNameToEdit);
        
        WWW www = new WWW("http://localhost/sql/student/searchSkill.php", form);
        yield return www;
        if (www.text[0] == '0')
        {
            var searchResultsList = new List<Tuple<string, GameObject>>();
            searchResultsList.Clear();

            for (var i = 0; i < resultPanel.transform.childCount; i++)
            {
                Destroy(resultPanel.transform.GetChild(i).gameObject);
            }
            
            var size = int.Parse(www.text.Split('\t')[1]);
            for (var i = 2; i < size+2 ; i++)
            {
                var skillGroupInfo = www.text.Split('\t')[i];

                var go = Instantiate(button, resultPanel.transform);
                go.GetComponentInChildren<Text>().text = skillGroupInfo;
                var skillGroupPrefab= new Tuple<string, GameObject>(skillGroupInfo, go);
                searchResultsList.Add(skillGroupPrefab);
            }
        }
        else
        {
            Debug.Log("search skill has failed, error : "+ www.text);
            errorText.text = "No match : " + www.text;
            errorText.transform.parent.gameObject.SetActive(true);
        }
    }

    IEnumerator AllSkill(GameObject resultPanel, string inputField)
    {
        WWWForm form = new WWWForm();
        form.AddField("regex",inputField);
        WWW www = new WWW("http://localhost/sql/student/searchAllSkillGroup.php", form);
        yield return www;
        if (www.text[0] == '0')
        {
            var searchResultsList = new List<Tuple<string, GameObject>>();
            searchResultsList.Clear();

            for (var i = 0; i < resultPanel.transform.childCount; i++)
            {
                Destroy(resultPanel.transform.GetChild(i).gameObject);
            }
            
            var size = int.Parse(www.text.Split('\t')[1]);
            for (var i = 2; i < size+2 ; i++)
            {
                var skillGroupInfo = www.text.Split('\t')[i];

                var go = Instantiate(button, resultPanel.transform);
                go.GetComponentInChildren<Text>().text = skillGroupInfo;
                var skillGroupPrefab= new Tuple<string, GameObject>(skillGroupInfo, go);
                searchResultsList.Add(skillGroupPrefab);
            }
        }
        else
        {
            Debug.Log("search all skill group has failed, error : "+ www.text);
            errorText.text = "No match : " + www.text;
            errorText.transform.parent.gameObject.SetActive(true);
        }
    }

    IEnumerator UpdateState(string skillName, string state)
    {
        string newState = null; 
        switch (state)
        {
            case "invalid":
                newState = "selfValidated";
                break;
            case "selfValidated":
                newState = "invalid";
                break;
        }
        if (newState != null)
        {
            WWWForm form = new WWWForm();
            form.AddField("state",newState);
            form.AddField("skillName",skillName);
            form.AddField("idUser",DataBaseManager.UserId);
            
            WWW www = new WWW("http://localhost/sql/student/updateSkillState.php", form);
            yield return www;
            if (www.text[0] == '0')
                Debug.Log(" you have switch your skill: "+ skillName+ "  to : " + newState);
        }
        else
        {
            Debug.Log(" you can't invalid a skill validated by the teacher");
            errorText.text = "you can't invalid a skill validated by the teacher" ;
            errorText.transform.parent.gameObject.SetActive(true);
        }

        
        
    }
        
    
    
}
