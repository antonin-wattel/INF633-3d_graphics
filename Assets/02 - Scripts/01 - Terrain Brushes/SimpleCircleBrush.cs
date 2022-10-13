using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCircleBrush : TerrainBrush {

    public float height = 15;

    public override void draw(int x, int z) {
        for (int zi = -radius; zi <= radius; zi++) {
            for (int xi = -radius; xi <= radius; xi++) {
                if (xi*xi + zi*zi <= radius*radius) {
                     // only draw if inside circle
                    terrain.set(x + xi, z + zi, height);
                }
            }
        }
    }
}
