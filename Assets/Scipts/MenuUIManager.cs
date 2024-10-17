using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private GameObject eventPanelUserInRange;
    [SerializeField] private GameObject eventPanelUserNotInRange;
    bool isUiPanelActive;
    int tempEvent;
    [SerializeField] private EventManager eventManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayStartEventPanel(int eventID)
    {
        if (isUiPanelActive == false)
        {
            tempEvent = eventID;
            eventPanelUserInRange.SetActive(true);
            isUiPanelActive = true;
        }
    }

    public void OnJoinButtonClick()
    {
        Debug.Log("ITWORKED");
        eventManager.ActivateEvent(tempEvent);
    }

    public void DisplayUserNotInRange()
    {
        if (isUiPanelActive == false)
        {
            eventPanelUserNotInRange.SetActive(true);
            isUiPanelActive = true;
        }
    }

    public void CloseButtonClick()
    {
        eventPanelUserInRange.SetActive(false);
        eventPanelUserNotInRange.SetActive(false);
        isUiPanelActive = false;
    }

    public void OnEventsListClick()
    {
        SceneManager.LoadScene("EventsList");
    }
}