using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal_Autonomous_collision : MonoBehaviour
{
    // Start is called before the first frame update
    // collision detection with animals
    void OnTriggerEnter(Collider other) {
    // Debug.Log("lake hit detected : " + other.gameObject.name);
        if (other.gameObject.name != "Terrain" && other.gameObject.name != "WaterBasicNightime") {
            Debug.Log("hit another animal : " + other.gameObject.name);
            // Destroy(other.gameObject);  
            // Debug.Log("animal destroyed"); 
        }
    }
}
