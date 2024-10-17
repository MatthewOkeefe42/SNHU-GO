using System;
using UnityEngine;

public static class JsonHelper
{
    // A wrapper class to allow Unity's JsonUtility to deserialize arrays
    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }

    // Deserialize a JSON array into an array of objects of type T
    public static T[] FromJson<T>(string json)
    {
        string newJson = "{\"Items\":" + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.Items;
    }

    // Serialize an array of objects of type T into a JSON array
    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T> { Items = array };
        return JsonUtility.ToJson(wrapper);
    }

    // Serialize an array of objects of type T into a JSON array with pretty-print option
    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T> { Items = array };
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }
}
