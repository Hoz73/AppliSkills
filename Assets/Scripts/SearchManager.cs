using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class SearchManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField searchInputField;
    [SerializeField] private GameObject searchResultsPanel;
    [SerializeField] private GameObject resultsList;
    private DataBaseManager dataBase;

    void Start()
    {
        dataBase = new DataBaseManager();
    }
    
    public void RegexSearchFromInputFieldUser() //TODO need to be modified (it was a test function)
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
            GameObject go = Instantiate(resultsList, searchResultsPanel.transform);
            Tuple<int, GameObject> idTuples = new Tuple<int, GameObject>(item.ID, go);
            idTuplesList.Add(idTuples);
        }
    }

    public void RegexSearchFromInputFieldSkill()//TODO
    {
        
    }
    
    public void RegexSearchFromInputFieldSkillGroup()//TODO
    {
        
    }
    
    public void RegexSearchFromInputFieldUserGroup()//TODO
    {
        
    }

}
