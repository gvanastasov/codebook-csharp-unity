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
    private static string newPartName = "New Part";

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

        GUILayout.BeginArea(new Rect(Screen.width - 210, Screen.height - 200, 200, 200));
        GUILayout.BeginVertical("box");

        GUILayout.Label("Chapter Manager");

        GameObject selectedObject = Selection.activeGameObject;
        bool isChapterSelected = selectedObject != null && selectedObject.name.StartsWith("chapter_");
        bool isPartSelected = selectedObject != null && selectedObject.name.StartsWith("part_");

        if (GUILayout.Button("Create Chapter"))
        {
            CreateChapter(isChapterSelected);
        }
        newChapterName = GUILayout.TextField(newChapterName, 80);

        GUI.enabled = isChapterSelected;
        if (GUILayout.Button("Remove Chapter"))
        {
            DeleteChapter(selectedObject);
        }

        GUIHelpers.DrawUILine(Color.gray, 1, 10);
        
        GUI.enabled = isChapterSelected || isPartSelected;
        if (GUILayout.Button("Create Part"))
        {
            var parent = isChapterSelected ? selectedObject : selectedObject.transform.parent.gameObject;
            CreatePart(parent);
        }

        GUI.enabled = isPartSelected;
        if (GUILayout.Button("Remove Part"))
        {
            DeletePart(selectedObject);
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
    /// Creates a new part in the selected chapter.
    /// </summary>
    /// <param name="selectedChapter"></param>
    private static void CreatePart(GameObject selectedChapter)
    {
        var newPart = new GameObject($"part_00 - {newPartName}");
        Undo.RegisterCreatedObjectUndo(newPart, "Create Part");

        // todo: for now always at the end.
        newPart.transform.SetParent(selectedChapter.transform);
        Reindex(selectedChapter.transform, "part");
    }

    /// <summary>
    /// Removes the selected part from the parent object.
    /// </summary>
    /// <param name="selectedPart"></param>
    private static void DeletePart(GameObject selectedPart)
    {
        var parent = selectedPart.transform.parent;
        if (parent == null)
        {
            Debug.LogError("Could not find part chapter parent.");
            return;
        }
        Undo.DestroyObjectImmediate(selectedPart);
        Reindex(parent, "part");
    }

    /// <summary>
    /// Removes the selected chapter from the parent object.
    /// </summary>
    /// <param name="selectedObject"></param>
    private static void DeleteChapter(GameObject selectedObject)
    {
        var parent = GameObject.Find("chapters").transform;
        if (parent == null)
        {
            Debug.LogError("Could not find chapters parent object.");
            return;
        }
        Undo.DestroyObjectImmediate(selectedObject);
        Reindex(parent);
    }

    /// <summary>
    /// Creates a new chapter in the parent object.
    /// </summary>
    /// <param name="isChapterSelected"></param>
    private static void CreateChapter(bool isChapterSelected)
    {
        var parent = GameObject.Find("chapters").transform;
        if (parent == null)
        {
            Debug.LogError("Could not find chapters parent object.");
            return;
        }

        var newChapter = new GameObject($"chapter_00 - {newChapterName}");
        Undo.RegisterCreatedObjectUndo(newChapter, "Create Chapter");

        var idx = isChapterSelected
            ? Selection.activeTransform.GetSiblingIndex() + 1
            : parent.childCount;

        parent.InsertChild(newChapter.transform, idx);
        Reindex(parent);

        newChapterName = NEW_CHAPTER_NAME;
    }

    /// <summary>
    /// Re-indexes children name prefix in the parent object.
    /// </summary>
    /// <param name="parent"></param>
    private static void Reindex(Transform parent, string type = "chapter")
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            var child = parent.GetChild(i);
            SetNameByIdx(i, child, type);
        }
    }

    /// <summary>
    /// Sets the name of the game object based on its index in the parent, given format: "{prefix}_00 - {name}".
    /// </summary>
    /// <param name="idx"></param>
    /// <param name="child"></param>
    private static void SetNameByIdx(int idx, Transform child, string prefix = "chapter")
    {
        var nameParts = child.name.Split('-', StringSplitOptions.RemoveEmptyEntries);

        var formattedIndex = idx < 9 ? "0" + (idx + 1).ToString() : (idx + 1).ToString();
        nameParts[0] = prefix + "_" + formattedIndex;

        child.name = string.Join(" - ", nameParts);
    }
}