using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using TMPro;

public class EventList : MonoBehaviour
{
    [SerializeField] private string eventEndpoint = "http://127.0.0.1:13756/events"; // URI

    [SerializeField] private GameObject eventPrefab;     // Event prefab to display individual events
    [SerializeField] private Transform contentPanel;     // Content panel inside the scroll view
    [SerializeField] public TextMeshProUGUI eventNameText;
    [SerializeField] public TextMeshProUGUI eventLocationText;
    [SerializeField] public TextMeshProUGUI eventBioText;
    [SerializeField] public TextMeshProUGUI eventTimeText;
    [SerializeField] public RawImage eventImage;

    private const int maxEvents = 10; // Limit number of events to 10

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

            int eventCount = Mathf.Min(events.Length, maxEvents); // Limit to 10 events
            for (int i = 0; i < eventCount; i++)
            {
                EventData eventData = events[i];

                // Instantiate a new event UI element (Prefab)
                GameObject newEvent = Instantiate(eventPrefab, contentPanel);

                // Find and update text elements in the prefab
                newEvent.transform.Find("EventName").GetComponent<TextMeshProUGUI>().text = eventData.eventName;
                newEvent.transform.Find("EventLocation").GetComponent<TextMeshProUGUI>().text = eventData.eventLocation;
                newEvent.transform.Find("EventBio").GetComponent<TextMeshProUGUI>().text = eventData.eventBio;
                newEvent.transform.Find("EventTime").GetComponent<TextMeshProUGUI>().text = eventData.eventTime;

                // Load and apply the event image
                StartCoroutine(LoadImage(eventData.eventImageUrl, newEvent.transform.Find("EventImage").GetComponent<RawImage>()));
            }
        }
    }

    private IEnumerator LoadImage(string imageUrl, RawImage eventImage)
    {
        if (string.IsNullOrEmpty(imageUrl))
        {
            Debug.LogError("Image URL is null or empty.");
            yield break;
        }

        UnityWebRequest imageRequest = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return imageRequest.SendWebRequest();

        if (imageRequest.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)imageRequest.downloadHandler).texture;
            if (texture != null)
            {
                eventImage.texture = texture;  // Apply the downloaded texture to the passed-in RawImage
            }
            else
            {
                Debug.LogError("Downloaded texture is null.");
            }
        }
        else
        {
            Debug.LogError("Error downloading image: " + imageRequest.error);
        }
    }
}