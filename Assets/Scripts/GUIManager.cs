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

    public GameObject BackButton;

    public GameObject NextButton;
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

    private void Start()
    {
        this.ChapterDropdown.onValueChanged.AddListener(delegate { OnActive_Changed(); });
        this.PartDropdown.onValueChanged.AddListener(delegate { OnActive_Changed(); });
    }

    public void SetChapterOptions(List<string> options)
    {
        this.ChapterDropdown.ClearOptions();
        this.ChapterDropdown.AddOptions(options);
        this.ChapterDropdown.RefreshShownValue();

        Navigation_Update();
    }

    public void SetPartOptions(List<string> options)
    {
        this.PartDropdown.ClearOptions();
        this.PartDropdown.AddOptions(options);

        if (options.Count == 0)
        {
            this.PartDropdown.interactable = false;
            Debug.LogWarning("No parts found for this chapter.");
        }
        else
        {
            this.PartDropdown.interactable = true;
        }

        this.PartDropdown.RefreshShownValue();
        this.PartDropdown.value = 0;

        Navigation_Update();
    }

    public void SetNextPart()
    {
        if (this.PartDropdown.value < this.PartDropdown.options.Count - 1)
        {
            this.PartDropdown.value++;
            this.PartDropdown.RefreshShownValue();
        }
        else
        {
            GameManager.Instance.Chapter_Next();
        }
    }

    public void SetPreviousPart()
    {
        if (this.PartDropdown.value > 0)
        {
            this.PartDropdown.value--;
            this.PartDropdown.RefreshShownValue();
        }
        else
        {
            GameManager.Instance.Chapter_Previous();
        }
    }

    private void OnActive_Changed()
    {
        Navigation_Update();
    }

    private void Navigation_Update()
    {
        if (this.ChapterDropdown.value == 0 && this.PartDropdown.value == 0)
        {
            this.BackButton.SetActive(false);
        }
        else
        {
            this.BackButton.SetActive(true);
        }

        if (this.ChapterDropdown.value == this.ChapterDropdown.options.Count - 1 &&
            this.PartDropdown.value == this.PartDropdown.options.Count - 1)
        {
            this.NextButton.SetActive(false);
        }
        else
        {
            this.NextButton.SetActive(true);
        }
    }
}
