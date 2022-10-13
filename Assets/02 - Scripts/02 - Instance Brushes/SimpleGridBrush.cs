using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGridBrush : InstanceBrush {

    //TODO: 
    //- remove trees that are already in the space
    //- adapt the red thinng to the rotation angle

    public int grid_spacing = 10;
    public float rotation = 0;

    public override void draw(float x, float z) {

        for (int zi = -radius; zi <= radius; zi+=grid_spacing) {
            for (int xi = -radius; xi <= radius; xi+=grid_spacing) {
                //apply rotation matrix to the position around the center of the square
                float rx = x + xi * Mathf.Cos(rotation) - zi * Mathf.Sin(rotation);
                float rz = z + xi * Mathf.Sin(rotation) + zi * Mathf.Cos(rotation);
                spawnObject(rx, rz);
            }
        }
    }
}