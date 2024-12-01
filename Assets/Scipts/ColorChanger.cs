using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Progress;

public class ColorChanger : MonoBehaviour
{
    [Header("Mesh to Change")]
    public SkinnedMeshRenderer Head;
    public SkinnedMeshRenderer Hair;
    public SkinnedMeshRenderer Body;
    public SkinnedMeshRenderer Legs;
    public SkinnedMeshRenderer Shirt;
    public SkinnedMeshRenderer Pants;
    public SkinnedMeshRenderer Shoes;




    [Header("Material to Cycle Through")]
    public List<Material> SkinColorOptions = new List<Material>();
    public List<Material> HairColorOptions = new List<Material>();
    public List<Material> ShirtColorOptions = new List<Material>();
    public List<Material> PantsColorOptions = new List<Material>();
    public List<Material> ShoesColorOptions = new List<Material>();

    private int currentSkinColorOption = 0;
    private int currentHairColorOption = 0;
    private int currentShirtColorOption = 0;
    private int currentPantsColorOption = 0;
    private int currentShoesColorOption = 0;


    //Skin Color
    public void nextSkinOption()
    {
        currentSkinColorOption++;

        if (currentSkinColorOption >= SkinColorOptions.Count)
        {

            currentSkinColorOption = 0;
        }

        Head.material = SkinColorOptions[currentSkinColorOption];
        Body.material = SkinColorOptions[currentSkinColorOption];
        Legs.material = SkinColorOptions[currentSkinColorOption];

    }

    public void prevSkinOption()
    {
        currentSkinColorOption--;
    
            if (currentSkinColorOption <= 0)
            {

                currentSkinColorOption = SkinColorOptions.Count - 1;
            }
            Head.material = SkinColorOptions[currentSkinColorOption];
            Body.material = SkinColorOptions[currentSkinColorOption];
            Legs.material = SkinColorOptions[currentSkinColorOption];

    }

    //Hair Color
    public void nextHairOption()
    {
        currentHairColorOption++;

        if (currentHairColorOption >= HairColorOptions.Count)
        {

            currentHairColorOption = 0;
        }

        Hair.material = HairColorOptions[currentHairColorOption];
        

    }

    public void prevHairOption()
    {
        currentHairColorOption--;

        if (currentHairColorOption <= 0)
        {

            currentHairColorOption = HairColorOptions.Count - 1;
        }
        Hair.material = HairColorOptions[currentHairColorOption];
     

    }

    //Shirt
    public void nextShirtColorOption()
    {
        currentShirtColorOption++;

        if (currentShirtColorOption >= ShirtColorOptions.Count)
        {

            currentShirtColorOption = 0;
        }

        Shirt.material = ShirtColorOptions[currentShirtColorOption];


    }

    public void prevShirtColorOption()
    {
        currentShirtColorOption--;

        if (currentShirtColorOption <= 0)
        {

            currentShirtColorOption = ShirtColorOptions.Count - 1;
        }
       Shirt.material = ShirtColorOptions[currentShirtColorOption];


    }

    //Pants Color
    public void nextPantsColorOption()
    {
        currentPantsColorOption++;

        if (currentPantsColorOption >= PantsColorOptions.Count)
        {

            currentPantsColorOption = 0;
        }

        Pants.material = PantsColorOptions[currentPantsColorOption];


    }

    public void prevpantsColorOption()
    {
        currentPantsColorOption--;

        if (currentPantsColorOption <= 0)
        {

            currentPantsColorOption = PantsColorOptions.Count - 1;
        }
        Pants.material = PantsColorOptions[currentPantsColorOption];


    }


    //Shoes Color
    public void nextShoesColorOption()
    {
        currentShoesColorOption++;

        if (currentShoesColorOption >= ShoesColorOptions.Count)
        {

            currentShoesColorOption = 0;
        }

        Shoes.material = ShoesColorOptions[currentShoesColorOption];


    }

    public void prevShoesColorOption()
    {
        currentShoesColorOption--;

        if (currentShoesColorOption <= 0)
        {

            currentShoesColorOption = ShoesColorOptions.Count - 1;
        }
        Shoes.material = ShoesColorOptions[currentShoesColorOption];


    }







}
