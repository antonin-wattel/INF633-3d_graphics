using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinBrush : TerrainBrush {

    public float h = 0.1f;
    public float s = 1.0f;
    public float o = 0.0f;
    public int maxN = 10;

    public override void draw(int x, int z) {

        for (int zi = -radius; zi <= radius; zi++) {
            for (int xi = -radius; xi <= radius; xi++) {
                
                float rand_max = Random.Range(0, maxN);
                float perlin = 0;
                for(int i = 0; i < rand_max; i++){
                    perlin += Mathf.PerlinNoise(s*((x+xi)+o), s*((z+zi)+o));
                }

                float height = h * perlin;

                terrain.set(x + xi, z + zi, terrain.get(x + xi, z + zi) + height);

            }
        }
    }
}
