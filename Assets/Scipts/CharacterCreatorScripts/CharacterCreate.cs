using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
public class CharacterCreate : MonoBehaviour
{
    public GameObject character;

  public void Submit()
    {
        PrefabUtility.SaveAsPrefabAsset(character, "Assets/Prefabs/SavedChar.Prefab");
        
    }
}
