using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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

        var progress = ChapterManager.LoadProgress();

        ChapterManager.Instance.Init();
        ChapterManager.Instance.Activate(
            chapter: progress[0],
            part: progress[1]
        );
        
        GUIManager.Instance.SetChapterOptions(
            ChapterManager.Instance.GetSceneChaptersNames());
        GUIManager.Instance.SetPartOptions(
            ChapterManager.Instance.GetCurrentPartsNames());

        GUIManager.Instance.ChapterDropdown.value = progress[0];
        GUIManager.Instance.PartDropdown.value = progress[1];

        GUIManager.Instance.SetDescription(
            ChapterManager.Instance.PartCurrent.Description);

        GUIManager.Instance.ChapterDropdown.onValueChanged.AddListener(
            delegate { OnChapterDropdown_Changed(GUIManager.Instance.ChapterDropdown.value); });
        GUIManager.Instance.PartDropdown.onValueChanged.AddListener(
            delegate { OnPartDropdown_Changed(GUIManager.Instance.PartDropdown.value); });
    }

    private void OnChapterDropdown_Changed(int chapterIdx)
    {
        ChapterManager.Instance.Activate(chapterIdx, 0);

        GUIManager.Instance.SetPartOptions(
            ChapterManager.Instance.GetCurrentPartsNames());
        GUIManager.Instance.SetNavigation();

        ChapterManager.SaveProgress(chapterIdx, 0);
        ClearConsole();
    }

    private void OnPartDropdown_Changed(int partIdx)
    {
        ChapterManager.Instance.Activate(
            GUIManager.Instance.ChapterDropdown.value, partIdx);

        GUIManager.Instance.SetDescription(
            ChapterManager.Instance.PartCurrent.Description);

        ChapterManager.SaveProgress(
            GUIManager.Instance.ChapterDropdown.value, partIdx);
        ClearConsole();
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
    private void ClearConsole()
    {
        Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
        Type logEntries = assembly.GetType("UnityEditor.LogEntries");
        MethodInfo clearConsoleMethod = logEntries.GetMethod("Clear");
        clearConsoleMethod.Invoke(new object(), null);
    }
#endregion
}
