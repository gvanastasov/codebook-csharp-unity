using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class ChapterManager : MonoBehaviour
{
    public static ChapterManager Instance;

    private List<Chapter> chapters = new List<Chapter>();

    private Chapter chapterCurrent;

    private Part partCurrent;

    public Chapter ChapterCurrent
    {
        get
        {
            return this.chapterCurrent;
        }
    }

    public Part PartCurrent
    {
        get
        {
            return this.partCurrent;
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            DestroyImmediate(gameObject);
        }
    }

    public void Init()
    {
        this.chapters = this.FindChapters();
    }

    public void Activate(int chapter, int part)
    {
        this.Chapter_Activate(chapter);
        this.Part_Activate(part);
    }

    public List<string> GetSceneChaptersNames()
    {
        return this.GetChapterNames(this.chapters);
    }

    public List<string> GetCurrentPartsNames()
    {
        return this.GetChapterPartsNames(this.chapterCurrent);
    }
    
    public static int[] LoadProgress()
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

    public static void SaveProgress(int chapter, int part)
    {
        EditorPrefs.SetString("ChapterManager.CurrentChapter", $"c{chapter:D2}p{part:D2}");
    }

    private void Part_Activate(int idx)
    {
        if (idx < 0 || idx >= this.chapterCurrent.Parts.Count)
        {
            Debug.LogError($"Invalid part index: {idx}");
        }

        if (
            this.partCurrent != null &&
            this.partCurrent.GetInstanceID() == this.chapterCurrent.Parts[idx].GetInstanceID())
        {
            return;
        }

        this.Chapter_DeactivateParts(this.chapterCurrent);
        this.partCurrent = this.chapterCurrent.Parts[idx];
        this.partCurrent.gameObject.SetActive(true);
    }

    private void Chapter_Activate(int idx)
    {
        if (idx < 0 || idx >= this.chapters.Count)
        {
            Debug.LogError($"Invalid chapter index: {idx}");
            return;
        }

        if (
            this.chapterCurrent != null && 
            this.chapterCurrent.GetInstanceID() == this.chapters[idx].GetInstanceID())
        {
            return;
        }

        this.Chapter_DeactivateAll();
        this.chapterCurrent = this.chapters[idx];
        this.chapterCurrent.gameObject.SetActive(true);
    }

    private void Chapter_DeactivateAll()
    {
        foreach (var chapter in this.chapters)
        {
            chapter.gameObject.SetActive(false);
        }
        this.chapterCurrent = null;
    }

    private void Chapter_DeactivateParts(Chapter chapter)
    {
        foreach (var part in chapter.Parts)
        {
            part.gameObject.SetActive(false);
        }
        this.partCurrent = null;
    }

    private List<string> GetChapterNames(List<Chapter> chapters)
    {
        return chapters.Select(chapter => chapter.Title).ToList();
    }

    private List<string> GetChapterPartsNames(Chapter chapter)
    {
        return chapter.Parts.Select(part => part.Title).ToList();
    }

    private List<Chapter> FindChapters()
    {
        var chapters = new List<Chapter>();
        var root = GameObject.Find("chapters");

        if (root == null)
        {
            Debug.LogError("Could not find chapters root.");
            return chapters;
        }

        foreach (Transform child in root.transform)
        {
            var chapter = child.GetComponent<Chapter>();

            if (chapter != null)
            {
                chapters.Add(chapter);
            }
        }

        return chapters;
    }
}
