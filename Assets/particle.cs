using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle {
    public Vector3 pos;
    public int state;
    public int temp;
    public Color currentColor;
    public Color targetColor;
    public ParticleSystem.Particle particle; 
    

    public Particle(Vector3 pos, int state) {
        this.pos = pos;
        this.state = state;
        this.temp = state;
        
        particle = new ParticleSystem.Particle();
        particle.position = pos;
        particle.startColor = Color.black;
        currentColor = Color.white;
        particle.startSize = 0.05f;
    }


    //0 == empty, 1 == tree, 2 == fire
    // Use this for initialization
    public void Update() {
        state = temp;
        if (state == 0) {
            targetColor = Color.black;
        }
        if (state == 1) {
            targetColor = Color.green;
        }

        if (state == 2) {
            currentColor = Color.red;
            targetColor = Color.red;
        }
        currentColor = Color.Lerp(currentColor, targetColor, 0.1f);
        particle.startColor = currentColor;
    }
}
