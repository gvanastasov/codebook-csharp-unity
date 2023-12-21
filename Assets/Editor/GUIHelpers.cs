using UnityEditor;
using UnityEngine;

public static class GUIHelpers
{
    /// <summary>
    /// Draws a line in the GUI.
    /// </summary>
    /// <param name="color"></param>
    /// <param name="thickness"></param>
    /// <param name="padding"></param>
    public static void DrawUILine(Color color, int thickness = 2, int padding = 10)
    {
        Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding+thickness));
        r.height = thickness;
        r.y+=padding/2;
        r.x-=2;
        r.width +=6;
        EditorGUI.DrawRect(r, color);
    }
}
