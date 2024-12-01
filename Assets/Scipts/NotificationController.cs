using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; // For HTTP requests

#if UNITY_ANDROID
using Unity.Notifications.Android;
using UnityEngine.Android;
#endif

#if UNITY_IOS
using Unity.Notifications.iOS;
#endif

public class NotificationController : MonoBehaviour
{
    [SerializeField] AndroidNotifications androidNotifications;
    [SerializeField] iOSNotifications iosNotifications;
    [SerializeField] private string favoritesEndpoint = "http://127.0.0.1:13756/favorites";

    string username = PlayerPrefs.GetString("Username");

    void Start()
    {
#if UNITY_ANDROID
        androidNotifications.RequestAuthorization();
        androidNotifications.RegisterNotificationChannel();
#endif

#if UNITY_IOS
        StartCoroutine(iosNotifications.RequestAuthorization());
#endif

        // Start fetching events periodically
        StartCoroutine(FetchEventsPeriodically());
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus == false)
        {
#if UNITY_ANDROID
            AndroidNotificationCenter.CancelAllNotifications();
            androidNotifications.SendNotification("Event Trigger", "An event you followed is happening soon!", 2);
#endif

#if UNITY_IOS
            iOSNotificationCenter.RemoveAllScheduledNotifications();
            iosNotifications.SendNotification("Event Trigger", "An event you followed is happening soon!", "Check it out!", 2);
#endif
        }
    }

    // Periodically fetch events from the backend
    IEnumerator FetchEventsPeriodically()
    {
        while (true) // Infinite loop to keep fetching events
        {
            string url = $"{favoritesEndpoint}?username={username}";
            UnityWebRequest request = UnityWebRequest.Get(url);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                EventData[] events = JsonHelper.FromJson<EventData>(jsonResponse);

                foreach (var eventData in events)
                {
                    ScheduleNotification(eventData.eventName, eventData.eventTime);
                }
            }
            else
            {
                Debug.LogError("Failed to fetch notifications: " + request.error);
            }

            yield return new WaitForSeconds(60); // Poll every minute
        }
    }


    // Schedule a notification for an event
    void ScheduleNotification(string eventName, string eventTime)
    {
        string notificationBody = $"The event '{eventName}' is starting soon!";

#if UNITY_ANDROID
        androidNotifications.SendNotification(eventName, notificationBody, 1);
#endif

#if UNITY_IOS
        iosNotifications.SendNotification(eventName, notificationBody, "", 1);
#endif
    }

}



/*   
 
 // Process the event data and schedule notifications
void ProcessEvents(string jsonResponse)
    {
        EventData[] events = JsonHelper.FromJson<EventData>(jsonResponse);

        foreach (EventData eventData in events)
        {
#if UNITY_ANDROID
            androidNotifications.SendNotification(
                eventData.eventName,
                $"Event '{eventData.eventName}' is starting soon!",
                1 // Trigger in 1 hour
            );
#endif

#if UNITY_IOS
            iosNotifications.SendNotification(
                eventData.eventName, 
                $"Event '{eventData.eventName}' is starting soon!", 
                "", 
                1 // Trigger in 1 hour
            );
#endif
        }
    }
}

 *   
 *   IEnumerator FetchEventsPeriodically()
    {
        while (true) // Infinite loop to keep fetching events
        {
            using (UnityWebRequest request = UnityWebRequest.Get(eventEndpoint))
            {
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    string jsonResponse = request.downloadHandler.text;
                    ProcessEvents(jsonResponse);
                }
                else
                {
                    Debug.LogError($"Error fetching events: {request.error}");
                }
            }
            yield return new WaitForSeconds(300); // Check every 5 minutes
        }
    }*/