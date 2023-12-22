using UnityEngine;

public class Conditions : MonoBehaviour
{
    // Unity reserved method name.
    // This method is called once you click on an object in Unity that has 
    // a collider (will talk about that later).
    void OnMouseDown()
    {
        // This is a CONDITION. It is a block of code that CHECKS something.
        // If the condition is true, the code inside the block is executed.
        // If the condition is false, the code inside the block is skipped.
        if (this.transform.position.x < 0)
        {
            Debug.Log("c03p01 :: Conditions :: OnMouseDown() :: x < 0 and the cube is on the LEFT side of the screen.");
        }
        else
        {
            Debug.Log("c03p01 :: Conditions :: OnMouseDown() :: x >= 0 and the cube is on the RIGHT side of the screen.");
        }
    }
}
