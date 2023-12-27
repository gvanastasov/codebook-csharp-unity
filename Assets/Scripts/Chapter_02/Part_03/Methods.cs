using System.Collections;
using UnityEngine;

public class Methods : MonoBehaviour
{
    public int delay = 2;

    public GameObject cube;

    public TextMesh text;

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

        if (this.cube != null)
        {
            this.text.text = "Cube: " + (this.cube.transform.position.x < 0 ? "Left" : "Right");
            this.StartCoroutine(this.TeleportCube());
        }
    }

    private void MyPrivateMethod()
    {
        // This is a METHOD. It is a block of code that DOES something.
        // You can CALL this method from anywhere in this class.
        Debug.Log("MyPrivateMethod() called!");
    }

    private int MyPrivateMethodWithReturn()
    {
        // This method has a return TYPE and it returns an integer VALUE.
        return 42;
    }

    private void MyPrivateVoid()
    {
        // This method has a return TYPE and it returns NOTHING (aka void).
        Debug.Log("Silence is golden.");
    }

    private int Sum(int a, int b)
    {
        // This method has PARAMETERS. Parameters are variables that you pass to the method.
        // This method has TWO parameters, both of type integer.
        return a + b;
    }

    private int MySum(int a, int b)
    {
        // The a and b variables are PARAMETERS of this method.
        // But they are also passed to the Sum() method as ARGUMENTS.
        return this.Sum(a, b);
    }

    public void MyPublicMethod()
    {
        // This is a PUBLIC (scope) method. It can be called from anywhere in the project (almost).
        Debug.Log("MyPublicMethod() called!");
    }

    // This is a special one (coroutine). Ignore for now.
    private IEnumerator TeleportCube()
    {
        yield return new WaitForSeconds(this.delay);
        this.cube.transform.position = new Vector3(
            this.cube.transform.position.x * -1, 
            this.cube.transform.position.y, 
            this.cube.transform.position.y);

        this.text.text = "Cube: " + (this.cube.transform.position.x < 0 ? "Left" : "Right");
        
        yield return this.TeleportCube();
    }
}
