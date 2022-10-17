using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAutomaticCollisions : MonoBehaviour
{
    // Start is called before the first frame update
    // collision detection with animals
    void OnTriggerEnter(Collider other) {
    // Debug.Log("lake hit detected : " + other.gameObject.name);
        if (other.gameObject.name != "Terrain" && other.gameObject.name != "WaterBasicNightime" && other.gameObject.name != "goalSphere") {
            // Debug.Log("snake hit by another animal : " + other.gameObject.name);
            Destroy(other.gameObject);  
            // Debug.Log(other.gameObject.name + " animal destroyed"); 
        }
    }
}
