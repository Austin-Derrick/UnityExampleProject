using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    // Variable to hold the player capsule's transform value
    public Transform player;
    // Variable to store empty GameObject Patrol_Route
    public Transform patrolRoute;
    // List to hold all child locations of Patrol_Route
    public List<Transform> locations;
    // Variable to track which patrol location enemy is currently at
    private int locationIndex = 0;
    // Variable to store the NavMeshAgent component of enemy
    private NavMeshAgent agent;
    // Variable to hold the enemies lives
    private int _lives = 3;
    // Property to access _lives
    public int EnemyLives
    {
        get { return _lives; }
        private set {
            _lives = value;
            if (_lives <= 0)
            {
                // Destroys gameObject if life is less then or equal to 0
                Destroy(this.gameObject);
                Debug.Log("Enemy down.");
            }
        }
    }
    void Start()
    {
        // returns a reference of the players transform information
        player = GameObject.Find("Player").transform;
        // assigns patrolRoute by finding empty GameObject Patrol_Route
        patrolRoute = GameObject.Find("Patrol_Route").transform;
        // Method call
        InitializePatrolRoute();
        // assigns agent to NavMeshAgent component of enemy
        agent = GetComponent<NavMeshAgent>();
        // Method call
        MoveToNextPatrolLocation();
    }
    void Update()
    {
        // if statment to check if the NaMeshAgent reached its destination
        if (agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            // Method call
            MoveToNextPatrolLocation();
        }
    }
    // Method to initialize patrol route
    void InitializePatrolRoute()
    {
        // Foreach to add all children of patrolRoute to locations List
        foreach (Transform child in patrolRoute)
        {
            // Adds child to list
            locations.Add(child);
        }
    }
    // Method to move enemy to the next patrol location
    void MoveToNextPatrolLocation()
    {
        // returns if the locations list is empty
        if (locations.Count == 0)
        {
            return;
        }        
        // assigns NavMeshAgents destination to position information of the indexed location
        agent.destination = locations[locationIndex].position;
        // increments the location by 1 and resets to 0 if reached the max number of locations
        locationIndex = (locationIndex + 1) % locations.Count;
    }

    // Method runs when a game object's collider enters a trigger
    // stores reference to trespassing object's collider component
    void OnTriggerEnter(Collider other)
    {
        // Checks if player collider enters trigger
        if (other.name == "Player")
        {
            // Changes the NavMeshAgnests destination to players position
            agent.destination = player.position;
            Debug.Log("Player Detected - attack!");
        }
    }
    // Method runs when game object's collider exits trigger
    // stores reference to trespassing object's collider component
    void OnTriggerExit(Collider other)
    {
        // Checks if player exits trigger
        if (other.name == "Player")
        {
            Debug.Log("Player out of range, resume patrol");
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            // Decrements enemy lives if hit by bullet
            EnemyLives -= 1;
            Debug.Log("Critical Hit!");
        }
    }
}
