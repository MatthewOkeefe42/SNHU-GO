using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private GameObject eventPanelUserInRange;
    [SerializeField] private GameObject eventPanelUserNotInRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DisplayStartEventPanel()
    {
        eventPanelUserInRange.SetActive(true);
    }
    public void DisplayUserNotInRange()
    {
        eventPanelUserNotInRange.SetActive(true);
    }
}
