using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [SerializeField] private GameObject addSkillPanel;
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
    [SerializeField] private GameObject searchResultPanelSkillGroup;
    [SerializeField] private GameObject searchResultPanelUserGroup;
    
    
    
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
    //private DataBaseManager dataBase;
    [SerializeField] private GameObject supervisorPrefab;
    
    //The list of supervisors
    [Header("LISTS TO INSTANTIATE THE OBJECTS IN")]
    [Space(30)]
    [SerializeField] private GameObject listOfSupervisors;
    
    [Header("OTHERS")]
    [Space(30)]
    [SerializeField] private TMP_Text userConnectedText;
    [SerializeField] private TMP_Text errorText;


    void Awake()
    {
        userConnectedText.text = "Connected as admin : " + DataBaseManager.UserName;
    }
    public void BySkillButton()
    {
        adminPanel.SetActive(false);
        addSkillPanel.SetActive(true);
        
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
        DataBaseManager.LogOut();
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
        int counter = 0;
        var searchResultPanel = searchResultPanelUserGroup;
        for (var i = 0; i < searchResultPanel.transform.childCount; i++)
        {
            if (searchResultPanel.transform.GetChild(i).GetComponent<Image>().color == Color.red)
            {
                var text = searchResultPanel.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text;
                var userGroupName = text.Split(' ')[0];
                DataBaseManager.UserGroupNameToEdit = userGroupName;
                counter++;
            }
        }

        if (counter ==1 )
        {
            editUserGroupPanel.SetActive(false);
            modifyUserGroupPanel.SetActive(true);
        }
        else
        {
            errorText.text = "You need to chose (one) UserGroup";
            errorText.transform.parent.gameObject.SetActive(true);
        }
       

    }
    public void ModifySkillGroup()
    {
        int counter = 0;
        var searchResultPanel = searchResultPanelSkillGroup;
        for (var i = 0; i < searchResultPanel.transform.childCount; i++)
        {
            if (searchResultPanel.transform.GetChild(i).GetComponent<Image>().color == Color.red)
            {
                var text = searchResultPanel.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text;
                
                var skillGroupName = text;
                //var skillGroupName = text.Split(' ')[0];
                DataBaseManager.SkillGroupNameToEdit = skillGroupName;
                counter++;
            }
        }
        
        if (counter ==1 )
        {
            editSkillGroupPanel.SetActive(false);
            modifySkillGroupPanel.SetActive(true);
        }
        else
        {
            errorText.text = "You need to chose (one) SkillGroup";
            errorText.transform.parent.gameObject.SetActive(true);
        }
        
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
        
        addSkillPanel.SetActive(false);
    }
    
}
