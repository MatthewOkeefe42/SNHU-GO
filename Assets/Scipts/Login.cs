using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [SerializeField] private string loginEndpoint = "http://127.0.0.1:13756/account/login"; // URI
    [SerializeField] private string createEndpoint = "http://127.0.0.1:13756/account/create"; // URI

    [SerializeField] private TextMeshProUGUI alertText;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button createButton;
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;

    public void OnLoginClick()
    {
        alertText.text = "Signing in...";
        ActivateButtons(false); // Prevent further button clicks after sign in.

        StartCoroutine(TryLogin()); 
    }

    public void OnCreateClick()
    {
        alertText.text = "Creating account...";
        ActivateButtons(false); // Prevent further button clicks after sign in.

        StartCoroutine(TryCreate());
    }


    private IEnumerator TryLogin()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        if(username.Length < 3 || username.Length > 24)
        {
            alertText.text = "Invalid username";
            ActivateButtons(true);
            yield break;
        }

        if (password.Length < 3 || password.Length > 24)
        {
            alertText.text = "Invalid password";
            ActivateButtons(true);
            yield break;
        }

        WWWForm form = new WWWForm(); // Information that goes in body of request
        form.AddField("rUsername", username);
        form.AddField("rPassword", password);


        UnityWebRequest request = UnityWebRequest.Post(loginEndpoint, form); // Declare web request in url
        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while (!handler.isDone)
        {
            startTime += Time.deltaTime;

            if (startTime > 10.0f) // If it takes longer than 10 seconds, break from loop
            {
                break;
            }

            yield return null;
        }

        if (request.result == UnityWebRequest.Result.Success)

            //Debug.Log(request.downloadHandler.text);
            //LoginResponse response = JsonUtility.FromJson<LoginResponse>(request.downloadHandler.text);

        {   // Login Success?
            if (request.downloadHandler.text != "Invalid credentials")
            {
                ActivateButtons(false);
                GameAccount returnedAccount = JsonUtility.FromJson<GameAccount>(request.downloadHandler.text);
                alertText.text = "Welcome " + returnedAccount.username + ((returnedAccount.adminFlag == 1) ? " Admin" : "");
            }
            else
            {   
                alertText.text = "Invalid credentials";
                ActivateButtons(true);
            }
        }
        else 
        {
            alertText.text = "Error connecting to the server...";
            ActivateButtons(true);
        }

        Debug.Log($"{username}:{password}");

        yield return null; // Pauses the execution of the coroutine and resumes it on the next frame.
    }

    private IEnumerator TryCreate()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        // Username syntax requirements
        if (username.Length < 3 || username.Length > 24)
        {
            alertText.text = "Invalid username";
            ActivateButtons(true);
            yield break;
        }
        // Password syntax requirements
        if (password.Length < 3 || password.Length > 24)
        {
            alertText.text = "Invalid password";
            ActivateButtons(true);
            yield break;
        }

        WWWForm form = new WWWForm(); // Information that goes in body of request
        form.AddField("rUsername", username);
        form.AddField("rPassword", password);


        UnityWebRequest request = UnityWebRequest.Post(createEndpoint, form); // Declare web request in url
        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while (!handler.isDone)
        {
            startTime += Time.deltaTime;

            if (startTime > 10.0f) // If it takes longer than 10 seconds, break from loop
            {
                break;
            }
            yield return null;
        }

        if (request.result == UnityWebRequest.Result.Success)
        {
            if (request.downloadHandler.text != "Invalid credentials" && request.downloadHandler.text != "Username is already taken") // Login Success?
            {
                GameAccount returnedAccount = JsonUtility.FromJson<GameAccount>(request.downloadHandler.text);
                alertText.text = "Account created";
            }
            else
            {
                alertText.text = "Username is already taken";
            }
        }
        else
        {
            alertText.text = "Error connecting to the server...";
        }
        ActivateButtons(true);

        yield return null; // Pauses the execution of the coroutine and resumes it on the next frame.
    }

    private void ActivateButtons(bool toggle)
    {
        loginButton.interactable = toggle;
        createButton.interactable = toggle;
    }
}