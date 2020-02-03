using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    // Variable to determine scene life of bullet
    public float onscreenDelay = 3f;

    // Update is called once per frame
    void Update()
    {
        // Destroys bullet gameobject with a delay
        Destroy(this.gameObject, onscreenDelay);
    }
}
