using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forestFireSimp : MonoBehaviour {
	
	public int resolution = 24;
    public float density = 1f;
	private ParticleSystem.Particle[] points;
    private Particle[][] particles;
	private bool play = false;
	int timer = 0;

    public float ignitionChance;
    public float vegetationDensity;
	void Start () {
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
        

    }
	void  UpdateBoard(){
        

        for (int i = 1; i < particles.Length - 1; i++)
        {
            for (int j = 1; j < particles.Length - 1; j++)
            {
                Particle cell = particles[i][j];


                int counter = 0;
                if (cell.state == 2)
                {
                    for (int row = i - 1; row < i + 2; row++)
                    {
                        for (int col = j - 1; col < j + 2; col++)
                        {
                            if (particles[row][col].state == 1)
                            {
                                particles[row][col].temp = 2;
                                counter++;
                            }
                        }
                    }
                   
                }
                if (cell.state == 2)
                {
                    cell.temp = 0; 
                    continue;

                }
                
                if (cell.state == 1 && Random.value < ignitionChance) {
                        cell.temp = 2;
                        
                        continue;

                    }


                
                if (cell.state == 0 && Random.value < vegetationDensity) {
                   
                    cell.temp = 1;
                    
                }

            

            }
        }
        for (int i = 1; i < particles.Length - 1; i++)
        {
            for (int j = 1; j < particles.Length - 1; j++)
            {
                Particle cell = particles[i][j];
                cell.Update();
            }

        }
            }
	void FixedUpdate () {
		
		if(Input.GetKeyUp("space"))
			play = true;
		if(play){
		timer++;
		if(timer % 2 == 1){
			UpdateBoard();
		}
		else{
			points = Convert2D(particles);
			ParticleSystem sys = GetComponent<ParticleSystem>();
			sys.SetParticles(points, resolution * resolution);
		}
		}
		
        
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
	public int Neighbourhood(int i, int j, Particle[][] arr) {
        int output = 0;
        //print(arr[10][10].state);
        for (int row = i - 1; row < i+2; row++) {
            for (int col = j - 1; col < j + 2; col++) {
                if (arr[i][j].state == 2) { 
                    output++;
                }
            }
        }
		
        return output;
    }
}
 