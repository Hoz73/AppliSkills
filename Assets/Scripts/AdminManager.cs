using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminManager : MonoBehaviour
{
    //Navigation panels
    [SerializeField] private GameObject adminPanel;
    [SerializeField] private GameObject navigationPanelBySkillGroup;
    [SerializeField] private GameObject navigationPanelByUserGroup;
    
    //Add panels
    [SerializeField] private GameObject addSkillGroupPanel;
    [SerializeField] private GameObject addUserGroupPanel;
    
    //Manager panels
    [SerializeField] private GameObject editSkillGroupPanel;
    [SerializeField] private GameObject editUserGroupPanel;
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
    
    
}
