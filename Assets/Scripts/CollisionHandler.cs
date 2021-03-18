using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("In Seconds")][SerializeField] float levelLoadDelay = 2f;
    [Tooltip("Special effects played on death.")][SerializeField] GameObject deathFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider collider) //processes whether the player triggered (ie. touched) a game object
    {
        StartDeathSequence();
        deathFX.SetActive(true);
        Invoke("ReloadScene" , levelLoadDelay);
    }
    
    //sends a message to the method in Player.cs to activate
    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
    }

    //reloads the scene when you die
    private void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }
}