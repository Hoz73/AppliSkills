using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private TMP_InputField nameSkillGroupInputField;
    [SerializeField] private TMP_InputField nameUserGroupInputField;
    
    //Edit panels
    [Header("EDIT PANELS")]
    [Space(30)]
    [SerializeField] private GameObject editSkillGroupPanel;
    [SerializeField] private GameObject editUserGroupPanel;
    [SerializeField] private TMP_InputField searchInputFieldEditSkillGroup;
    [SerializeField] private TMP_InputField searchInputFieldEditUserGroup;
    
    
    // Modify panels
    [Header("Modify PANELS")]
    [Space(30)]
    [SerializeField] private GameObject modifySkillGroupPanel;
    [SerializeField] private GameObject modifyUserGroupPanel;
    [SerializeField] private TMP_InputField newNameUserGroupInputField;
    [SerializeField] private TMP_InputField newNameSkillGroupInputField;
    [SerializeField] private TMP_InputField searchInputFieldModifyAddStudent;
    [SerializeField] private TMP_InputField searchInputFieldModifyDeleteStudent;
    
    //instances
    [Header("INSTANCES")]
    [Space(30)]
    private DataBaseManager dataBase;
    [SerializeField] private GameObject supervisorPrefab;
    
    //The list of supervisors
    [Header("LISTS TO INSTANTIATE THE OBJECTS IN")]
    [Space(30)]
    [SerializeField] private GameObject listOfSupervisors;
    
    

    void Awake()
    {
        dataBase = new DataBaseManager();
    }
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
    
    public void Disconnect()
    {
        SceneManager.LoadScene("LogIn");
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
    
    public void ModifyUserGroup()
    {
        editUserGroupPanel.SetActive(false);
        modifyUserGroupPanel.SetActive(true);
    }
    
    public void ModifySkillGroup()
    {
        editSkillGroupPanel.SetActive(false);
        modifySkillGroupPanel.SetActive(true);
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
        
        modifySkillGroupPanel.SetActive(false);
        modifyUserGroupPanel.SetActive(false);
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

    public void CreateSkillGroup() //TODO call the function (CreateSkillGroup) from the database
    {

        Tuple<int, string, string> s = Supervisor();
        dataBase.CreateSkillGroup(s.Item1,"tableName");
    }
    
}
