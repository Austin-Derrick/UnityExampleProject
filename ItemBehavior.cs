using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    // Creates variable to store reference of an attatched script
    public GameBehavior gameManager;

    void Start()
    {
        // Finds GameManager game object in scene
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }

    // OnCollsionEnter method triggers when two colliders interact, calls this method with a reference to the collided game object
    void OnCollisionEnter(Collision collision)
    {
        // If statment to check if the collided object is the player
        if (collision.gameObject.name == "Player")
        {
            // Destroys the parent object of the game object the script is attatched to
            Destroy(this.transform.parent.gameObject);            
            // Prints a simple message confirming objects interaction
            Debug.Log("Item Collected!");
            // Increments gameManagerItems by 1 when an item is collected
            gameManager.Items += 1;
        }
    }
}
