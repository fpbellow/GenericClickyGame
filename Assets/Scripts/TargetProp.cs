using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetProp : MonoBehaviour
{
    
    public ParticleSystem explosionParticle;
    public AudioClip targetSound;

    
    private Rigidbody targetRb;
    private GameManager gameManager;

    public int targetVal;
    
    private float minSpeed =12;
    private float maxSpeed =15;
    private float torqueRange =10;
    private float xRange = 4;
    private float ySpawnPos =-2;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);

        targetRb.AddTorque(RandomTorqe(),RandomTorqe(), RandomTorqe(),ForceMode.Impulse);
        transform.position = RandomSpawnPos();
        

    }


    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            gameManager.playTargetSound(targetSound);
            if (gameObject.CompareTag("PropBomb") && gameManager.spawnRate <= 1f)
            {
                gameManager.MissedTarget();
            }
            Destroy(gameObject);
            gameManager.UpdateScore(targetVal);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        } 
    }

 


    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("PropBomb"))
        {
            gameManager.MissedTarget();
        }
        
    }
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorqe()
    {
        return Random.Range(-torqueRange, torqueRange);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
