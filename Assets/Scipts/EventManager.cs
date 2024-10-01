using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public int MAX_DISTANCE = 70;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateEvent(int eventID)
    {
        if (eventID == 1) // Dining Hall
        {
            SceneManager.LoadScene("Dining Hall"); 
        }
        if (eventID == 2) // SNHU Green
        {
            SceneManager.LoadScene("Green Space");
        }
    }
}
