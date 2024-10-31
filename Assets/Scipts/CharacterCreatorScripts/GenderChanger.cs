using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class GenderChanger : MonoBehaviour
{

    [Header("Mesh to Change")]
    public SkinnedMeshRenderer Head;
    public SkinnedMeshRenderer Hair;
    public SkinnedMeshRenderer shirt;
    public SkinnedMeshRenderer Body;
    public SkinnedMeshRenderer Legs;
    public SkinnedMeshRenderer pants;
    public SkinnedMeshRenderer Shoes;



    [Header("Mesh to Cycle Through")]
    public List<Mesh> Meshes = new List<Mesh>();


    public void ChangeToFemale()
    {

        Head.sharedMesh = Meshes[0];
        Hair.sharedMesh = Meshes[1];
        shirt.sharedMesh = Meshes[2];    
        Body.sharedMesh = Meshes[3];    
        Legs.sharedMesh = Meshes[4];
        pants.sharedMesh = Meshes[5];
        Shoes.sharedMesh = Meshes[6];
        

    }

}
