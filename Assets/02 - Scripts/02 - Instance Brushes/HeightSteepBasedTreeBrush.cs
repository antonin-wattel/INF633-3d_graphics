using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightSteepBasedTreeBrush : InstanceBrush
{
    public float height1 = 10f;
    public float height2 = 20f;
    public float height3 = 30f;
    public float steepness_max = 20f;

    public override void draw(float x, float z) {
        
        Debug.Log("x = " + x + " z = " + z);
        float curr_height = terrain.get(x, z);
        float curr_steepness = terrain.getSteepness(x, z);
        Debug.Log($"Height = {curr_height}, Steepness = {curr_steepness}");

        if((curr_height < height1) && (curr_steepness < steepness_max)) {
            spawnObjectCustom(x, z, 1);
        }
        else if((curr_height >= height1) && (curr_height < height2) && (curr_steepness < steepness_max)) {
            spawnObjectCustom(x, z, 0);
        }
        else if((curr_height >= height2) && (curr_height < height3) && (curr_steepness < steepness_max)) {
            spawnObjectCustom(x, z, 2);
        }
        else {
            // Don't plant anything
            Debug.Log("Too high up. Nothing can grow.");
        }
        
        

    }
}
