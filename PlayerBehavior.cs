using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    // Players forward or backward speed
    public float moveSpeed = 10f;
    // Players left or right rotation speed
    public float rotateSpeed = 75f;
    // Variable to hold the amount of applied jump force
    public float jumpVelocity = 5f;
    // Variable to hold the distance to ground layer
    public float distanceToGround = 0.1f;
    // LayerMask Varaible for collider detection
    public LayerMask groundLayer;
    // Variable to store bullet pre-fab
    public GameObject bullet;
    // Variable to hold bullet speed
    public float bulletSpeed = 100f;
    // Variable to hold reference to GameBehavior script
    private GameBehavior _gameManager;

    // Stores vertical axis input
    private float vInput;
    // Stores horizontal axis input
    private float hInput;
    // Contains Player RigidBody information
    private Rigidbody _rb;
    // Variable to hold Player's capsule collider
    private CapsuleCollider _col;

    // Start is called before the first frame update
    void Start()
    {
        // Finds RigidBody component of the attatched GameObject
        _rb = GetComponent<Rigidbody>();
        // Finds CapsuleCollider component of attatched GaemObject
        _col = GetComponent<CapsuleCollider>();
        // Finds and returns GameBehvaior script on GameManager GameObject
        _gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        // Detects when Up, Down, W, or S keys are pressed and multiplies value by moveSpeed
        vInput = Input.GetAxis("Vertical") * moveSpeed;        
        // Detects when Left, Right, A and D keys are pressed and multiplies that value by rotateSpeed
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;
        // Checks for spacebar input and if the player is on the ground
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            // Uses rigid body force to deliver instant for to player object
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }
        /*
        // Uses Vector3 parameter to move the capsules transform component
        this.transform.Translate(Vector3.forward * vInput * Time.deltaTime);        
        // Uses vector3 parameter to rotaate the transform component
        this.transform.Rotate(Vector3.up * hInput * Time.deltaTime);
        */
    }    
    // Any physics || RigidBody-related code goes inside Fixedupdate
    void FixedUpdate()
    {
        // new Vector3 variable to store left and right rotation
        Vector3 rotation = Vector3.up * hInput;        
        // Takes a Vector3 and returns a rotation value in Euler angles
        // Quaterion value required to use MoveRotation method
        // Conversion to rotation type that Unity Prefers
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        // MoveRotation method takes in a Vector3 parameter and applies force accordingly:
        // Uses angleRot, which has horizontal input, and multiplies it by RigidBody rotation  
        _rb.MoveRotation(_rb.rotation * angleRot);
        // MovePosition method takes in a Vector3 parameter and applies force accordingly:
        // Uses Vector made up of: capsules transform position in the forward direction, multiplied by the vertical inputs and Time.DeltaTime
        _rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);        
        // Checks for mouse input
        if (Input.GetMouseButtonDown(0))
        {
            // creates local GameObject variable
            GameObject newBullet = Instantiate(bullet, _col.bounds.max, this.transform.rotation) as GameObject;
            // Returns and stores RigidBody on newBullet
            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
            // Sets the velocity property of the RigidBody to the players transform.forward direction multiplied by the bullet speed
            bulletRB.velocity = this.transform.forward * bulletSpeed;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            // Decrements player lives if collided by enemy
            _gameManager.Lives -= 1;
        }
    }
    // Method for checking player capsule collider distance from ground
    private bool IsGrounded()
    {
        // Vector3 Variable to hold lower bound of players capsule collider
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y);
        // Bool varaible checks the given capsule collider position's relationship to the given collider radius, and LayerMask colliders
        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);
        // returns true if player collider is on the ground and false if not
        return grounded;
    }
}
