using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinBrush : TerrainBrush {

    //TO COMPLETE
    //https://docs.unity3d.com/ScriptReference/Mathf.PerlinNoise.html

    public float amplitude = 1;
    //add all params of the perlin brush

    public override void draw(int x, int z) {
        for (int zi = -radius; zi <= radius; zi++) {
            for (int xi = -radius; xi <= radius; xi++) {

                float cur = terrain.get(x+xi, z+zi);
                float h = amplitude * Mathf.PerlinNoise(xi, zi);// wrong -> might want to try with smaller stuff
                // h = h* amplitude;
                terrain.set(x + xi, z + zi, h);
            }
        }
    }
}

