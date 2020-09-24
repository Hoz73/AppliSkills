using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AdminManager : MonoBehaviour
{
    //Navigation panels
    [Header("NAVIGATION PANELS")]
    [SerializeField] private GameObject adminPanel;
    [SerializeField] private GameObject navigationPanelBySkillGroup;
    [SerializeField] private GameObject navigationPanelByUserGroup;
    
    //Add panels
    [Header("ADD PANELS")]
    [Space(30)]
    [SerializeField] private GameObject addSkillGroupPanel;
    [SerializeField] private GameObject addUserGroupPanel;
    [SerializeField] private TMP_InputField NameSkillGroupInputField;
    [SerializeField] private TMP_InputField NameUserGroupInputField;
    
    //Edit panels
    [Header("EDIT PANELS")]
    [Space(30)]
    [SerializeField] private GameObject editSkillGroupPanel;
    [SerializeField] private GameObject editUserGroupPanel;
    
    //instances
    [Header("INSTANCES")]
    [Space(30)]
    [SerializeField] private DataBaseManager dataBase;
    [SerializeField] private GameObject supervisorPrefab;
    
    //The list of supervisors
    [Header("LISTS TO INSTANTIATE THE OBJECTS IN")]
    [Space(30)]
    [SerializeField] private GameObject listOfSupervisors;
    
    
    
    public void BySkillGroupButton()
    {
        adminPanel.SetActive(false);
        navigationPanelBySkillGroup.SetActive(true);
        
    }
    
    public void ByUserGroupButton()
    {
        adminPanel.SetActive(false);
        navigationPanelByUserGroup.SetActive(true);
    }
    
    public void Disconnect() //TODO to change the scene
    {
        
    }

    public void AddSkillGroup() 
    {
        navigationPanelBySkillGroup.SetActive(false);
        addSkillGroupPanel.SetActive(true);
    }

    public void ManageSkillGroups() 
    {
        navigationPanelBySkillGroup.SetActive(false);
        editSkillGroupPanel.SetActive(true);
    }

    public void AddUserGroup() 
    {
        navigationPanelByUserGroup.SetActive(false);
        addUserGroupPanel.SetActive(true);
    }

    public void ManageUserGroups()
    {
        navigationPanelByUserGroup.SetActive(false);
        editUserGroupPanel.SetActive(true);
    }

    public void ReturnToAdminPanel()
    {
        adminPanel.SetActive(true);
        
        navigationPanelBySkillGroup.SetActive(false);
        navigationPanelByUserGroup.SetActive(false);
        
        addSkillGroupPanel.SetActive(false);
        addUserGroupPanel.SetActive(false);
        
        editSkillGroupPanel.SetActive(false);
        editUserGroupPanel.SetActive(false);
    }
    
    public Tuple<int, string, string>Supervisor() //TODO not done yet, waiting the new server 
    {
        var sups = dataBase.GetUsers();
        
         
        var listOfSups = new List<Tuple<GameObject, Tuple<int, string, string>>>();

        foreach (var sup in sups)
        {
            Debug.Log("yolo");
            var supervisorButton = Instantiate(supervisorPrefab, listOfSupervisors.transform);
            listOfSups.Add(new Tuple<GameObject, Tuple<int, string, string> >(supervisorButton, sup));
        }
        return null;
    }

    public void CreateSkillGroup(Tuple<int, string, string> supervisor) //TODO call the function (createSkillGroup) from the database
    {
        
    }
    
}
