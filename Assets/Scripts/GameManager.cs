using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string NAME_PATTERN = @"(chapter|part)_(\d+) - (.*)";
    private const string NAME_FORMAT = "$2. $3";

    public static GameManager Instance;

    public GameObject ChaptersRoot;

    private List<GameObject> chapters;

    private GameObject chapterCurrent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            DestroyImmediate(gameObject);
        }

        this.chapters = this.FindChapters();
        this.Chapter_DeactivateAll();
        this.Chapter_Activate(0);
    }

    private void Start()
    {
        GUIManager.Instance.SetChapterOptions(FindChapterNames());
        GUIManager.Instance.SetPartOptions(FindPartNames(this.chapterCurrent));

        GUIManager.Instance.ChapterDropdown.onValueChanged.AddListener(
            delegate { OnChapterDropdown_Changed(GUIManager.Instance.ChapterDropdown.value); });
    }

    private void OnChapterDropdown_Changed(int idx)
    {
        this.Chapter_DeactivateCurrent();
        this.Chapter_Activate(idx);
        GUIManager.Instance.SetPartOptions(FindPartNames(this.chapterCurrent));
    }

    private void Chapter_DeactivateAll()
    {
        foreach (GameObject chapter in this.chapters)
        {
            chapter.SetActive(false);
        }
        this.chapterCurrent = null;
    }

    private void Chapter_Activate(int chapterIndex)
    {
        this.chapterCurrent = this.chapters[chapterIndex];
        this.chapterCurrent.SetActive(true);
    }

    private void Chapter_Deactivate(GameObject chapter)
    {
        this.chapters.FirstOrDefault(x => x.GetInstanceID() == chapter.GetInstanceID()).SetActive(false);
    }

    private void Chapter_DeactivateCurrent()
    {
        this.Chapter_Deactivate(this.chapterCurrent);
    }

#region Helpers
    public List<GameObject> FindChapters()
    {
        List<GameObject> chapters = new List<GameObject>();

        foreach (Transform child in this.ChaptersRoot.transform)
        {
            if (child.gameObject.tag == "Chapter")
            {
                chapters.Add(child.gameObject);
            }
        }

        return chapters;
    }

    public List<string> FindChapterNames()
    {
        return this.FindChapters().Select(c => this.ExtractName(c.name)).ToList();
    }

    public List<GameObject> FindParts(GameObject chapter)
    {
        List<GameObject> parts = new List<GameObject>();

        foreach (Transform child in chapter.transform)
        {
            if (child.gameObject.tag == "Part")
            {
                parts.Add(child.gameObject);
            }
        }

        return parts;
    }

    public List<string> FindPartNames(GameObject chapter)
    {
        return this.FindParts(chapter).Select(c => this.ExtractName(c.name)).ToList();
    }

    private string ExtractName(string name)
    {
        return Regex.Replace(name, NAME_PATTERN, NAME_FORMAT);
    }
#endregion
}
