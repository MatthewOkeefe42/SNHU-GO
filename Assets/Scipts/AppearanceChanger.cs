using System.Collections.Generic;
using UnityEngine;

public class AppearanceChanger : MonoBehaviour
{
    [Header("Mesh to Change")]
    public SkinnedMeshRenderer Hair;
    public SkinnedMeshRenderer Shirt;
    public SkinnedMeshRenderer Pants;
    public SkinnedMeshRenderer Shoes;

    [Header("Gender Component")]
    public GenderChanger gender; // Reference to the GenderChanger script

    [Header("Material to Cycle Through")]
    public List<Mesh> MaleHairOptions = new List<Mesh>();
    public List<Mesh> FemaleHairOptions = new List<Mesh>();
    public List<Mesh> MaleShirtOptions = new List<Mesh>();
    public List<Mesh> FemaleShirtOptions = new List<Mesh>();
    public List<Mesh> MalePantsOptions = new List<Mesh>();
    public List<Mesh> FemalePantsOptions = new List<Mesh>();
    public List<Mesh> MaleShoesOptions = new List<Mesh>();
    public List<Mesh> FemaleShoesOptions = new List<Mesh>();


    private int currentMaleHairOption = 0;
    private int currentFemaleHairOption = 0;
    private int currentMaleShirtOption = 0;
    private int currentFemaleShirtOption = 0;
    private int currentMalePantsOption = 0;
    private int currentFemalePantsOption = 0;
    private int currentMaleShoesOption = 0;
    private int currentFemaleShoesOption = 0;

    void Start()
    {
        // Try to find the GenderChanger component if it's not assigned
        if (gender == null)
        {
            gender = GetComponent<GenderChanger>();
            if (gender == null)
            {
                Debug.LogError("GenderChanger component is not assigned or found on this GameObject.");
                return;
            }
        }

        // Check if Hair is assigned
        if (Hair == null)
        {
            Debug.LogError("Hair (SkinnedMeshRenderer) is not assigned in the Inspector.");
        }
    }



    //Hair 
    public void nextHairOption()
    {
        if (Hair == null || gender == null) return; // Ensure Hair and gender are assigned

        if (gender.isMale) // Accessing isMale from GenderChanger
        {
            currentMaleHairOption++;

            if (currentMaleHairOption >= MaleHairOptions.Count)
            {
                currentMaleHairOption = 0;
            }

            Hair.sharedMesh = MaleHairOptions[currentMaleHairOption];
        }
        else
        {
            currentFemaleHairOption++;

            if (currentFemaleHairOption >= FemaleHairOptions.Count)
            {
                currentFemaleHairOption = 0;
            }

            Hair.sharedMesh = FemaleHairOptions[currentFemaleHairOption];
        }
    }

    public void prevHairOption()
    {
        if (Hair == null || gender == null) return; // Ensure Hair and gender are assigned

        if (gender.isMale) // Accessing isMale from GenderChanger
        {
            currentMaleHairOption--;

            if (currentMaleHairOption < 0)
            {
                currentMaleHairOption = MaleHairOptions.Count - 1;
            }

            Hair.sharedMesh = MaleHairOptions[currentMaleHairOption];
        }
        else
        {
            currentFemaleHairOption--;

            if (currentFemaleHairOption < 0)
            {
                currentFemaleHairOption = FemaleHairOptions.Count - 1;
            }

            Hair.sharedMesh = FemaleHairOptions[currentFemaleHairOption];
        }
    }


    //Shirt
    public void nextShirtOption()
    {
        if (Shirt == null || gender == null) return; // Ensure Hair and gender are assigned

        if (gender.isMale) // Accessing isMale from GenderChanger
        {
            currentMaleShirtOption++;

            if (currentMaleShirtOption >= MaleShirtOptions.Count)
            {
                currentMaleShirtOption = 0;
            }

            Shirt.sharedMesh = MaleShirtOptions[currentMaleShirtOption];
        }
        else
        {
            currentFemaleShirtOption++;

            if (currentFemaleShirtOption >= FemaleShirtOptions.Count)
            {
                currentFemaleShirtOption = 0;
            }

            Shirt.sharedMesh = FemaleShirtOptions[currentFemaleShirtOption];
        }
    }

