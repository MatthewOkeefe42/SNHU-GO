using Mapbox.Unity.Map;
using Mapbox.Utils;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class EventManager : MonoBehaviour
{
    [SerializeField] private string eventEndpoint = "http://127.0.0.1:13756/events"; // URI
    [SerializeField] private AbstractMap map; // Reference to your Mapbox map component
    [SerializeField] private GameObject markerPrefab; // Prefab for the event marker, should be assigned in the Inspector
    [SerializeField] private float markerScale = 0.05f; // Scale factor for markers


    private const int maxEvents = 10; // Limit number of events to 10
    public int MAX_DISTANCE = 70;

    void Start()
    {
        // Ensure markers spawn only after map is initialized
        map.OnInitialized += () => StartCoroutine(DisplayEventOnMap());
    }

    private IEnumerator DisplayEventOnMap()
    {
        UnityWebRequest request = UnityWebRequest.Get(eventEndpoint);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error fetching events: " + request.error);
        }
        else
        {
            EventData[] events = JsonHelper.FromJson<EventData>(request.downloadHandler.text);
            int eventCount = Mathf.Min(events.Length, maxEvents);

            for (int i = 0; i < eventCount; i++)
            {
                EventData eventData = events[i];
                Vector2d location = new Vector2d(eventData.xCoord, eventData.yCoord);

                Debug.Log($"Spawning marker for event {eventData.eventName} at lat: {eventData.xCoord}, lon: {eventData.yCoord}");
                SpawnEventMarker(location);
            }
        }
    }

    private void SpawnEventMarker(Vector2d location)
    {
        // Convert geographic coordinates to Unity world position
        Vector3 worldPosition = map.GeoToWorldPosition(location, true);

        // Log the world position to verify accuracy
        Debug.Log($"Calculated world position for marker: {worldPosition}");

        // Instantiate and set marker's position
        GameObject marker = Instantiate(markerPrefab, worldPosition, Quaternion.identity);
        marker.transform.localScale = new Vector3(markerScale, markerScale, markerScale);
        marker.transform.SetParent(this.transform, false);
    }
}