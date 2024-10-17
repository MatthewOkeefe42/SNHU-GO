using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Mapbox.Json;
using TMPro;
using UnityEngine.UI;
using static Unity.VisualScripting.FlowStateWidget;

public class EventList : MonoBehaviour
{
    public GameObject eventPrefab;    // Reference to the Event Prefab
    public Transform contentPanel;    // Content Panel inside Scroll View
    private const int maxEvents = 10; // Limit the number of events to 10

    // URL of the backend server (change this if hosted on a different server/port)
    [SerializeField] private string eventEndpoint = "http://127.0.0.1:13756/Events"; // URI
    [SerializeField] public TextMeshProUGUI eventNameText;
    [SerializeField] public TextMeshProUGUI eventLocationText;
    [SerializeField] public TextMeshProUGUI eventBioText;
   // [SerializeField] public TextMeshProUGUI eventxCoordText;
   // [SerializeField] public TextMeshProUGUI eventyCoordText;
    [SerializeField] public TextMeshProUGUI eventTimeText;
    [SerializeField] public RawImage eventImage;

    public class EventData
    {
        //public int eventId; // Event ID, corresponds to eventId field in MongoDB
        public string eventName; // Event name, corresponds to eventName field in MongoDB
        public string eventBio; // Event bio or description, corresponds to eventBio field in MongoDB
        public string eventLocation; // Event location as a string, corresponds to eventLocation field in MongoDB
        public string xCoord; // Event coordinates latitude corresponds to xCoord field in MongoDB
        public string yCoord; // Event coordinates longitude corresponds to yCoord field in MongoDB
        public string eventTime; // Event time as a string, corresponds to eventTime field in MongoDB
        public string eventImageUrl;
    }

    // Start is called before the first frame update
    void Start() // Call the GetEvents() coroutine when the script starts to fetch the events
    { 
        StartCoroutine(GetEvents());
    }

    // Coroutine for fetching events from the backend
    IEnumerator GetEvents()
    {
        UnityWebRequest request = UnityWebRequest.Get(eventEndpoint);   // Create an HTTP GET request to the server
        yield return request.SendWebRequest();                          // Send the request and wait for the server's response

        // Check if there was an error with the request (e.g., connection or protocol error)
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error fetching events: " + request.error);
        }
        else
        {
            // Deserialize the response into an array of EventData objects
            EventData[] events = JsonHelper.FromJson<EventData>(request.downloadHandler.text);

            // Limit the number of events to 10
            int numberOfEvents = Mathf.Min(events.Length, maxEvents);

            // Populate the UI with events
            for (int i = 0; i < numberOfEvents; i++)
            {
                EventData eventData = events[i];

                // Instantiate a new event UI element (Prefab) and set it as a child of the content panel
                GameObject newEvent = Instantiate(eventPrefab, contentPanel);

                // Set the event's name and location in the UI
                newEvent.transform.Find("EventName").GetComponent<Text>().text = eventData.eventName;
                newEvent.transform.Find("EventLocation").GetComponent<Text>().text = eventData.eventLocation;

                // Start a coroutine to download and display the event's image
                StartCoroutine(LoadImage(eventData.eventImageUrl, newEvent));
            }
        }
    }
    private IEnumerator LoadImage(string imageUrl, GameObject eventObject)
    {
        UnityWebRequest imageRequest = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return imageRequest.SendWebRequest();

        if (imageRequest.result == UnityWebRequest.Result.Success)
                {
                    // Apply the downloaded texture to the RawImage component
                    Texture2D texture = ((DownloadHandlerTexture)imageRequest.downloadHandler).texture;
                    eventImage.texture = texture;
                }
                else
                {
                    Debug.LogError("Error downloading image: " + imageRequest.error);
                }
            }
    }