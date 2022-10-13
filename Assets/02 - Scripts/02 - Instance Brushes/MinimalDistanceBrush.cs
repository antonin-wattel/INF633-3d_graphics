using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimalDistanceBrush : InstanceBrush {

    // randomly places objects in a square, but only if they are far enough from each other
    // (every placed object has > X free space)

    public int spacing = 10;
    public int maxSteepness = 5;
    public int maxHeight = 40;

    public override void draw(float x, float z) {
            
            //choose a random position in the square
            float rx = Random.Range(x - radius, x + radius);
            float rz = Random.Range(z - radius, z + radius);

            //prevent placement in too steep areas
            float steepness = terrain.getSteepness(rx, rz);
            if (steepness > maxSteepness) {
                draw(x, z);//redo again if too steep
                return;
            }

            //prevent placement in too high areas
            float height = terrain.getInterp(rx, rz);
            if (height > maxHeight) {
                draw(x, z);//redo again if too high
                return;
            }

            //make sure it is far enough from the other objects
            int num_objects = terrain.getObjectCount();
            for (int i=0; i<num_objects; i++) {
                Vector3 loc = terrain.getObjectLoc(i);
                if (Vector2.Distance(new Vector2(loc.x, loc.z), new Vector2(rx, rz)) < spacing) {
                    draw(x, z);//redo again if too close
                    return;
                }
            }

            // //change object depending on height 
            // //(very naive)
            // // TODO: use a distribution instead
            // int objectToSpawn;

            // if (height < 10) {
            //     objectToSpawn = object1;
            // }
            // else if (height < 20) {
            //     objectToSpawn = object2;
            // }
            // else {
            //     objectToSpawn = object3;
            // }


            
            Debug.Log("height: " + height + " steepness: " + steepness);
            Debug.Log("terrain.object_prefab: "+terrain.object_prefab.name);
            // Debug.Log("objectToSpawn: " + objectToSpawn);

            spawnObject(rx, rz);
    }
}