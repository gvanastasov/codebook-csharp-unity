using UnityEngine;

// Lets ignore this for now.
public class Vars : MonoBehaviour
{
    // This is a public variable.
    // This is a class scope variable.
    // This is serialized and can be set in the inspector.
    public int UniverseSecret = 42;

    [Header("Other Common Variables")]
    public float Speed = 1.0f;
    public string Name = "John Doe";
    public bool IsAlive = true;

    // Lets ignore this for now.
    private void Start()
    {
        Debug.Log("c02p01 :: Vars :: double click me to see the code! PS: " + UniverseSecret + " is the answer to life, the universe and everything.");

        // This is a local variable.
        // This is a method scope variable.
        // This is not serialized and can not be set in the inspector.
        // Because we don't really use this anywhere, but alocated space for it, both your IDE and 
        // Unity will warn you about it - don't waste your computer's memory!
        int localSecret = 42;
    }
}
