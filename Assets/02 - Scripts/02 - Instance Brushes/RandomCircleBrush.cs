using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCircleBrush : InstanceBrush {

    // TO COMPLETE

    public override void draw(float x, float z) {


        // choose a random position in the square
        float rx = Random.Range(x - radius, x + radius);
        float rz = Random.Range(z - radius, z + radius);

        // make sure it is in the circle
        if (Vector2.Distance(new Vector2(rx, rz), new Vector2(x, z)) < radius) {
            // spawn the object at that position
            spawnObject(rx, rz);
        }
        else {
            draw(x, z);//redo again if not in the circle
        }
    
    }
}