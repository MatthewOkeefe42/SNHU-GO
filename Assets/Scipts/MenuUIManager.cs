using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI usernameText;
    [SerializeField] private GameObject eventPanelUserInRange;
    [SerializeField] private GameObject eventPanelUserNotInRange;
    [SerializeField] private EventManager eventManager;
    bool isUiPanelActive;
    int tempEvent;

    // Start is called before the first frame update
    void Start()
    {
        string username = PlayerPrefs.GetString("Username"); // "Guest" as a default fallback
        usernameText.text = username;
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
    }

    public void DisplayUserNotInRange()
    {
        if (isUiPanelActive == false)
        {
            eventPanelUserNotInRange.SetActive(true);
            isUiPanelActive = true;
        }
    }

    public void OnAttendButtonClick()
    {
        Debug.Log("Attended");
        //alertText.text = "Joined event!";
        eventPanelUserInRange.SetActive(false);
        eventPanelUserNotInRange.SetActive(false);
        isUiPanelActive = true;
    }

    public void CloseButtonClick()
    {
        Debug.Log("Closed");
        eventPanelUserInRange.SetActive(false);
        eventPanelUserNotInRange.SetActive(false);
        isUiPanelActive = true;
    }

    public void OnEventsListClick()
    {
        Debug.Log("Clicked");
        SceneManager.LoadScene("EventsList");
    }

    public void OnProfileClick()
    {
        Debug.Log("Clicked");
        SceneManager.LoadScene("ProfileScene");
    }
    public void OnMapButtonClick()
    {
        SceneManager.LoadScene("Location-basedGame");
    }
}