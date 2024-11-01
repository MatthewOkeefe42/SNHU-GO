using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;


public class HairChange : MonoBehaviour
{
    [Header("Mesh to Change")]
    public SkinnedMeshRenderer Hair;

    

    
    

    [Header("Material to Cycle Through")]
    public List<Mesh> options = new List<Mesh>();
    


        
    private int currentOption = 0;
    public void nextOption()
    {
        currentOption++;

        if (currentOption >= options.Count)
        {

            currentOption = 0;
        }

        Hair.sharedMesh = options[currentOption];

    }
    public void prevOption()
    {
        currentOption--;
        if (currentOption <= 0)
        {

            currentOption = options.Count - 1;
        }
        Hair.sharedMesh = options[currentOption];



    }

}
