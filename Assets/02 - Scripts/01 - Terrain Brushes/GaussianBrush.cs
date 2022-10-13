using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaussianBrush : TerrainBrush {

    public float amplitude = 1;
    public bool upwards = true; 

    public override void draw(int x, int z) {

        int dir = 1;

        if (upwards) {
            dir = 1;
        } else {
            dir = -1;
        }

        for (int zi = -radius; zi <= radius; zi++) {
            for (int xi = -radius; xi <= radius; xi++) {
                float cur = terrain.get(x+xi, z+zi);
                float g = amplitude*Mathf.Exp(-(xi*xi + zi*zi)/(5*radius));
                terrain.set(x + xi, z + zi, cur + dir*g);               
            }
        }
    }
}

