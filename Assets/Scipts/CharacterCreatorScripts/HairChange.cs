using System.Collections.Generic;
using UnityEngine;


public class HairChange : MonoBehaviour
{
    [Header("Mesh to Change")]
    public SkinnedMeshRenderer Hair;

    [Header("Gender Component")]
    public GenderChanger gender; // Reference to the GenderChanger script

    [Header("Material to Cycle Through")]
    public List<Mesh> MaleHairOptions = new List<Mesh>();
    public List<Mesh> FemaleHairOptions = new List<Mesh>();

    private int currentMaleHairOption = 0;
    private int currentFemaleHairOption = 0;

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

    public void nextOption()
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

    public void prevOption()
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
}
