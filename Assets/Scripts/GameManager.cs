using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject ChaptersRoot;

    private List<GameObject> chapters;

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

        Setup();
    }

    private void Setup()
    {
        this.chapters = this.FindChapters();
        foreach (GameObject chapter in this.chapters)
        {
            chapter.SetActive(false);
        }
        this.chapters.First().SetActive(true);
    }

    private void Start()
    {
        GUIManager.Instance.SetChapterOptions(this.chapters.Select(c => c.name).ToList());
        GUIManager.Instance.SetPartOptions(FindParts(this.chapters.First()).Select(c => c.name).ToList());

        GUIManager.Instance.ChapterDropdown.onValueChanged.AddListener(delegate { OnChapterDropdown_Changed(); });
    }

    public List<GameObject> FindChapters()
    {
        List<GameObject> chapters = new List<GameObject>();

        foreach (Transform child in this.ChaptersRoot.transform)
        {
            if (child.gameObject.tag == "Chapter")
            {
                Debug.Log("Found chapter: " + child.gameObject.name);
                chapters.Add(child.gameObject);
            }
        }

        return chapters;
    }

    public List<GameObject> FindParts(GameObject chapter)
    {
        List<GameObject> parts = new List<GameObject>();

        foreach (Transform child in chapter.transform)
        {
            if (child.gameObject.tag == "Part")
            {
                Debug.Log("Found part: " + child.gameObject.name);
                parts.Add(child.gameObject);
            }
        }

        return parts;
    }

    private void OnChapterDropdown_Changed()
    {
        string chapterName = GUIManager.Instance.ChapterDropdown.options[GUIManager.Instance.ChapterDropdown.value].text;
        GameObject chapter = this.chapters
            .Where(chapterGameObject => {
                var isSelected = chapterGameObject.name == chapterName;
                chapterGameObject.SetActive(isSelected);
                return isSelected;
            })
            .First();
        GUIManager.Instance.SetPartOptions(FindParts(chapter).Select(c => c.name).ToList());
    }
}
