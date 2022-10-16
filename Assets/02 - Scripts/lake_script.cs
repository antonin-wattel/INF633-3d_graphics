using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lake_script : MonoBehaviour
{
    // collision detection with animals
    void OnTriggerEnter(Collider other) {
    // Debug.Log("lake hit detected : " + other.gameObject.name);
        // if (other.gameObject.name == "Capsule") {
        Debug.Log("hit with animal : " + other.gameObject.name);
        // Destroy(other.gameObject);  
        //     Debug.Log("destroyed capsule"); 
        // }
    }
}
