using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Button colorButton;

    private void Awake()
    {
        colorButton.onClick.AddListener(() =>
        {
            Debug.Log("color button");
        });
    }
}
