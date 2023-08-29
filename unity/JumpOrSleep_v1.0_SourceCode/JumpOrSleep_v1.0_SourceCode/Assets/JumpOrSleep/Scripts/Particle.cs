using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {
    public GameObject player;
    public ParticleSystem particle1;
    public float speed = 2;
    public Vector3 velocity;
    float deltaX;
    bool inScene = true;

    void Awake()
    {
        particle1.Stop();
    }

	// Use this for initialization
	void Start () {    
        deltaX = Camera.main.orthographicSize * Screen.width / Screen.height;
        velocity = new Vector3(Random.Range(1, 5), Random.Range(1, 5), 0);
        velocity = velocity.normalized;
	}
	
	// Update is called once per frame
	void Update () {
        player.transform.Translate(velocity * speed * Time.deltaTime);
        
        if (player.transform.position.x > deltaX)
        {
            if (inScene)
            {
                velocity = Vector3.Reflect(velocity, Vector3.left);
                PlayParticle();
            }
        }
        else if (player.transform.position.x < -deltaX)
        {
            if(inScene)
            {
                velocity = Vector3.Reflect(velocity, Vector3.right);
                PlayParticle();
            }         
        }
        else if (player.transform.position.y > Camera.main.orthographicSize)
        {
            if (inScene)
            {
                velocity = Vector3.Reflect(velocity, Vector3.down);
                PlayParticle();
            }          
        }
        else if (player.transform.position.y < -Camera.main.orthographicSize)
        {
            if (inScene)
            {
                velocity = Vector3.Reflect(velocity, Vector3.up);
                PlayParticle();
            }           
        }
        else
        {
            inScene = true;
        }
        
    }

    void PlayParticle()
    {
        particle1.Stop();
        particle1.Play();
        inScene = false;
    }
}
