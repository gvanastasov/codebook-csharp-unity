using System;
using System.Collections.Generic;
using UnityEngine;

public class Arrays : MonoBehaviour
{
    // This is an array of intergers.
    // It has a fixed size of 3 elements.
    public int[] Numbers = new int[3] { 1, 10, 42 };

    void Start()
    {
        // This is how you access an element in an array.
        Debug.Log("c04p01 :: The first element at index 0 is " + Numbers[0]);
        Debug.Log("c04p01 :: The second element at index 1 is " + Numbers[1]);
        Debug.Log("c04p01 :: The last element at index N-1 is " + Numbers[Numbers.Length - 1]);

        // This is how you get the length of an array.
        Debug.Log("c04p01 :: The length of the array is " + Numbers.Length);

        // This is how you change an element in an array.
        Numbers[0] = 2;
        Numbers[1] = 20;
        Numbers[2] = 84;

        GenerateCubes();
    }

    // This is just a helper method to generate cubes in the scene, array alike.
    private void GenerateCubes()
    {
        for (int i = 0; i < Numbers.Length; i++)
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.parent = this.transform;
            cube.transform.position = new Vector3(-1f + i * 1, 0.5f, 0);
            cube.transform.localScale = new Vector3(0.5f, 0.5f, 0.25f);
            cube
                .GetComponent<MeshRenderer>()
                .SetMaterials(new List<Material> { Resources.Load<Material>("Materials/m_Green") });

            var text = new GameObject("Text", new Type[] { typeof(MeshRenderer), typeof(TextMesh) });
            text.GetComponent<TextMesh>().text = $"idx: {i}\nval: {Numbers[i]}";
            text.GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;
            text.GetComponent<TextMesh>().alignment = TextAlignment.Center;
            text.transform.parent = cube.transform;
            text.transform.localScale = new Vector3(0.2f, 0.2f, 1);
            text.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    // Feeling adventurous?
    void Matrix()
    {
        // This is a 2D array of intergers, also known as multidimensional array.
        // It has a fixed size of 3x3 elements.
        int[,] Matrix = new int[3, 3] {
            { 1, 2, 3 },
            { 10, 20, 30 },
            { 100, 200, 300 }
        };

        // This is how you access an element in a 2D array.
        Debug.Log("c04p01 :: The first element at index 0,0 is " + Matrix[0, 0]);
        Debug.Log("c04p01 :: The second element at index 0,1 is " + Matrix[0, 1]);
        Debug.Log("c04p01 :: The last element at index N-1,N-1 is " + Matrix[Matrix.GetLength(0) - 1, Matrix.GetLength(1) - 1]);

        // This is how you get the length of a 2D array.
        Debug.Log("c04p01 :: The length of the array is " + Matrix.Length);

        // This is how you change an element in a 2D array.
        Matrix[0, 0] = 2;
        Matrix[0, 1] = 20;
        Matrix[0, 2] = 84;
    }
}
