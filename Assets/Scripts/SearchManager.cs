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
    public DataBaseManager dataBase;



    public void RegexSearchFromInputField()
    {
        List<DataBaseManager.User> results = dataBase.RegexSearch(searchInputField.text);
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

}
