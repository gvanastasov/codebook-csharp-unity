using UnityEngine;

public class Methods : MonoBehaviour
{
    // Unity reserved method name.
    // Start is called before the first frame update, aka when you hit PLAY button.
    void Start()
    {
        // You see this because we have added this script to a game object in the scene
        // and Unity created an instance of this class (lets ignore these words for now)
        // and Unity called this method for us. 
        Debug.Log("c02p03 :: Methods :: double click me to see the code!");

        // This is how you call a method.
        // You can call this method from anywhere in this class (because, shhtt, its private).
        // Notice the keyword "this" before the method name. Signals that this method belongs to this class,
        // and only instances of this class can call this method.
        this.MyPrivateMethod();
    }

    private void MyPrivateMethod()
    {
        // This is a method. It is a block of code that does something.
        // You can call this method from anywhere in this class.
        Debug.Log("MyPrivateMethod() called!");
    }
}
