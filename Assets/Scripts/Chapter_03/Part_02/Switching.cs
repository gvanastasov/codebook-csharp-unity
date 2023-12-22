using UnityEngine;

public class Switching : MonoBehaviour
{
    // This is just a variable that we will use to keep track of the color.
    public int ColorIndex = 1;

    void OnMouseDown()
    {
        // Switch is a special type of condition.
        // It checks if the value of the variable is equal to the value in the case.
        switch (ColorIndex)
        {
            // If it is, it executes the code in the case.
            // If it is not, it skips the code in the case, and checks the next case.
            case 1:
                GetComponent<Renderer>().material.color = Color.red;
                Debug.Log("c03p02 :: Switching :: OnMouseDown() :: cube is now red! Click to read more.");
                break;
            case 2:
                GetComponent<Renderer>().material.color = Color.green;
                Debug.Log("c03p02 :: Switching :: OnMouseDown() :: cube is now green! Click to read more.");
                break;
            case 3:
                GetComponent<Renderer>().material.color = Color.blue;
                Debug.Log("c03p02 :: Switching :: OnMouseDown() :: cube is now blue! Click to read more.");
                break;
            // If no case is true, it executes the code in the default case.
            default:
                Debug.LogError("c03p02 :: Switching :: OnMouseDown() :: this should never happen!");
                break;
        }

        // Lets increment the color index, so that on the next click we change the color.
        ColorIndex += 1;

        // Lets rotate back to 1 and start over.
        if (ColorIndex > 3)
        {
            ColorIndex = 1;
        }
    }
}
