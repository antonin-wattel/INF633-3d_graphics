using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSquareBrush : InstanceBrush {

    // TO COMPLETE

    public override void draw(float x, float z) {
        // choose a random position in the square
        float rx = Random.Range(x - radius, x + radius);
        float rz = Random.Range(z - radius, z + radius);

        // spawn the object at that position
        spawnObject(rx, rz);
    }
}