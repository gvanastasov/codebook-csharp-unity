using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
#region Static Members
    public static GUIManager Instance;
#endregion

#region Serialized Fields
    public TMP_Dropdown ChapterDropdown;

    public TMP_Dropdown PartDropdown;
#endregion

#region Public Properties
    public string SelectedChapterName
    {
        get
        {
            return ChapterDropdown.options[ChapterDropdown.value].text;
        }
    }
#endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
    }

    public void SetChapterOptions(List<string> options)
    {
        ChapterDropdown.ClearOptions();
        ChapterDropdown.AddOptions(options);
        ChapterDropdown.RefreshShownValue();
    }

    public void SetPartOptions(List<string> options)
    {
        PartDropdown.ClearOptions();
        PartDropdown.AddOptions(options);

        if (options.Count == 0)
        {
            PartDropdown.interactable = false;
            Debug.LogWarning("No parts found for this chapter.");
        }
        else
        {
            PartDropdown.interactable = true;
        }

        PartDropdown.RefreshShownValue();
        PartDropdown.value = 0;
    }
}
