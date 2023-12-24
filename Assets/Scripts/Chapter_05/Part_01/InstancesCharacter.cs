using UnityEngine;

// This is a public class definition. It is not a public class instance.
// This class inherits from MonoBehaviour, so it can be attached to a GameObject.
// Once attached, Unity actually creates an instance of this class and associates it with the GameObject.
// As a matter of fact, GameObjects are instances of the GameObject class too. Everything is an object.
// Welcome to OOP (Object Oriented Programming).
public class InstancesCharacter : MonoBehaviour
{
    // This reserved method is called when the GameObject is created, this is a Unity specific thing,
    // described in the life cycle of Objects in Unity. Unity will call this one every instance of the class.
    void Awake()
    {
        Debug.Log($"c05p01 :: Instances :: This is an instance of the {nameof(InstancesCharacter)} class, and the parent GameObject is {gameObject.name}");
    }

    // This is a nested class definition. It is not a nested class instance.
    // The nested class is not a MonoBehaviour, so it cannot be attached to a GameObject.
    private class Character
    { 
        private string name;
        private Vector4 color;

        // This is a CONSTRUCTOR - a special method that is called when an instance of the class is created.
        public Character(string name, Vector4 color)
        {
            this.name = name;
            this.color = color;
        }

        // This is a CONSTRUCTOR OVERLOAD - a special method that is called when an instance of the class is created.
        // This constructor overload calls the first constructor, passing in a default color.
        public Character(string name)
            : this(name, new Vector4(1, 1, 1, 1))
        {
        }

        public string Name 
        {
            get { return name; }
        }
    }

    void Start()
    {
        // This is an instance of the Character class.
        // It is created using the CONSTRUCTOR OVERLOAD that takes a single string parameter.
        // The instance we just created is a local variable, so it will be destroyed when the Start method ends.
        // The instance of the InstancesCharacter class will not be destroyed, because it is attached to a GameObject.
        Character character = new Character(name: this.gameObject.name);
        Debug.Log($"c05p01 :: Instances :: This is an instance of the {nameof(Character)} class, and the name is {character.Name}");

        // Class instances are passed by REFERENCE.
        // This means that the variable character2 is pointing (pointers) to the same instance as the variable character.
        var character2 = character;
        Debug.Log($"c05p01 :: Instances :: This is an instance of the {nameof(Character)} class, and the name is {character2.Name}");

        // This is an instance of the Enemy class.
        var enemy = new Enemy(name: "Enemy", weapon: "Sword");
        Debug.Log($"c05p01 :: Instances :: This is an instance of the {nameof(Enemy)} class, and the name is {enemy.Name} and the weapon is {enemy.Weapon}");

        // This is also an instance of the Character class, because the Enemy class inherits from the it.
        // This is called POLYMORPHISM, aka "many forms".
        var enemyCharacter = enemy as Character;

        // We no longer have access to the Weapon property, because it is not defined in the Character class.
        // enemyCharacter.Weapon will not compile.

        // We can cast the enemyCharacter variable back to an Enemy class, and then we will have access to the Weapon property again.
        // This is called DOWNCASTING.
        // This is a risky operation, because the enemyCharacter variable may not be an instance of the Enemy class.
        var enemy2 = (enemyCharacter as Enemy);
    }

    // This is a nested class definition.
    private class Weapon
    {
        private string name;

        public Weapon(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return name; }
        }
    }

    // This is yet another nested class definition.
    // This is yet another inherited class definition.
    // This class inherits from 
    private class Enemy : Character
    {
        // Class composition is a way to create a class that contains other classes.
        Weapon weapon;

        public Enemy(string name, Vector4 color, Weapon weapon)
            : base(name, color)
        {
            this.weapon = weapon;
        }

        public Enemy(string name, string weapon)
            : this(name, new Vector4(1, 0, 0, 1), weapon)
        {
        }

        public string Weapon
        {
            get { return weapon; }
        }
    }

    // This is a static class definition.
    // This class cannot be instantiated, because it is static.
    // This class cannot be inherited from, because it is static.
    private static class EnemyFactory
    {
        public static Enemy CreateEnemy(string name, string weapon)
        {
            return new Enemy(name, weapon);
        }
    }

    // This is an abstract class definition.
    // This class cannot be instantiated, because it is abstract.
    // This class can be inherited from, because it is abstract.
    // This class can have ABSTRACT methods, which MUST be implemented by the inherited class.
    private abstract class AbstractEnemy : Character
    {
        public AbstractEnemy(string name, Vector4 color)
            : base(name, color)
        {
        }

        // This is an abstract property.
        // This property MUST be implemented by the inherited class.
        public abstract string Weapon { get; }

        // This is an abstract method.
        // This method MUST be implemented by the inherited class.
        protected abstract void Attack();
    }

    // This is a sealed class definition.
    // This class cannot be inherited from, because it is sealed.
    // This class can be instantiated, because it is not abstract.
    private sealed class FinalEnemy : AbstractEnemy
    {
        private string weapon;

        public FinalEnemy(string name, string weapon)
            : base(name, new Vector4(1, 0, 0, 1))
        {
            this.weapon = weapon;
        }

        public override string Weapon
        {
            get { return weapon; }
        }

        protected override void Attack()
        {
            Debug.Log($"c05p01 :: Instances :: {nameof(FinalEnemy)} :: Attack");
        }
    }
}
