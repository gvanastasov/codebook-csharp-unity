using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

/// <summary>
/// A custom tool for managing chapters and parts in the Unity editor.
/// </summary>
public class ChapterManager : EditorWindow
{
    private const string NEW_CHAPTER_NAME = "New Chapter";
    private static string newChapterName = "New Chapter";

    /// <summary>
    /// Called when the scripts are reloaded, to ensure the tool is enabled or disabled as appropriate.
    /// </summary>
    [DidReloadScripts]
    private static void OnScriptsReloaded() 
    {
        var state = Menu.GetChecked("Window/Custom Tools/Chapter Manager");
        if (state) 
        {
            SceneView.duringSceneGui += OnSceneGUI;
        }
        else
        {
            SceneView.duringSceneGui -= OnSceneGUI;
        }
    }

    /// <summary>
    /// Toggles the Chapter Manager tool on and off.
    /// </summary>
    [MenuItem("Window/Custom Tools/Chapter Manager")]
    public static void ToggleTools()
    {
        var state = Menu.GetChecked("Window/Custom Tools/Chapter Manager");
        Menu.SetChecked("Window/Custom Tools/Chapter Manager", !state);

        if (!state)
        {
            SceneView.duringSceneGui += OnSceneGUI;
        }
        else
        {
            SceneView.duringSceneGui -= OnSceneGUI;
        }
    }

    /// <summary>
    /// Draws the GUI for the Chapter Manager tool.
    /// </summary>
    /// <param name="sceneview"></param>
    private static void OnSceneGUI(SceneView sceneview)
    {
        Handles.BeginGUI();

        GUILayout.BeginArea(new Rect(Screen.width - 210, Screen.height - 160, 200, 160));
        GUILayout.BeginVertical("box");

        GUILayout.Label("Chapter Manager");

        GameObject selectedObject = Selection.activeGameObject;
        bool isChapterSelected = selectedObject != null && selectedObject.name.StartsWith("chapter_");

        if (GUILayout.Button("Create Chapter"))
        {
            var parent = GameObject.Find("chapters").transform;
            if (parent == null)
            {
                Debug.LogError("Could not find chapters parent object.");
                return;
            }

            var newChapter = new GameObject($"chapter_00 - {newChapterName}");
            InsertChapter(parent, newChapter, isChapterSelected);

            newChapterName = NEW_CHAPTER_NAME;
        }
        newChapterName = GUILayout.TextField(newChapterName, 80);

        GUI.enabled = isChapterSelected;
        if (GUILayout.Button("Create Part"))
        {
            // Call your method for creating a new part here
        }

        if (GUILayout.Button("Remove Chapter"))
        {
            var parent = GameObject.Find("chapters").transform;
            if (parent == null)
            {
                Debug.LogError("Could not find chapters parent object.");
                return;
            }
            selectedObject.transform.SetParent(null);
            DestroyImmediate(selectedObject);
            ReindexChapters(parent);
        }
        GUI.enabled = true;

        if (GUI.Button(new Rect(177, 3, 12, 12), "", "WinBtnClose"))
        {
            ToggleTools();
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();

        Handles.EndGUI();
    }

    /// <summary>
    /// Re-indexes the chapters in the parent object.
    /// </summary>
    /// <param name="parent"></param>
    private static void ReindexChapters(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            var child = parent.GetChild(i);
            SetChapterNameByIdx(i, child);
        }
    }

    /// <summary>
    /// Inserts a new chapter into the parent object, either at the end or after the currently selected chapter.
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="newChapter"></param>
    /// <param name="createAfterCurrent"></param>
    private static void InsertChapter(Transform parent, GameObject newChapter, bool createAfterCurrent)
    {
        // get index
        var desiredIndex = createAfterCurrent 
            ? Selection.activeTransform.GetSiblingIndex() + 1 
            : parent.childCount;

        // cache
        var children = new List<Transform>();
        for (int i = 0; i < parent.childCount; i++)
        {
            var child = parent.GetChild(i);
            children.Add(child);
        }

        // unparent
        foreach (var child in children)
        {
            child.SetParent(null);
        }

        // insert new chapter
        if (desiredIndex >= children.Count)
        {
            children.Add(newChapter.transform);
        }
        else
        {
            children.Insert(desiredIndex, newChapter.transform);
        }

        // re-parent
        for (int i = 0; i < children.Count; i++)
        {
            var child = children[i];
            child.SetParent(parent);
            child.SetSiblingIndex(i);

            SetChapterNameByIdx(i, child);
        }
    }

    /// <summary>
    /// Sets the name of the chapter based on its index in the parent.
    /// </summary>
    /// <param name="idx"></param>
    /// <param name="child"></param>
    private static void SetChapterNameByIdx(int idx, Transform child)
    {
        var nameParts = child.name.Split('-');

        var formattedIndex = idx < 9 ? "0" + (idx + 1).ToString() : (idx + 1).ToString();
        nameParts[0] = "chapter_" + formattedIndex;

        child.name = string.Join("-", nameParts);
    }
}