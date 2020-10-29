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
    [SerializeField] private TMP_InputField searchInputFieldSkillToAdd;
    [SerializeField] private TMP_InputField searchInputFieldSkillToDelete;
    [SerializeField] private TMP_InputField searchInputFieldUserGroup;
    [SerializeField] private TMP_InputField searchInputFieldSkillGroup;
    [SerializeField] private TMP_InputField searchInputFieldAddStudent;
    [SerializeField] private TMP_InputField searchInputFieldDeleteStudent;
    [SerializeField] private TMP_InputField searchInputFieldSupervisor;
    [SerializeField] private TMP_InputField searchInputFieldStudent;
    
    [Header("RESULT PANELS")]
    [Space(30)]
    [SerializeField] private GameObject listOfSkillsToAdd;
    [SerializeField] private GameObject listOfSkillsToDelete;
    [SerializeField] private GameObject searchResultsPanelSkillGroup;
    [SerializeField] private GameObject searchResultsPanelUser;
    [SerializeField] private GameObject searchResultsPanelUserGroup;
    [SerializeField] private GameObject listOfStudentsToAdd;
    [SerializeField] private GameObject listOfStudentsToDelete;
    [SerializeField] private GameObject listOfSupervisors;
    [SerializeField] private GameObject listOfStudents;
    [SerializeField] private GameObject button;


    public void RegexSearchFromInputFieldUser()
    {
        StartCoroutine(RegexUsers(searchResultsPanelUser, searchInputFieldUser.text));
    }
    
    public void RegexSearchFromInputFieldSkillToAdd()
    {
        StartCoroutine(RegexSkills(listOfSkillsToAdd, searchInputFieldSkillToAdd.text));
    }
    
    public void RegexSearchFromInputFieldSkillToDelete()
    {
        StartCoroutine(RegexSkills(listOfSkillsToDelete, searchInputFieldSkillToDelete.text));
    }
    
    public void RegexSearchFromInputFieldSkillGroup()
    {
        StartCoroutine(RegexSkillGroup(searchResultsPanelSkillGroup, searchInputFieldSkillGroup.text));
    }
    
    public void RegexSearchFromInputFieldUserGroup()
    {
        StartCoroutine(RegexUserGroup(searchResultsPanelUserGroup, searchInputFieldUserGroup.text));
    }

    public void RegexSearchFromInputFieldStudentToAdd()
    {
        StartCoroutine(RegexStudent(listOfStudentsToAdd, searchInputFieldAddStudent.text));
    }
    
    public void RegexSearchFromInputFieldStudentToDelete()
    {
        StartCoroutine(RegexStudent(listOfStudentsToDelete, searchInputFieldDeleteStudent.text));
    }
    
    public void RegexSearchFromInputFieldTeacher()
    {
        StartCoroutine(RegexTeacher(listOfSupervisors, searchInputFieldSupervisor.text));
    }
    
    public void RegexSearchFromInputFieldStudent()
    {
        StartCoroutine(RegexStudent(listOfStudents, searchInputFieldStudent.text));
    }
    
    IEnumerator RegexSkills(GameObject resultPanel, string inputField)
    {
        WWWForm form = new WWWForm();
        form.AddField("regex",inputField);
        form.AddField("table","skill");
        form.AddField("field","skillName");
        WWW www = new WWW("http://localhost/sql/search.php", form);
        yield return www;
        if(www.text[0] =='0')
        {
            var searchResultsList = new List<Tuple<string, GameObject>>();
            searchResultsList.Clear();
            for (int i = 0; i < resultPanel.transform.childCount; i++)
            {
                Destroy(resultPanel.transform.GetChild(i).gameObject);
            }
            
            //Debug.Log("search skills has finished  :" + www.text);
            var size = int.Parse(www.text.Split('\t')[1]);
            for (var i = 2; i < size+2 ; i++)
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
            Debug.Log("search users has failed, error : "+ www.text);
        }
    }
    
    IEnumerator RegexUsers(GameObject resultPanel, string inputField)
    {
        WWWForm form = new WWWForm();
        form.AddField("regex",inputField);
        form.AddField("table","user");
        form.AddField("field","firstName");
        
        WWW www = new WWW("http://localhost/sql/search.php", form);
        yield return www;
        if(www.text[0] =='0')
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
                //Debug.Log( "welcome "+ www.text.Split('\t')[i]);
                
                var userInfo = www.text.Split('\t')[i];

                var go = Instantiate(button, resultPanel.transform);
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

    IEnumerator RegexSkillGroup(GameObject resultPanel, string inputField)
    {
        WWWForm form = new WWWForm();
        form.AddField("regex",inputField);
        form.AddField("table","skillgroup");
        form.AddField("field","skillGroupName");
        
        WWW www = new WWW("http://localhost/sql/search.php", form);
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
            Debug.Log("search skillGroup has failed, error : "+ www.text);
        }
    }

    IEnumerator RegexUserGroup(GameObject resultPanel, string inputField)
    {
        WWWForm form = new WWWForm();
        form.AddField("regex",inputField);
        form.AddField("table","usergroup");
        form.AddField("field","userGroupName");
        
        WWW www = new WWW("http://localhost/sql/search.php", form);
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
                var userGroupInfo = www.text.Split('\t')[i];

                var go = Instantiate(button, resultPanel.transform);
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

    IEnumerator RegexStudent(GameObject resultPanel, string inputField)
    {
        WWWForm form = new WWWForm();
        form.AddField("regex",inputField);
        form.AddField("table","user");
        form.AddField("field","firstName");
        form.AddField("type","student");
        
        WWW www = new WWW("http://localhost/sql/search.php", form);
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
                var studentInfo = www.text.Split('\t')[i];

                var go = Instantiate(button, resultPanel.transform);
                go.GetComponentInChildren<Text>().text = studentInfo;
                var studentPrefab= new Tuple<string, GameObject>(studentInfo, go);
                searchResultsList.Add(studentPrefab);
            }
        }
        else
        {
            Debug.Log("search userGroup has failed, error : "+ www.text);
        }
    }
    
    IEnumerator RegexTeacher(GameObject resultPanel, string inputField)
    {
        WWWForm form = new WWWForm();
        form.AddField("regex",inputField);
        form.AddField("table","user");
        form.AddField("field","firstName");
        form.AddField("type","teacher");
        
        WWW www = new WWW("http://localhost/sql/search.php", form);
        yield return www;
        if (www.text[0] == '0')
        {
            var searchResultsList = new List<Tuple<string, GameObject>>();
            searchResultsList.Clear();
            for (var i = 0; i < listOfSupervisors.transform.childCount; i++)
            {
                Destroy(listOfSupervisors.transform.GetChild(i).gameObject);
            }
            
            var size = int.Parse(www.text.Split('\t')[1]);
            for (var i = 2; i < size+2 ; i++)
            {
                var teacherInfo = www.text.Split('\t')[i];

                var go = Instantiate(button, listOfSupervisors.transform);
                go.GetComponentInChildren<Text>().text = teacherInfo;
                var teacherPrefab= new Tuple<string, GameObject>(teacherInfo, go);
                searchResultsList.Add(teacherPrefab);
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
