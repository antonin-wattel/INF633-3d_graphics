using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightSteepBasedTreeBrush : InstanceBrush
{
    int radius = 30;
    float height = 0.0f;
    float steepness = 0.0f;

    public override void draw(float x, float z) {
        
        Debug.Log("x = " + x + " z = " + z);
        height = terrain.get(x, z);
        steepness = terrain.getSteepness(x, z);
        Debug.Log($"Height = {height}, Steepness = {steepness}");

        // hardcoded values for now //
        if (height < 10f && steepness < 20f) {
            spawnObjectCustom(x, z, 0);
        }
        // else if (height < 20f && steepness < 30f) {
        //     spawnObjectCustom(x, z, 1);
        // }
        else{
            // do nothing
            spawnObjectCustom(x, z, 1);
        }

        // some code for the distribution brush //
        // for (int zi = -radius; zi <= radius; zi=zi+10) {
        //     for (int xi = -radius; xi <= radius; xi=xi+10) {
        //         spawnObjectCustom(x+xi, z+zi, 0);
        //     }
        // }

    }
}
