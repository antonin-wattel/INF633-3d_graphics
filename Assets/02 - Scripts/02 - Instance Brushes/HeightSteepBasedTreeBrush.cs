using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightSteepBasedTreeBrush : InstanceBrush {

    public override void draw(float x, float z) {

        float height = terrain.getInterp(x, z);
        float steepness = terrain.getSteepness(x, z);

        Debug.Log($"Height - {height}, Steepness - {steepness}");

    }
}