    public void prevShirtOption()
    {
        if (Shirt == null || gender == null) return; // Ensure Hair and gender are assigned

        if (gender.isMale) // Accessing isMale from GenderChanger
        {
            currentMaleShirtOption--;

            if (currentMaleShirtOption < 0)
            {
                currentMaleShirtOption = MaleShirtOptions.Count - 1;
            }

            Shirt.sharedMesh = MaleShirtOptions[currentMaleShirtOption];
        }
        else
        {
            currentFemaleShirtOption--;

            if (currentFemaleShirtOption < 0)
            {
                currentFemaleShirtOption = FemaleShirtOptions.Count - 1;
            }

            Shirt.sharedMesh = FemaleShirtOptions[currentFemaleShirtOption];
        }
    }

    //Pants
    public void nextPantsOption()
    {
        if (Hair == null || gender == null) return; // Ensure Hair and gender are assigned

        if (gender.isMale) // Accessing isMale from GenderChanger
        {
            currentMaleHairOption++;

            if (currentMaleHairOption >= MaleHairOptions.Count)
            {
                currentMaleHairOption = 0;
            }

            Hair.sharedMesh = MaleHairOptions[currentMaleHairOption];
        }
        else
        {
            currentFemaleHairOption++;

            if (currentFemaleHairOption >= FemaleHairOptions.Count)
            {
                currentFemaleHairOption = 0;
            }

            Hair.sharedMesh = FemaleHairOptions[currentFemaleHairOption];
        }
    }

    public void prevPantsOption()
    {
        if (Pants == null || gender == null) return; // Ensure Hair and gender are assigned

        if (gender.isMale) // Accessing isMale from GenderChanger
        {
            currentMalePantsOption--;

            if (currentMalePantsOption < 0)
            {
                currentMalePantsOption = MalePantsOptions.Count - 1;
            }

            Pants.sharedMesh = MalePantsOptions[currentMalePantsOption];
        }
        else
        {
            currentFemalePantsOption--;

            if (currentFemalePantsOption < 0)
            {
                currentFemalePantsOption = FemalePantsOptions.Count - 1;
            }

            Pants.sharedMesh = FemalePantsOptions[currentFemalePantsOption];
        }
    }
    //Shoes
    public void nextShoesOption()
    {
        if (Shoes == null || gender == null) return; // Ensure Hair and gender are assigned

        if (gender.isMale) // Accessing isMale from GenderChanger
        {
            currentMaleShoesOption++;

            if (currentMaleShoesOption >= MaleShoesOptions.Count)
            {
                currentMaleShoesOption = 0;
            }

            Shoes.sharedMesh = MaleShoesOptions[currentMaleShoesOption];
        }
        else
        {
            currentFemaleShoesOption++;

            if (currentFemaleShoesOption >= FemaleShoesOptions.Count)
            {
                currentFemaleShoesOption = 0;
            }

            Shoes.sharedMesh = FemaleShoesOptions[currentFemaleShoesOption];
        }
    }

    public void prevShoesOption()
    {
        if (Shoes == null || gender == null) return; // Ensure Hair and gender are assigned

        if (gender.isMale) // Accessing isMale from GenderChanger
        {
            currentMaleShoesOption--;

            if (currentMaleShoesOption < 0)
            {
                currentMaleShoesOption = MaleShoesOptions.Count - 1;
            }

            Shoes.sharedMesh = MaleShoesOptions[currentMaleShoesOption];
        }
        else
        {
            currentFemaleShoesOption--;

            if (currentFemaleShoesOption < 0)
            {
                currentFemaleShoesOption = FemaleShoesOptions.Count - 1;
            }

            Shoes.sharedMesh = FemaleShoesOptions[currentFemaleShoesOption];
        }
    }

  

}
