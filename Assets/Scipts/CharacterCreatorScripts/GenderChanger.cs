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
    public List<Mesh> MaleMeshes = new List<Mesh>();
    public List<Mesh> FMeshes = new List<Mesh>();
   public bool isMale = true;
    public void ChangeToMale()
    {

        isMale = true;
            Head.sharedMesh = MaleMeshes[0];
            Hair.sharedMesh = MaleMeshes[1];
            shirt.sharedMesh = MaleMeshes[2];
            Body.sharedMesh = MaleMeshes[3];
            Legs.sharedMesh = MaleMeshes[4];
            pants.sharedMesh = MaleMeshes[5];
            Shoes.sharedMesh = MaleMeshes[6];
        
    }
    public void ChangeToFemale()
    {
        isMale = false;

        Head.sharedMesh = FMeshes[0];
        Hair.sharedMesh = FMeshes[1];
        shirt.sharedMesh = FMeshes[2];
        Body.sharedMesh = FMeshes[3];
        Legs.sharedMesh = FMeshes[4];
        pants.sharedMesh = FMeshes[5];
        Shoes.sharedMesh = FMeshes[6];

    }

}
