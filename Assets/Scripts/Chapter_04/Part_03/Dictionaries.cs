using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dictionaries : MonoBehaviour
{
    // This is a DICTIONARY of strings and integers.
    // It has a dynamic size of elements.
    public Dictionary<string, int> inventory = new Dictionary<string, int>();

    void Start()
    {
        // This is how you add an element to a dictionary.
        Debug.Log("c04p03 :: Adding a new item to the inventory.");
        inventory.Add("Sword", 1);
        inventory.Add("Shield", 1);
        inventory.Add("Potion", 5);

        // This is how you get the length of a dictionary.
        Debug.Log("c04p03 :: The length of the dictionary is " + inventory.Count);

        // This is how you access an element in a dictionary.
        Debug.Log("c04p03 :: The first element at key 'Sword' is " + inventory["Sword"]);

        // This is how you change an element in a dictionary.
        inventory["Sword"] = 2;
        inventory["Shield"] = 2;
        inventory["Potion"] = 10;
    }

    public void ClearInventory()
    {
        // This is how you remove all elements from a dictionary.
        inventory.Clear();
    }

    public void RemoveItem(string item)
    {
        // This is how you remove an element from a dictionary.
        inventory.Remove(item);
    }
}
