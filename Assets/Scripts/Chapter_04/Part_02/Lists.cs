using System.Collections.Generic;
using UnityEngine;

public class Lists : MonoBehaviour
{
    // This is a LIST of GameObjects (special type, defined inside Unity).
    // It has a dynamic size of elements.
    // We have not initialized it, but unity does that internally for us. It will be empty at first.
    // We can add/remove/shift/etc. elements to it at runtime.
    public List<GameObject> cubes;
    
    public TextMesh Counter;

    public void Start()
    {
        // This is how you copy one list into another.
        // I will use this to later resptore the original state of the list.
        var cache = new List<GameObject>(cubes);

        // This is how you add an element to a list.
        Debug.Log("c04p02 :: Adding a new cube to the list.");
        cubes.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        
        // This is how you get the length of a list.
        Debug.Log("c04p02 :: The length of the list is " + cubes.Count);
        
        // This is how you access an element in a list.
        Debug.Log("c04p02 :: The first element at index 0 is " + cubes[0]);
        
        // This is how you access an element in a list (via index - a special property).
        cubes[1].transform.position = new Vector3(1, 1, 1);
        
        // This is how you remove an element from a list (and also from the scene, i don't 
        // want random cubes in my awasome scene)
        var cube1 = cubes[1];
        cubes.Remove(cube1);
        Destroy(cube1);

        // This is how you insert and element at a specific index.
        var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cubes.Insert(0, go);

        // This is how you remove an element at a specific index.
        cubes.RemoveAt(0);
        Destroy(go);

        // This is how you remove all elements from a list.
        cubes.Clear();

        // Restore the original state of the list.
        cubes = new List<GameObject>(cache);

        Counter.text = $"Cubes: {cubes.Count}";
    }

    // Special Unity function that is called when you are hovering an object (and some other conditions, but more later).
    void OnMouseOver()
    {
        // Left moust button clicked.
        if(Input.GetMouseButtonDown(0))
        {
            if (cubes.Count > 10)
            {
                Debug.Log("c04p02 :: There are too many cubes.");
                return;
            }
            else
            {
                AddCube();
                Counter.text = $"Cubes: {cubes.Count}";
            }
        }
        // Right moust button clicked.
        else if(Input.GetMouseButtonDown(1))
        {
            // Just some random logic I invented to keep the original cube.
            if (cubes.Count > 1)
            {
                RemoveLastCube();
            }
            else 
            {
                Debug.Log("c04p02 :: There are no cubes to remove. Well theres the Original Cube. Can't touch this.");
            }

            Counter.text = $"Cubes: {cubes.Count}";
        };
    }

    // Okay, so things went a bit wild here, but let's break it down.
    private void AddCube()
    {
        // Cache the LAST element, so we can get it's position and add a new cube next to it.
        // Lists (like arrays) are 0 indexed, therefore the last element is at index N-1, where
        // N is the LENGTH of the list.
        var lastCube = cubes[cubes.Count - 1];

        // Instantiate a new cube.
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        
        // Set the name of the cube.
        cube.name = $"Just a Clone Cube_{cubes.Count}";
        
        // Set the position of the cube with small offset on the X axis, compared to last cube.
        cube.transform.position = lastCube.transform.position + new Vector3(0.5f, 0, 0);

        // Copy the scale of the last cube. 
        // Some mindblowing here - localScale is a value type, so assignment will copy the value, not the reference.
        cube.transform.localScale = lastCube.transform.localScale;

        // Set the parent of the cube to this object (so it does not wonder in the wild wild world of Unity)
        cube.transform.parent = this.transform;
        
        // Make it pretty - ignore this for now.
        cube
            .GetComponent<MeshRenderer>()
            .SetMaterials(new List<Material> { Resources.Load<Material>("Materials/m_Red") });

        // And finally, why we are here - ADD an element to the list.
        cubes.Add(cube);
    }

    private void RemoveLastCube()
    {
        var target = cubes[cubes.Count - 1];
        
        // Remove the element from the list.
        cubes.Remove(target);

        // Destroy the game object.
        Destroy(target);
    }
}
