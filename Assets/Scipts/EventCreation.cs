using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class EventCreation : MonoBehaviour
{
    [SerializeField] private string eventEndpoint = "http://127.0.0.1:13756/events"; // URI

    [SerializeField] public TextMeshProUGUI adminUsername;
    [SerializeField] private TextMeshProUGUI alertText;

    [SerializeField] public TMP_InputField eventNameText;
    [SerializeField] public TMP_InputField eventBioText;
    [SerializeField] public TMP_InputField eventImageURL;
    [SerializeField] public TMP_InputField eventTimeText;
    [SerializeField] public TMP_InputField eventDateText;
    [SerializeField] public TMP_InputField eventLocationText;
    [SerializeField] public TMP_InputField eventXcoordText;
    [SerializeField] public TMP_InputField eventYcoordText;

    [SerializeField] private Button registerButton;


    // Start is called before the first frame update
    void Start()
    {
        registerButton.onClick.AddListener(OnApplyClick);
        string admin = PlayerPrefs.GetString("Username"); // "Guest" as a default fallback
        adminUsername.text = admin;
    }

    public void OnApplyClick()
    {   // Triggered when the apply button is clicked
        alertText.text = "Applying changes to profile...";
        StartCoroutine(RegisterEvent());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TryApply()
    {
        alertText.text = "Registering event...";
        StartCoroutine(RegisterEvent());
    }

   private IEnumerator RegisterEvent()
    {
        string admin = adminUsername.text;
        string eventName = eventNameText.text;
        string eventBio = eventBioText.text;
        string eventImage = eventImageURL.text;
        string eventTime = eventTimeText.text;
        string eventDate = eventDateText.text;
        string eventLocation = eventLocationText.text;
        string eventXcoord = eventXcoordText.text;
        string eventYcoord = eventYcoordText.text;

        if (string.IsNullOrEmpty(eventName))
        {
            alertText.text = "Event name cannot be empty.";
            yield break;
        }

        // Prepare form for server
        WWWForm form = new WWWForm();
        form.AddField("eventAdmin", admin);
        form.AddField("eventName", eventName);
        form.AddField("eventBio", eventBio);
        form.AddField("eventImageUrl", eventImage);
        form.AddField("eventTime", eventTime);
        form.AddField("eventDate", eventDate);
        form.AddField("eventLocation", eventLocation);
        form.AddField("xCoord", eventXcoord);
        form.AddField("yCoord", eventYcoord);


        // Send profile data to the server
        UnityWebRequest request = UnityWebRequest.Post(eventEndpoint, form);
        yield return request.SendWebRequest();

        // Check server response
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            alertText.text = "Error: " + request.error;
        }
        else
        {
            alertText.text = "Profile registered!";
        }
    }
}