using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loops : MonoBehaviour
{
    public TextMesh Counter;
    public GameObject forLoopBox;
    public GameObject foreachLoopBox;
    public GameObject whileLoopBox;
    public GameObject doWhileLoopBox;

    private List<GameObject> cubes = new List<GameObject>();

    private List<string> materials = new List<string>
    {
        "m_Red",
        "m_Green",
        "m_Blue",
    };

    private float offsetX = -2;

    private void Awake()
    {
        ResetBoxes();
        UpdateCubesCounterText();
    }

    private void Start()
    {
        // Special type of function that can be paused and resumed at any point.
        this.StartCoroutine(GenerateCubes_ForLoop());
    }

    private IEnumerator GenerateCubes_ForLoop()
    {
        forLoopBox.SetActive(true);

        // This is a for loop, used to repeat a block of code a set number of times.
        // The loop will automatically keep track of the index for you.
        // You need to specify the starting index, the end index, and the step.
        // 
        Debug.Log("c04p04 :: Loops :: created 5 cubes with a FOR loop, bravo.");
        for (int i = 0; i < 5; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.name = $"Just a Clone Cube_{cubes.Count}";
            cube.transform.parent = this.transform;
            cube.transform.position = new Vector3(offsetX, 0.5f, 0);
            cube.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            
            // Add the cube to the list of cubes.
            cubes.Add(cube);
            UpdateCubesCounterText();

            offsetX += 0.4f;

            // Wait for half a second - coroutines special stuff ignore for now.
            yield return new WaitForSeconds(0.5f);
        }

        yield return StartCoroutine(UpdateCubes_ForeachLoop());
    }

    private IEnumerator UpdateCubes_ForeachLoop()
    {
        foreachLoopBox.SetActive(true);

        var counter = 0;

        // This is a foreach loop, used to repeat a block of code for each item in a list.
        // There is no need to keep track of the index, as the loop will automatically do that for you.
        Debug.Log("c04p04 :: Loops :: updated 5 cubes with a FOREACH loop, bravo.");
        foreach (GameObject cube in cubes)
        {
            var materialName = materials[counter]; 
            cube
                .GetComponent<MeshRenderer>()
                .SetMaterials(new List<Material> { Resources.Load<Material>($"Materials/{materialName}") });

            counter++;
            if (counter >= materials.Count)
            {
                counter = 0;
            }

            // Wait for half a second.
            yield return new WaitForSeconds(0.5f);
        }

        yield return StartCoroutine(GenerateCubes_WhileLoop());
    }

    private IEnumerator GenerateCubes_WhileLoop()
    {
        whileLoopBox.SetActive(true);

        Debug.Log("c04p04 :: Loops :: created 5 more cubes with a WHILE loop.");
        while (cubes.Count < 10)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.name = $"Just a Clone Cube_{cubes.Count}";
            cube.transform.parent = this.transform;
            cube.transform.position = new Vector3(offsetX, 0.5f, 0);
            cube.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            
            // Add the cube to the list of cubes.
            cubes.Add(cube);
            UpdateCubesCounterText();

            offsetX += 0.4f;

            // Wait for half a second.
            yield return new WaitForSeconds(0.5f);
        }

        yield return StartCoroutine(RemoveCubes_DoWhileLoop());
    }

    private IEnumerator RemoveCubes_DoWhileLoop()
    {
        doWhileLoopBox.SetActive(true);

        // This is a do-while loop, used to repeat a block of code while a condition is true.
        // The loop will automatically keep track of the index for you.
        // You need to specify the starting index, the end index, and the step.
        Debug.Log("c04p04 :: Loops :: removed 10 cubes with a DO-WHILE loop.");
        do
        {
            var lastCube = cubes[cubes.Count - 1];
            Destroy(lastCube);
            cubes.Remove(lastCube);
            UpdateCubesCounterText();

            // Wait for half a second.
            yield return new WaitForSeconds(0.5f);
        } while (cubes.Count > 0);

        Debug.Log("c04p04 :: Loops :: all done. Lets rewind.");

        yield return new WaitForSeconds(1f);
        ResetBoxes();
        yield return StartCoroutine(GenerateCubes_ForLoop());
    }

    private void ResetBoxes()
    {
        forLoopBox.SetActive(false);
        foreachLoopBox.SetActive(false);
        whileLoopBox.SetActive(false);
        doWhileLoopBox.SetActive(false);

        offsetX = -2;
    }

    private void UpdateCubesCounterText()
    {
        Counter.text = $"Cubes: {cubes.Count}";
    }
}
