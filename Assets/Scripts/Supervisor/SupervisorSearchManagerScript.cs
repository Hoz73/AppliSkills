using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class SupervisorSearchManagerScript : MonoBehaviour
{
    [SerializeField] private TMP_InputField searchInputFieldUserGroup;
    [SerializeField] private TMP_InputField searchInputFieldSkill;

    [SerializeField] private GameObject userGroupSearchResultsPanel;
    [SerializeField] private GameObject skillSearchResultsPanel;

    [SerializeField] private GameObject button;

    [SerializeField] GameObject BrowseBySkillBySkillGroupPanel;
    [SerializeField] GameObject BrowseByUserByUserGroupPanel;
    [SerializeField] GameObject[] panels;


    public void OnEventSearchInputFieldSkillInputField()
    {
        StartCoroutine(RegexSkillInputField(skillSearchResultsPanel, searchInputFieldSkill.text));
    }

    public void OnEventSearchInputFieldSkillGroupInputField()
    {
        StartCoroutine(RegexSkillGroupInputField(skillSearchResultsPanel, searchInputFieldSkill.text));
    }

    public void OnEventSearchInputFieldUserGroupInputField()
    {
        StartCoroutine(RegexUserGroup(skillSearchResultsPanel, searchInputFieldSkill.text));
    }

    IEnumerator RegexSkillGroupInputField(GameObject resultPanel, string inputField)
    {
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
            Debug.Log("search skills has failed, error : " + www.text);
        }
    }


    IEnumerator RegexSkillInputField(GameObject resultPanel, string inputField)
    {
        WWWForm form = new WWWForm();
        form.AddField("regex", inputField);
        form.AddField("table", "skill");
        form.AddField("field", "skillName");

        WWW www = new WWW("http://localhost/sql/search.php", form);
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
            Debug.Log("search skills has failed, error : " + www.text);
        }
    }


    IEnumerator RegexUserGroup(GameObject resultPanel, string inputField)
    {
        WWWForm form = new WWWForm();
        form.AddField("regex", inputField);
        form.AddField("table", "usergroup");
        form.AddField("field", "userGroupName");

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
            for (var i = 2; i < size + 2; i++)
            {
                var userGroupInfo = www.text.Split('\t')[i];

                var go = Instantiate(button, resultPanel.transform);
                go.GetComponentInChildren<Text>().text = userGroupInfo;
                var userGroupPrefab = new Tuple<string, GameObject>(userGroupInfo, go);
                searchResultsList.Add(userGroupPrefab);
            }
        }
        else
        {
            Debug.Log("search userGroup has failed, error : " + www.text);
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
                DataBaseManager.ChosenSkill = skillGroup;
                counter++;
            }
        }
        if (counter != 1)
        {
            Debug.Log("invalid action");
            DataBaseManager.ChosenSkill = null;
        }

        //turn off every panel
        foreach (var p in panels)
        {
            p.SetActive(false);
        }

        BrowseBySkillBySkillGroupPanel.SetActive(true);
    }

    public void PollUserGroup(GameObject panel)
    {
        var searchResultPanel = panel;
        int counter = 0;
        for (var i = 0; i < searchResultPanel.transform.childCount; i++)
        {
            if (searchResultPanel.transform.GetChild(i).GetComponent<Image>().color == Color.red)
            {
                var text = searchResultPanel.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text;
                var userGroup = text.Split(' ')[0];
                DataBaseManager.ChosenUserGroup = userGroup;
                counter++;
            }
        }
        if (counter != 1)
        {
            Debug.Log("invalid action");
            DataBaseManager.ChosenSkill = null;
        }

        //turn off every panel
        foreach (var p in panels)
        {
            p.SetActive(false);
        }

        BrowseByUserByUserGroupPanel.SetActive(true);


    }
}
 