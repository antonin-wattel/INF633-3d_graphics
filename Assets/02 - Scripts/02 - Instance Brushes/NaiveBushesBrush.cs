using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaiveBushesBrush : InstanceBrush {
    // place random bushes of several objects within the square (very naive)

    public int bushCount = 10;
    public int bushRadius = 5;
    public int objectperBush = 5;
    
    public override void draw(float x, float z) {

        for (int i = 0; i < bushCount; i++) {

            // choose a random position in the square
            float cx = Random.Range(x - radius, x + radius);
            float cz = Random.Range(z - radius, z + radius);

            float rx = Random.Range(cx - bushRadius, cx + bushRadius);
            float rz = Random.Range(cz - bushRadius, cz + bushRadius);
            for (int j = 0; j < objectperBush; j++) {
                spawnObject(rx, rz);
            }
        }
    }
}