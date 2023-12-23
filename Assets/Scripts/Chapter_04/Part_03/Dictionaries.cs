using System.Collections.Generic;
using UnityEngine;

// Theres a lot of magic in this script. Quate a lot of it is not really how things should be done,
// but the main goal here is to actually understand how dictionaries work.
public class Dictionaries : MonoBehaviour
{
    // It is a collection of key-value pairs.
    // It has a dynamic size of elements.
    // It is not ordered.
    // It is not sorted.
    // It is not serializable (aka Unity will not save this in the scene - unless we do some other magic).
    public Dictionary<string, int> inventory = new Dictionary<string, int>();

    public GameObject kvpPrefab;

    private List<string> items = new List<string> { "Sword", "Shield", "Potion", "Boots", "Armor" };

    private float positionY = 2f;

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

        // This is how you itterate over key value pairs in a dictionary.
        // This will render the inventory in the scene.
        foreach (KeyValuePair<string, int> kvp in inventory)
        {
            // Let's leave this magic for later.
            GenerateKvpSceneElement(kvp);
        }
    }

    // This is special broadcasting mechanism for later.
    void OnMouseDown_Broadcast(EventEmitter emitter)
    {
        if (emitter.gameObject.name == "kvp_Add")
        {
            var key = emitter.OnMouseDown_EventData;

            // This is how you check if a dictionary contains a key.
            if (inventory.ContainsKey(key))
            {
                inventory[key] += 1;
                UpdateKvpText(new KeyValuePair<string, int>(key, inventory[key]));
            }
        }
        else if (emitter.gameObject.name == "kvp_Remove")
        {
            var key = emitter.OnMouseDown_EventData;

            if (inventory.ContainsKey(key))
            {
                inventory[key] -= 1;
                UpdateKvpText(new KeyValuePair<string, int>(key, inventory[key]));
            }

            if (inventory[key] <= 0)
            {
                // This is how you remove an element from a dictionary.
                inventory.Remove(key);
                Destroy(this.transform.Find(key).gameObject);
            }
        }
        else if (emitter.gameObject.name == "kvp_RandomButton")
        {
            var randomItem = items[Random.Range(0, items.Count)];
            var randomAmount = Random.Range(1, 10);

            if (inventory.ContainsKey(randomItem))
            {
                inventory[randomItem] += randomAmount;
                UpdateKvpText(new KeyValuePair<string, int>(randomItem, inventory[randomItem]));
            }
            else
            {
                inventory.Add(randomItem, randomAmount);
                GenerateKvpSceneElement(new KeyValuePair<string, int>(randomItem, randomAmount));
            }
        }
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

    private void GenerateKvpSceneElement(KeyValuePair<string, int> kvp)
    {
        var eventListener = this.GetComponent<EventListener>();

        var kvpGO = GameObject.Instantiate(kvpPrefab, new Vector3(0, positionY, 0), Quaternion.identity);
        kvpGO.name = kvp.Key;
        kvpGO.transform.parent = this.transform;
        kvpGO.GetComponentInChildren<TextMesh>().text = kvp.Key + " : " + kvp.Value;
        foreach (var eventEmitter in kvpGO.GetComponentsInChildren<EventEmitter>())
        {
            eventListener.eventEmitters.Add(eventEmitter);

            if (
                eventEmitter.gameObject.name == "kvp_Add" || 
                eventEmitter.gameObject.name == "kvp_Remove")
            {
                eventEmitter.OnMouseDown_EventData = kvp.Key;
            }
        }

        positionY -= 0.3f;
    }

    private void UpdateKvpText(KeyValuePair<string, int> kvp)
    {
        foreach (Transform child in this.transform)
        {
            if (child.gameObject.name == kvp.Key)
            {
                child.GetComponentInChildren<TextMesh>().text = kvp.Key + " : " + kvp.Value;
            }
        }
    }
}
