using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class AvatarCreationManager : MonoBehaviour
{
    // Dropdown fields
    private DropdownField outfitDropdown;
    private DropdownField hairDropdown;
    private DropdownField colorDropdown;
    private Button finishButton;

    // USS path file
    public StyleSheet avatarStyleSheet;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        //USS stylesheet
        if (avatarStyleSheet != null)
        {
            root.styleSheets.Add(avatarStyleSheet);
        }
        // Fetch UI components
        outfitDropdown = root.Q<DropdownField>("OutfitDropdown");
        hairDropdown = root.Q<DropdownField>("HairDropdown");
        colorDropdown = root.Q<DropdownField>("ColorDropdown");
        finishButton = root.Q<Button>("finishButton");

        outfitDropdown.choices = new List<string> { "Casual", "Formal", "Sporty" };
        hairDropdown.choices = new List<string> { "Short", "Medium", "Long" };
        colorDropdown.choices = new List<string> { "Red", "Blue", "Green" };

        finishButton.clicked += OnFinishButtonClicked;

        // Set default selected values
        outfitDropdown.index = 0;
        hairDropdown.index = 0;
        colorDropdown.index = 0;

        // Bring dropdowns to front when focused or clicked
        outfitDropdown.RegisterCallback<FocusInEvent>(ev => BringToFront(outfitDropdown));
        hairDropdown.RegisterCallback<FocusInEvent>(ev => BringToFront(hairDropdown));
        colorDropdown.RegisterCallback<FocusInEvent>(ev => BringToFront(colorDropdown));
    }
    // Bring UI elements to front
    private void BringToFront(VisualElement element)
    {
        element.BringToFront();
    }
    // Method triggered when the finish button is clicked
    private void OnFinishButtonClicked()
    {
        SaveAvatarPreferences(outfitDropdown.value, hairDropdown.value, colorDropdown.value);
        SceneManager.LoadScene("RegisterScene");
    }

    // Save avatar preferences
    private void SaveAvatarPreferences(string outfit, string hair, string color)
    {
        PlayerPrefs.SetString("AvatarOutfit", outfit);
        PlayerPrefs.SetString("AvatarHair", hair);
        PlayerPrefs.SetString("AvatarColor", color);
        PlayerPrefs.Save();
    }
}
