using UnityEngine;

public class Test : MonoBehaviour
{
    private struct Weapon
    {
        // This is a constructor.
        // Structs can have constructors, but they cannot have destructors.
        public Weapon(string name, int damage)
        {
            this.name = name;
            this.damage = damage;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        private string name;
        private int damage;
    }

    void Awake()
    {
        var weapon = new Weapon("Sword", 10);
        Debug.Log($"Weapon name: {weapon.Name}, damage: {weapon.Damage}");
    }

    // Integers are actually structs, not classes.
    public int health = 100;

    // Booleans are actually structs, not classes.
    // Actually all primiteve types are structs, not classes.
    public bool isAlive = true;

    // Strings are the only special thing here, they are actually classes, not structs.
    // But they are immutable, so they behave like structs.
    public string Name = "Player";
}
