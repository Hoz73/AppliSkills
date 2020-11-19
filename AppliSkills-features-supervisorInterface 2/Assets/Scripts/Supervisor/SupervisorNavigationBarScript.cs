using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupervisorNavigationBarScript : MonoBehaviour
{
    
    [SerializeField] GameObject[] panels;


    public void SupervisorNavigationbarClick(GameObject PanelToActivate)
    {
        //turn off every panel
        foreach (var panel in panels)
        {
            panel.SetActive(false);
        }

       PanelToActivate.SetActive(true);
    }
}
