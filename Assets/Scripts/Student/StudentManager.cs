using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StudentManager : MonoBehaviour
{
    [Header("STUDENT PANELS")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject skillGroupPanel;
    [SerializeField] private GameObject skillPanel;
    
    
    [Header("SEARCH INPUTS")]
    [Space(30)]
    [SerializeField] private TMP_InputField skillGroupSearchInputField;
    [SerializeField] private TMP_InputField skillSearchInputField;
    
    
    [Header("SEARCH RESULT PANELS")]
    [Space(30)]
    [SerializeField] private GameObject skillGroupResultPanel;
    [SerializeField] private GameObject skillResultPanel;
    
    [Header("PREFABS")]
    [Space(30)]
    [SerializeField] private GameObject button;

    
    public void SkillGroupPanelActivate()
    {
        skillGroupPanel.SetActive(true);
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
    }

    public void Disconnect()
    {
        Debug.Log("Logout");
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

    IEnumerator SkillGroup(GameObject resultPanel, string inputField)
    {
        WWWForm form = new WWWForm();
        form.AddField("regex",inputField);
        form.AddField("idUser", DataBaseManager.UserId);
        
        WWW www = new WWW("http://localhost/sql/student/searchSkillGroup.php", form);
        yield return www;
        Debug.Log(www.text);
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
        }
    }

    IEnumerator Skill(GameObject resultPanel, string inputField)
    {
        WWWForm form = new WWWForm();
        form.AddField("regex",inputField);
        form.AddField("skillGroupName", DataBaseManager.SkillGroupNameToEdit);
        
        WWW www = new WWW("http://localhost/sql/student/searchSkill.php", form);
        yield return www;
        Debug.Log(www.text);
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
            Debug.Log("search skill has failed, error : "+ www.text);
        }
    }
}
