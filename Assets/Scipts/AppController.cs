using UnityEngine;
using UnityEngine.SceneManagement;

public class AppController : MonoBehaviour
{
    void Start()
    {
        // Check if the user is already logged in
        bool isLoggedIn = PlayerPrefs.GetInt("IsLoggedIn", 0) == 1;

        if (isLoggedIn)
        {
            // If the user is logged in, load the Main Menu scene
            SceneManager.LoadScene("Location-basedGame");    // Replace "MainMenu" with your home scene name
        }
        else
        {
            // If the user is not logged in, load the Login scene
            SceneManager.LoadScene("LoginScene"); // Replace "LoginScene" with your login scene name
        }
    }
}