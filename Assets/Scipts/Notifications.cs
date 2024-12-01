using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

public class Notifications : MonoBehaviour
{
    void Start()
    {
        // Create a notification channel
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        // Schedule a notification
        var notification = new AndroidNotification()
        {
            Title = "Event Reminder",
            Text = "An event you favorited starts in 1 hour!",
            SmallIcon = "default",
            FireTime = System.DateTime.Now.AddMinutes(60), // Notification after 1 hour
        };

        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }
}

/*

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

public class Notifications : MonoBehaviour
{


void Start()
{
    var channel = new AndroidNotificationChannel
    {
        Id = "channel_id",
        Name = "Default Channel",
        Importance = Importance.Default,
        Description = "Notifications for favorite events"
    };
    AndroidNotificationCenter.RegisterNotificationChannel(channel);
}


    public void NotificationScheduler(string eventName, string eventDateTime)
    {
        // Parse eventDateTime into a DateTime object
        DateTime eventTime = DateTime.Parse(eventDateTime);
        DateTime oneHourBefore = eventTime.AddHours(-1);
        TimeSpan delay = oneHourBefore - DateTime.Now;

        if (delay.TotalSeconds > 0) // Ensure the event is in the future
        {
            var notification = new AndroidNotification
            {
                Title = "Upcoming Event!",
                Text = $"{eventName} starts in one hour!",
                SmallIcon = "icon_small",
                LargeIcon = "icon_large",
                FireTime = DateTime.Now.Add(delay)
            };

            AndroidNotificationCenter.SendNotification(notification, "channel_id");
            Debug.Log($"Notification scheduled for: {eventName} at {oneHourBefore}");
        }
        else
        {
            Debug.LogWarning("Event has already started or is too close to notify.");
        }
    }
}





 */