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
            Debug.Log("c03p01 :: Conditions :: OnMouseDown() :: x < 0 and the cube is on the LEFT side of the screen. Click me to see more.");
        }
        else
        {
            Debug.Log("c03p01 :: Conditions :: OnMouseDown() :: x >= 0 and the cube is on the RIGHT side of the screen. Click me to see more.");
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

    private string GetWeekdayNameByNumber(int number)
    {
        // Either the first condition is true, or the second one, or the third one, etc.
        // OR = ||
        if (number < 0 || number > 7)
        {
            Debug.LogError("c03p01 :: Conditions :: GetWeekdayNameByNumber() :: not a valid weekday number!");
        }

        // Switch is a special type of condition.
        // It checks if the value of the variable is equal to the value in the case.
        switch (number)
        {
            // If it is, it executes the code in the case.
            // If it is not, it skips the code in the case, and checks the next case.
            case 1:
                return "Monday";
            case 2:
                return "Tuesday";
            case 3:
                return "Wednesday";
            case 4:
                return "Thursday";
            case 5:
                return "Friday";
            case 6:
                return "Saturday";
            case 7:
                return "Sunday";
            // If no case is true, it executes the code in the default case.
            default:
                return "Invalid weekday number!";
        }
    }
}
