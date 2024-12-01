using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FavoritesList : MonoBehaviour
{
    [SerializeField] private string eventEndpoint = "http://127.0.0.1:13756/events"; // URI
    [SerializeField] private string favoritesEndpoint = "http://127.0.0.1:13756/favorites";

    [SerializeField] private GameObject eventPrefab;        // Event prefab to display individual events

    [SerializeField] private Transform contentPanel;        // Content panel inside the scroll view
    [SerializeField] public Button favoriteEvent;
    [SerializeField] public Button eventList;

    [SerializeField] public TextMeshProUGUI eventIDText;
    [SerializeField] public TextMeshProUGUI eventNameText;
    [SerializeField] public TextMeshProUGUI eventLocationText;
    [SerializeField] public TextMeshProUGUI eventBioText;
    [SerializeField] public TextMeshProUGUI eventTimeText;
    [SerializeField] public RawImage eventImage;

    private const int maxEvents = 10; // Limit number of events to 10

    void Start()
    {
        StartCoroutine(GetFavorites());        // Start fetching events when the scene starts
    }

    public void EventListClick()
    {
        SceneManager.LoadScene("EventsList");

        Debug.Log("Clicked GetEvents!");
    }

    public void FavoriteEventClick(string eventId)
    {
        string username = PlayerPrefs.GetString("Username");
        Debug.Log("FavoriteEventClick called with:");
        Debug.Log("Username: " + username);
        Debug.Log("Event ID: " + eventId);

        StartCoroutine(UpdateFavorites(username, eventId));
        Debug.Log("Updating Favorites!");
    }

    public IEnumerator UpdateFavorites(string username, string eventId)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("eventId", eventId);

        Debug.Log("Sending request to: " + favoritesEndpoint);
        Debug.Log("Username: " + username + ", Event ID: " + eventId);

        UnityWebRequest request = UnityWebRequest.Post(favoritesEndpoint, form);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Event added to favorites!");
        }
        else
        {
            Debug.LogError("Error adding event to favorites: " + request.error);
        }
    }

    private IEnumerator GetFavorites()
    {
        string username = PlayerPrefs.GetString("Username");

        UnityWebRequest request = UnityWebRequest.Get(favoritesEndpoint + "?username=" + username);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error fetching favorite events: " + request.error);
        }
        else
        {
            Debug.Log("Response JSON: " + request.downloadHandler.text);

            // Attempt to parse JSON response
            FavoriteEvents favoriteData = JsonUtility.FromJson<FavoriteEvents>(request.downloadHandler.text);

            if (favoriteData.eventIds == null)
            {
                Debug.Log("No favorites available.");
            }
            else
            {
                Debug.Log("Fetched favorites successfully.");
                foreach (string eventId in favoriteData.eventIds)
                {
                    yield return StartCoroutine(GetEventDetails(eventId));
                }
            }
        }
    }

    private IEnumerator GetEventDetails(string eventId)
    {
        UnityWebRequest eventRequest = UnityWebRequest.Get(eventEndpoint + "/" + eventId);
        yield return eventRequest.SendWebRequest();

        if (eventRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error fetching event details: " + eventRequest.error);
        }
        else
        {
            Debug.Log("Working!");

            // Parse the response to get event details
            EventData eventData = JsonUtility.FromJson<EventData>(eventRequest.downloadHandler.text);

            // Instantiate the event UI element and populate it with data
            GameObject favoriteEvent = Instantiate(eventPrefab, contentPanel);
            favoriteEvent.transform.Find("EventName").GetComponent<TextMeshProUGUI>().text = eventData.eventName;
            favoriteEvent.transform.Find("EventLocation").GetComponent<TextMeshProUGUI>().text = eventData.eventLocation;
            favoriteEvent.transform.Find("EventBio").GetComponent<TextMeshProUGUI>().text = eventData.eventBio;
            favoriteEvent.transform.Find("EventDate").GetComponent<TextMeshProUGUI>().text = eventData.eventDate;
            favoriteEvent.transform.Find("EventTime").GetComponent<TextMeshProUGUI>().text = eventData.eventTime;

            // Load and display the event image
            StartCoroutine(LoadImage(eventData.eventImageUrl, favoriteEvent.transform.Find("EventImage").GetComponent<RawImage>()));
        }
    }

    private IEnumerator LoadImage(string imageUrl, RawImage eventImage)
    {
        if (string.IsNullOrEmpty(imageUrl))
        {
            // Debug.LogError("Image URL is null or empty.");
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