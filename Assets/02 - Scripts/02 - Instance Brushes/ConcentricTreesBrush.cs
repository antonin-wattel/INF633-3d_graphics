using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcentricTreesBrush : InstanceBrush {

    public int numTrees = 10;
    public float radiusSkip = 1.0f;

    public override void draw(float x, float z) {

        for(float r = 0.0f; r <= radius; r+=radiusSkip) {

            if(r == 0.0f) spawnObject(x, z);
            else {
                float spacing = (2*Mathf.PI)/numTrees;
                for(float j = 0; j < (2*Mathf.PI); j+=spacing) {
                    float xi = x+(r*Mathf.Cos(j));
                    float zi = z+(r*Mathf.Sin(j));
                    spawnObject(xi, zi);
                }
            }

        }
    
    }
}