using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    // Declares a Vector3 varaible to store the distance wanted between the Main Camera and the Player
    public Vector3 camOffSet = new Vector3(0, 1.2f, -2.6f);   
    // Varaible to hold the player capsule's Transform information
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        // Locates player capsule by name and retrieves its transform propery from the scene
        target = GameObject.Find("Player").transform;
    }
    // Method to ensure camera movement after player movement
    void LateUpdate()
    {
        // Sets camera position to target.TransformPoint(camOffSet)
        this.transform.position = target.TransformPoint(camOffSet);
        
        // Updates the capsules rotation every frame
        this.transform.LookAt(target);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
