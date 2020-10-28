using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class SearchManager : MonoBehaviour
{
    [Header("SEARCH INPUT FIELDS")]
    [SerializeField] private TMP_InputField searchInputFieldUser;
    [SerializeField] private TMP_InputField searchInputFieldSkill;
    [SerializeField] private TMP_InputField searchInputFieldUserGroup;
    [SerializeField] private TMP_InputField searchInputFieldSkillGroup;
    
    [Header("RESULT PANELS")]
    [Space(30)]
    [SerializeField] private GameObject searchResultsPanelSkill;
    [SerializeField] private GameObject searchResultsPanelSkillGroup;
    [SerializeField] private GameObject searchResultsPanelUser;
    [SerializeField] private GameObject searchResultsPanelUserGroup;
    [SerializeField] private GameObject button;


    public void RegexSearchFromInputFieldUser()
    {
        StartCoroutine(RegexUsers());
    }
    
    public void RegexSearchFromInputFieldSkill()
    {
        StartCoroutine(RegexSkills());
    }
    
    public void RegexSearchFromInputFieldSkillGroup()
    {
        StartCoroutine(RegexSkillGroup());
    }
    
    public void RegexSearchFromInputFieldUserGroup()
    {
        StartCoroutine(RegexUserGroup());
    }
    
    IEnumerator RegexSkills()
    {
        WWWForm form = new WWWForm();
        form.AddField("regex",searchInputFieldSkill.text);
        form.AddField("table","skill");
        form.AddField("field","skillName");
        WWW www = new WWW("http://localhost/sql/search.php", form);
        yield return www;
        if(www.text[0] =='0')
        {
            var searchResultsList = new List<Tuple<string, GameObject>>();
            searchResultsList.Clear();
            for (int i = 0; i < searchResultsPanelSkill.transform.childCount; i++)
            {
                Destroy(searchResultsPanelSkill.transform.GetChild(i).gameObject);
            }
            
            //Debug.Log("search skills has finished  :" + www.text);
            var size = int.Parse(www.text.Split('\t')[1]);
            for (var i = 2; i < size+2 ; i++)
            {
                var skillInfo = www.text.Split('\t')[i];

                var go = Instantiate(button, searchResultsPanelSkill.transform);
                go.GetComponentInChildren<Text>().text = skillInfo;
                var skillPrefab = new Tuple<string, GameObject>(skillInfo, go);
                searchResultsList.Add(skillPrefab);
            }
        }
        else
        {
            Debug.Log("search users has failed, error : "+ www.text);
        }
    }
    
    IEnumerator RegexUsers()
    {
        WWWForm form = new WWWForm();
        form.AddField("regex",searchInputFieldUser.text);
        form.AddField("table","user");
        form.AddField("field","firstName");
        
        WWW www = new WWW("http://localhost/sql/search.php", form);
        yield return www;
        if(www.text[0] =='0')
        {
            var searchResultsList = new List<Tuple<string, GameObject>>();
            searchResultsList.Clear();
            for (var i = 0; i < searchResultsPanelUser.transform.childCount; i++)
            {
                Destroy(searchResultsPanelUser.transform.GetChild(i).gameObject);
            }
            
            var size = int.Parse(www.text.Split('\t')[1]);
            for (var i = 2; i < size+2 ; i++)
            {
                //Debug.Log( "welcome "+ www.text.Split('\t')[i]);
                
                var userInfo = www.text.Split('\t')[i];

                var go = Instantiate(button, searchResultsPanelUser.transform);
                go.GetComponentInChildren<Text>().text = userInfo;
                var userPrefab= new Tuple<string, GameObject>(userInfo, go);
                searchResultsList.Add(userPrefab);
            }
        }
        else
        {
            Debug.Log("search users has failed, error : "+ www.text);
        }
    }

    IEnumerator RegexSkillGroup()
    {
        WWWForm form = new WWWForm();
        form.AddField("regex",searchInputFieldSkillGroup.text);
        form.AddField("table","skillgroup");
        form.AddField("field","skillGroupName");
        
        WWW www = new WWW("http://localhost/sql/search.php", form);
        yield return www;
        if (www.text[0] == '0')
        {
            var searchResultsList = new List<Tuple<string, GameObject>>();
            searchResultsList.Clear();
            for (var i = 0; i < searchResultsPanelSkillGroup.transform.childCount; i++)
            {
                Destroy(searchResultsPanelSkillGroup.transform.GetChild(i).gameObject);
            }
            
            var size = int.Parse(www.text.Split('\t')[1]);
            for (var i = 2; i < size+2 ; i++)
            {
                var skillGroupInfo = www.text.Split('\t')[i];

                var go = Instantiate(button, searchResultsPanelSkillGroup.transform);
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

    IEnumerator RegexUserGroup()
    {
        WWWForm form = new WWWForm();
        form.AddField("regex",searchInputFieldUserGroup.text);
        form.AddField("table","usergroup");
        form.AddField("field","userGroupName");
        
        WWW www = new WWW("http://localhost/sql/search.php", form);
        yield return www;
        if (www.text[0] == '0')
        {
            var searchResultsList = new List<Tuple<string, GameObject>>();
            searchResultsList.Clear();
            for (var i = 0; i < searchResultsPanelUserGroup.transform.childCount; i++)
            {
                Destroy(searchResultsPanelUserGroup.transform.GetChild(i).gameObject);
            }
            
            var size = int.Parse(www.text.Split('\t')[1]);
            for (var i = 2; i < size+2 ; i++)
            {
                var userGroupInfo = www.text.Split('\t')[i];

                var go = Instantiate(button, searchResultsPanelUserGroup.transform);
                go.GetComponentInChildren<Text>().text = userGroupInfo;
                var userGroupPrefab= new Tuple<string, GameObject>(userGroupInfo, go);
                searchResultsList.Add(userGroupPrefab);
            }
        }
        else
        {
            Debug.Log("search userGroup has failed, error : "+ www.text);
        }
    }

    
   /* public void RegexSearchFromInputFieldUser()
    {
        
        List<DataBaseManager.User> results = dataBase.RegexSearch(searchInputField.text, "tableName", "fieldName");
        List<Tuple<int, GameObject>> idTuplesList = new List<Tuple<int, GameObject>>();
        
        idTuplesList.Clear();
        for (int i = 0; i < searchResultsPanel.transform.childCount; i++)
        {
            Destroy(searchResultsPanel.transform.GetChild(i).gameObject);
        }
        
        
        foreach (var item in results)
        {
            GameObject go = Instantiate(button, searchResultsPanel.transform);
            Tuple<int, GameObject> idTuples = new Tuple<int, GameObject>(item.ID, go);
            idTuplesList.Add(idTuples);
        }
    }*/

}
