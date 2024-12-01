using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
    [SerializeField] private string profileEndpoint = "http://127.0.0.1:13756/account/profiles"; // URI
    [SerializeField] private TextMeshProUGUI alertText;
    [SerializeField] private TextMeshProUGUI usernameText;
    [SerializeField] private TextMeshProUGUI profileText;
    [SerializeField] private TMP_InputField profileNameInputField;
    [SerializeField] private TMP_Dropdown pronounsDropdown;
    [SerializeField] private TMP_Dropdown privacyDropdown;
    [SerializeField] private Button createAvatarButton;
    [SerializeField] private Button applyButton;

    private void Start()
    {   // Bind button events
        applyButton.onClick.AddListener(OnApplyClick);
        createAvatarButton.onClick.AddListener(OnCreateAvatarClicked);
        string username = PlayerPrefs.GetString("Username", "Guest"); // "Guest" as a default fallback
        usernameText.text = username;
    }

    public void OnCreateAvatarClicked()
    {
        SceneManager.LoadScene("CharacterCreation");
    }
    
    public void OnApplyClick()
    {   // Triggered when the apply button is clicked
        alertText.text = "Applying changes to profile...";
        StartCoroutine(TryApply());
    }

    private IEnumerator TryApply()
    {   // Coroutine to apply profile changes

        string username = PlayerPrefs.GetString("Username", "Guest");
        string profileName = profileNameInputField.text;
        string pronouns = pronounsDropdown.options[pronounsDropdown.value].text;
        string privacySetting = privacyDropdown.options[privacyDropdown.value].text;

        if (string.IsNullOrEmpty(profileName))
        {
            alertText.text = "Profile name cannot be empty.";
            yield break;
        }

        // Prepare form for server
        WWWForm form = new WWWForm();
        form.AddField("Username", username);
        form.AddField("ProfileName", profileName);
        form.AddField("Pronouns", pronouns);
        form.AddField("Privacy", privacySetting);

        // Send profile data to the server
        UnityWebRequest request = UnityWebRequest.Post(profileEndpoint, form);
        yield return request.SendWebRequest();

        // Check server response
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            alertText.text = "Error: " + request.error;
        }
        else
        {
            profileText.text = ("Profile Name: " + profileName);
            alertText.text = "Profile updated!";
        }
    }
}