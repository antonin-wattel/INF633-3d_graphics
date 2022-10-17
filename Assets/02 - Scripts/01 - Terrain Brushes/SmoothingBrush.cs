using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothingBrush : TerrainBrush {

    public int kernel_size = 3;
    public float sigma = 1;
    public float[,] kernel = new float[3,3];
    

    public void compute_kernel() {
        // compute a gaussian kernel of size kernel_size and standard deviation sigma

        if (kernel_size != kernel.GetLength(0)) {
            kernel = new float[kernel_size, kernel_size];
        }
        float sum = 0;
        for (int i = 0; i < kernel_size; i++) {
            for (int j = 0; j < kernel_size; j++) {
                // print("yo - " + Mathf.Exp(-(i*i + j*j)/(2*sigma*sigma)));
                kernel[i, j] = Mathf.Exp(-(i*i + j*j)/(2*sigma*sigma));
                sum += kernel[i, j];
            }
        }
        for (int i = 0; i < kernel_size; i++) {
            for (int j = 0; j < kernel_size; j++) {
                kernel[i, j] /= sum;
            }
        }
    }
    

    public override void draw(int x, int z) {
        compute_kernel();
        //convolve the kernel on the square brush centered at x,y and radius radius
        for (int zi = -radius; zi <= radius; zi++) {
            for (int xi = -radius; xi <= radius; xi++) {
                float cur = terrain.get(x+xi, z+zi);
                float gauss = 0;
                for (int i = 0; i < kernel_size; i++) {
                    for (int j = 0; j < kernel_size; j++) {
                        gauss += kernel[i, j]*terrain.get(x+xi+i, z+zi+j);
                    }
                }
                terrain.set(x + xi, z + zi, gauss);
            }
        }
    }

}

