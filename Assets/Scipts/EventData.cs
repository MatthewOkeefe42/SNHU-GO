using System;

[System.Serializable]
public class EventData
{
    public string _id; // Event ID, corresponds to eventId field in MongoDB
    public string eventAdmin; // Event ID, corresponds to eventId field in MongoDB
    public string eventName; // Event name, corresponds to eventName field in MongoDB
    public string eventBio; // Event bio or description, corresponds to eventBio field in MongoDB
    public string eventLocation; // Event location as a string, corresponds to eventLocation field in MongoDB
    public string eventTime; // Event time as a string, corresponds to eventTime field in MongoDB
    public string eventDate;
    public string eventType;        // Club meeting or campus event
    public double xCoord;
    public double yCoord;
    public string eventImageUrl;
}