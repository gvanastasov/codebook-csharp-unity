using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
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

        var progress = LoadProgress();
        
        this.chapters = this.FindChapters();
        this.Chapter_Activate(progress[0]);
        this.Chapter_ActivatePart(this.chapterCurrent, progress[1]);

        GUIManager.Instance.SetChapterOptions(FindChapterNames());
        GUIManager.Instance.SetPartOptions(FindPartNames(this.chapterCurrent));

        GUIManager.Instance.ChapterDropdown.onValueChanged.AddListener(
            delegate { OnChapterDropdown_Changed(GUIManager.Instance.ChapterDropdown.value); });
        GUIManager.Instance.PartDropdown.onValueChanged.AddListener(
            delegate { OnPartDropdown_Changed(GUIManager.Instance.PartDropdown.value); });

        GUIManager.Instance.ChapterDropdown.value = progress[0];
        GUIManager.Instance.PartDropdown.value = progress[1];
    }

    private static int[] LoadProgress()
    {
        var savedChapter = EditorPrefs.GetString("ChapterManager.CurrentChapter");
        if (string.IsNullOrEmpty(savedChapter))
        {
            savedChapter = "c01p01";
        }

        string pattern = @"c(\d+)p(\d+)";
        Match match = Regex.Match(savedChapter, pattern);

        if (match.Success)
        {
            int chapter = int.Parse(match.Groups[1].Value);
            int part = int.Parse(match.Groups[2].Value);

            return new int[] { chapter, part };
        }

        return new int[] { 0, 0 };
    }

    private static void SaveProgress(int chapter, int part)
    {
        EditorPrefs.SetString("ChapterManager.CurrentChapter", $"c{chapter:D2}p{part:D2}");
    }

    private void OnChapterDropdown_Changed(int idx)
    {
        this.Chapter_Activate(idx);
        this.Chapter_ActivatePart(this.chapterCurrent, 0);
        GUIManager.Instance.SetPartOptions(FindPartNames(this.chapterCurrent));

        SaveProgress(idx, 0);
    }

    private void OnPartDropdown_Changed(int idx)
    {
        this.Chapter_ActivatePart(this.chapterCurrent, idx);

        SaveProgress(GUIManager.Instance.ChapterDropdown.value, idx);
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
        this.Chapter_DeactivateAll();
        this.chapterCurrent = this.chapters[chapterIndex];
        this.chapterCurrent.SetActive(true);
    }

    private void Chapter_DeactivateParts(GameObject chapter)
    {
        foreach (Transform child in chapter.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void Chapter_ActivatePart(GameObject chapter, int partIndex)
    {
        this.ClearConsole();
        this.Chapter_DeactivateParts(chapter);
        chapter.transform.GetChild(partIndex).gameObject.SetActive(true);
    }

    public void Chapter_Next()
    {
        int idx = GUIManager.Instance.ChapterDropdown.value + 1;
        if (idx >= GUIManager.Instance.ChapterDropdown.options.Count)
        {
            idx = 0;
        }
        GUIManager.Instance.ChapterDropdown.value = idx;
        OnChapterDropdown_Changed(idx);
    }

    public void Chapter_Previous()
    {
        int idx = GUIManager.Instance.ChapterDropdown.value - 1;
        if (idx < 0)
        {
            idx = GUIManager.Instance.ChapterDropdown.options.Count - 1;
        }
        GUIManager.Instance.ChapterDropdown.value = idx;
        OnChapterDropdown_Changed(idx);
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

    private void ClearConsole()
    {
        Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
        Type logEntries = assembly.GetType("UnityEditor.LogEntries");
        MethodInfo clearConsoleMethod = logEntries.GetMethod("Clear");
        clearConsoleMethod.Invoke(new object(), null);
    }
#endregion
}
