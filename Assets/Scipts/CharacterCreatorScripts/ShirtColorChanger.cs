using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShirtColorChanger : MonoBehaviour
{
    [Header("Mesh to Change")]
    public SkinnedMeshRenderer ShirtColor;
   
    


    [Header("Material to Cycle Through")]
    public List<Material> options = new List<Material>();

    private int currentOption = 0;
    public void nextOption()
    {
        
        currentOption++;

        if (currentOption >= options.Count)
        {

            currentOption = 0;
        }

        ShirtColor.material = options[currentOption];
     
    }
    public void prevOption()
    {
        currentOption--;
        if (currentOption <= 0)
        {

            currentOption = options.Count- 1;
        }
        ShirtColor.material = options[currentOption];
      
        

    }

}
