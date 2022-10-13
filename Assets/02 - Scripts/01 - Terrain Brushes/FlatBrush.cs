using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatBrush : TerrainBrush {

    public override void draw(int x, int z) {
        for (int zi = -radius; zi <= radius; zi++) {
            for (int xi = -radius; xi <= radius; xi++) {
                terrain.set(x + xi, z + zi, 0);
            }
        }
    }
}
