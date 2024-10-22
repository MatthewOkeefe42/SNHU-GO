using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using TMPro;

public class EventList : MonoBehaviour
{
    [SerializeField] private string eventEndpoint = "http://127.0.0.1:13756/events"; // URI

    [SerializeField] public TextMeshProUGUI eventNameText;
    [SerializeField] public TextMeshProUGUI eventLocationText;
    [SerializeField] public TextMeshProUGUI eventBioText;
    [SerializeField] public TextMeshProUGUI eventTimeText;
    [SerializeField] public RawImage eventImage;

    [System.Serializable]
    public class EventData
    {
        public int eventId; // Event ID, corresponds to eventId field in MongoDB
        public string eventName; // Event name, corresponds to eventName field in MongoDB
        public string eventBio; // Event bio or description, corresponds to eventBio field in MongoDB
        public string eventLocation; // Event location as a string, corresponds to eventLocation field in MongoDB
        public string eventTime; // Event time as a string, corresponds to eventTime field in MongoDB
        public string eventImageUrl;
    }

    void Start()
    {
        StartCoroutine(GetEvents());        // Start fetching events when the scene starts
    }

    public IEnumerator GetEvents()
    {
        UnityWebRequest request = UnityWebRequest.Get(eventEndpoint);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error fetching events: " + request.error);
        }
        else
        {
            // Parse the JSON response
            EventData[] events = JsonHelper.FromJson<EventData>(request.downloadHandler.text);

            foreach (EventData eventData in events)
            {
                eventNameText.text = eventData.eventName;
                eventLocationText.text = eventData.eventLocation;
                eventBioText.text = eventData.eventBio;
                eventTimeText.text = eventData.eventTime;

                // Start downloading the event image
                StartCoroutine(LoadImage(eventData.eventImageUrl));
            }
        }
    }

    private IEnumerator LoadImage(string imageUrl)
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