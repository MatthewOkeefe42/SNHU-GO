using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;

public class LoginManager : MonoBehaviour // Script for the Login Manager
{
    // UI elements
    private TextField usernameField;
    private TextField passwordField;
    private Button loginButton;
    private Button registerButton;

    void OnEnable() // Called when the script is enabled
    {
        // Get the root of the UI
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Get the components of the UI
        usernameField = root.Q<TextField>("UsernameField");
        passwordField = root.Q<TextField>("PasswordField");
        loginButton = root.Q<Button>("LoginButton");
        registerButton = root.Q<Button>("RegisterButton");

        // Add listeners to the buttons
        loginButton.clicked += OnLoginClicked;
        registerButton.clicked += OnRegisterClicked;
    }

    // Method called when login button is clicked
    private void OnLoginClicked()
    {
        string username = usernameField.text;
        string password = passwordField.text;

        // Call AuthenticateUser method to check credentials
        if (AuthenticateUser(username, password))
        {
            UnityEngine.Debug.Log("Login successful");
            // You can navigate to another scene or perform further actions here
        }
        else
        {
            UnityEngine.Debug.Log("Login failed. Please check your username and password.");
        }
    }

    // Method called when register button is clicked
    private void OnRegisterClicked()
    {
        UnityEngine.Debug.Log("Navigating to Register Screen");
        // Navigate to the Register screen or show a registration form
    }

    // Authenticate user (replace with real authentication logic)
    private bool AuthenticateUser(string username, string password)
    {
        // In a real-world scenario, replace this with server-side authentication logic
        return username == "username" && password == "password";
    }
}