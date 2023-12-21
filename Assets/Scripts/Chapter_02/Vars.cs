using UnityEngine;

public class Vars : MonoBehaviour
{
    // This is a public variable.
    // This is serialized and can be set in the inspector.
    public int UniverseSecret = 42;

    // This is a private variable.
    // This is not serialized and cannot be set in the inspector.
    // private int UniverseSecret_Private = 42;

    // This is a static variable.
    // This is not serialized and cannot be set in the inspector.
    // public static int UniverseSecret_Static = 42;

    void Start()
    {
        Debug.Log("c02p01 :: Vars :: double click me to see the code! PS: " + UniverseSecret + " is the answer to life, the universe and everything.");
    }
}
