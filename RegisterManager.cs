using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class RegisterManager : MonoBehaviour
{
    // UI elements
    private TextField usernameField;
    private TextField pronounsField;
    private TextField passwordField;
    private Button createAvatarButton;

    void OnEnable()
    {
        // Get the root of the UI
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Fetch UI components 
        usernameField = root.Q<TextField>("UsernameField");    // For username input
        pronounsField = root.Q<TextField>("PronounsField");    // For pronouns input
        passwordField = root.Q<TextField>("PasswordField");    // For password input
        createAvatarButton = root.Q<Button>("CreateAvatarButton");  // The create avatar button

        // Add listener to the create avatar button
        createAvatarButton.clicked += OnCreateAvatarClicked;
    }

    private void OnCreateAvatarClicked()
    {
        // Retrieve values from the input fields
        string username = usernameField.text;
        string pronouns = pronounsField.text;
        string password = passwordField.text;

        // Validate input 
        if (ValidateRegistration(username, pronouns, password))
        {
         

            // Loads avatar scene
            SceneManager.LoadScene("AvatarCreationScene");
        }
        else
        {
            Debug.Log("Invalid registration details. Please fill out all fields.");
        }
    }

    // Simple validation function to ensure fields are not empty
    private bool ValidateRegistration(string username, string pronouns, string password)
    {
        return !string.IsNullOrEmpty(username) &&
               !string.IsNullOrEmpty(pronouns) &&
               !string.IsNullOrEmpty(password);
    }
}
