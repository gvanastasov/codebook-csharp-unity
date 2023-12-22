using UnityEngine;

public class Conditions : MonoBehaviour
{
    // Unity reserved method name.
    // This method is called once you click on an object in Unity that has 
    // a collider (will talk about that later).
    private void OnMouseDown()
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

    private string MagicWithDigits(int number)
    {
        // We can also have multiple blocks of code, each with its own condition.
        if (number > 9)
        {
            return "This is not a digit!";
        }
        else if (number < 0)
        {
            return "This also is not a digit!";
        }
        // If no condition is true, the code in the ELSE block is executed.
        else
        {
            // We can also have nested conditions.
            // We can also have multiple conditions in one block.
            if (number > 4 && number < 9)
            {
                return "This is a digit between 5 and 8!";
            }
            else if (number == 0)
            {
                return "This is a magic digit!";
            }
            else
            {
                return "This is a digit between 1 and 4!";
            }
        }
    }
}
