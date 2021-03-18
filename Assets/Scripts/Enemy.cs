using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 10;        
    [SerializeField] int hits = 5;

    ScoreBoard scoreBoard;

    // Start is called before the first frame update
    void Start()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>(); //adds a box collider at runtime
        boxCollider.isTrigger = false; //makes sure it isn't a trigger collider

        scoreBoard = FindObjectOfType<ScoreBoard>(); //gets a ScoreBoard class object
    }

    // Update is called once per frame
    void Update()
    {
        //put stuff here later 
    }

    //tells the enemies what to do when a particle collides
    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        
        if(hits <= 0)
        {
            KillEnemy();
        }
    }

    //called on particle collision, moderates how many hits an enemy takes
    void ProcessHit()
    {
        scoreBoard.ScoreHit(scorePerHit);
        hits--;
    }

    //called when a particle collides, kills the enenmy
    void KillEnemy()
    {   
        GameObject FX = Instantiate(deathFX, transform.position, Quaternion.identity); //instantiates effects when enemy gets hit by particle
        FX.transform.parent = parent; //sets parent

        Destroy(gameObject); //destroys the enemy ship
    }
}