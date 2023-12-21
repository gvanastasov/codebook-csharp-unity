using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

/// <summary>
/// A custom tool for managing chapters and parts in the Unity editor.
/// </summary>
public class ChapterManager : EditorWindow
{
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

        GUILayout.BeginArea(new Rect(Screen.width - 210, Screen.height - 140, 200, 140));
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

            var newChapter = new GameObject("chapter_00 - New Chapter");
            InsertChapter(parent, newChapter, isChapterSelected);
        }

        GUI.enabled = isChapterSelected;
        if (GUILayout.Button("Create Part"))
        {
            // Call your method for creating a new part here
        }

        if (GUILayout.Button("Remove Chapter"))
        {
            // Call your method for removing the selected chapter here
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
    /// Inserts a new chapter into the parent object, either at the end or after the currently selected chapter.
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="newChapter"></param>
    /// <param name="createAfterCurrent"></param>
    private static void InsertChapter(Transform parent, GameObject newChapter, bool createAfterCurrent)
    {
        var desiredIndex = createAfterCurrent 
            ? Selection.activeTransform.GetSiblingIndex() + 1 
            : parent.childCount;

        var allChildren = new List<Transform>();
        for (int i = 0; i < parent.childCount; i++)
        {
            var child = parent.GetChild(i);
            allChildren.Add(child);
        }

        foreach (var child in allChildren)
        {
            child.SetParent(null);
        }

        if (desiredIndex >= allChildren.Count)
        {
            allChildren.Add(newChapter.transform);
        }
        else
        {
            allChildren.Insert(desiredIndex, newChapter.transform);
        }

        for (int i = 0; i < allChildren.Count; i++)
        {
            allChildren[i].SetParent(parent);
            allChildren[i].SetSiblingIndex(i);

            string[] nameParts = allChildren[i].name.Split('-');

            string formattedIndex = i < 9 ? "0" + (i + 1).ToString() : (i + 1).ToString();
            nameParts[0] = "chapter_" + formattedIndex;

            allChildren[i].name = string.Join("-", nameParts);
        }
    }
}