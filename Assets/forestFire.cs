using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forestFire : MonoBehaviour {


    public int resolution = 24;
    public float density = 1f;
    public float vChance = 0.8f;
    public float eChance = 0.5f;
    public float sChance = 0.5f;
    public float iChance = 0.2f;
	int[,] testArr = new int[,]{{0,0,0,0,0,0},
							    {0,1,0,1,1,0},
							    {0,0,1,0,0,0},
							    {0,1,1,1,1,0},
							    {0,0,0,0,1,0},
							    {0,0,0,0,0,0}};
    private ParticleSystem.Particle[] points;
    private Particle[][] particles;
    
	// Use this for initialization
	void Start () {
		
					   
		print (test(1, 2, testArr));
        Initialize();

	}
    void Initialize() {
        points = new ParticleSystem.Particle[resolution * resolution];
        particles = new Particle[resolution][];

        float increment = (1f / density) / (resolution - 1);

        for (int i = 0; i < particles.Length; i++)
        {
            particles[i] = new Particle[resolution];
            for (int j = 0; j < particles[i].Length; j++)
            {
                particles[i][j] = new Particle(new Vector3((resolution * j * increment),
                                        (resolution * i * increment),
                                          0),
                                          0);

            }

        }

        for (int i = 1; i < particles.Length -1; i++) {
            for (int j = 1; j < particles.Length - 1; j++)
            {
                if (Random.value < vChance)
                {
                    particles[i][j].targetColor = Color.green;
                    particles[i][j].state = 1;

                }
                else {
                    particles[i][j].targetColor = Color.black;
                    particles[i][j].state = 0;
                }
            }
        }
    }

    void UpdateBoard() {
        for (int i = 1; i < particles.Length - 1; i++)
        {
            for (int j = 1; j < particles.Length - 1; j++)
            {
                if (particles[i][j].state == 1) {
                    float temp = 0;
					//print(NumOfBurningMoore(i,j, particles));
                    for (int x = 0; x < NumOfBurningMoore(i,j, particles); x++) {
                        temp += Random.value;
                        
                    }
                    if (temp > 1 - sChance) {
                        particles[i][j].state = 2;
                        particles[i][j].targetColor = Color.red;
                        print("hi");
                        continue;
                    }
                }
                if (particles[i][j].state == 1 && Random.value < iChance) {
                    particles[i][j].state = 2;
                    particles[i][j].targetColor = Color.red;
                }
                else if (particles[i][j].state == 2 && Random.value < eChance) {
                    particles[i][j].state = 3;
                    particles[i][j].targetColor = Color.gray;
                }
                else if (particles[i][j].state == 3 && Random.value < vChance) {
                    particles[i][j].state = 1;
                    particles[i][j].targetColor = Color.green;
                }
                
                particles[i][j].Update();
                
            }
        }

    }

	// Update is called once per frame
	void FixedUpdate () {
        UpdateBoard();
        points = Convert2D(particles);
        ParticleSystem sys = GetComponent<ParticleSystem>();
        sys.SetParticles(points, resolution * resolution);
		
	}

    private ParticleSystem.Particle[] Convert2D(Particle[][] arr2D) {
        ParticleSystem.Particle[] output = new ParticleSystem.Particle[arr2D.Length * arr2D.Length];
        int counter = 0;
        for (int i = 0; i < arr2D.Length; i++) {
            for (int j = 0; j < arr2D.Length; j++) {
                output[counter] = arr2D[i][j].particle;
                counter++;
            }
        }
        return output;
    }

    public int NumOfBurningMoore(int i, int j, Particle[][] arr) {
        int output = 0;
        for (int row = i - 1; row < i+2; row++) {
            for (int col = j - 1; col < j + 2; col++) {
                if (arr[i][j].state == 2) { 
                    output++;
                }
            }
        }
		
        return output;
    }
	public int test(int r, int c, int[,] arr) {
        int output = 0;
        for (int row = r - 1; row < r+2; row++) {
            for (int col = c - 1; col < c + 2; col++) {
                if (arr[row,col] == 1) { 
                    output++;
                }
            }
        }
		
        return output;
    }

}
