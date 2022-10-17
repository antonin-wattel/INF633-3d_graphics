using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightSteepBasedTreeBrush : InstanceBrush
{
    public float height_max = 30f;
    public float steepness_max = 20f;

    public override void draw(float x, float z) {
        
        float height = terrain.get(x, z);
        float steepness = terrain.getSteepness(x, z);
        Debug.Log($"Height = {height}, Steepness = {steepness}");

        // hardcoded values for now //
        if ((height < height_max) && (steepness < steepness_max)) {
            spawnObjectCustom(x, z, 1);
        }
        // else if (height < 20f && steepness < 30f) {
        //     spawnObjectCustom(x, z, 1);
        // }
        else{
            // do nothing
            spawnObjectCustom(x, z, 0);
        }

        // some code for the distribution brush //
        // for (int zi = -radius; zi <= radius; zi=zi+10) {
        //     for (int xi = -radius; xi <= radius; xi=xi+10) {
        //         spawnObjectCustom(x+xi, z+zi, 0);
        //     }
        // }

    }
}