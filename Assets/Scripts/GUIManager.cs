using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public static GUIManager Instance;

    public TMP_Dropdown ChapterDropdown;

    public TMP_Dropdown PartDropdown;

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
            PartDropdown.AddOptions(new List<string> { "n/a" });
        }
        else
        {
            PartDropdown.interactable = true;
        }

        PartDropdown.RefreshShownValue();
        PartDropdown.value = 0;
    }
}
