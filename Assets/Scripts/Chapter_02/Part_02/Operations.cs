using UnityEngine;

public class Operations : MonoBehaviour
{
    public int A = 32;

    public int B = 10;

    public string firstName = "John";

    public string lastName = "Doe";

    void Start()
    {
        Debug.Log("c02p02 :: Operations :: double click me to see the code! PS: " + A + " + " + B + " = " + (A + B));

        Debug.Log("Sum: " + (A + B));
        Debug.Log("Subtract: " + (A - B));
        Debug.Log("Multiply: " + (A * B));
        Debug.Log("Divide: " + (A / B));
        Debug.Log("Concatinate: " + firstName + " " + lastName);
    }
}
