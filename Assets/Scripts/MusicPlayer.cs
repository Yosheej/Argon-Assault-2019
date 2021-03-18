using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    //makes music continue after loading the next scene
    void Awake()
    {
        int numOfMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        if(numOfMusicPlayers > 1)
        {
            Destroy(gameObject);
        }

        //destroys extra music players so it doesn't overlap
        else
        {
            DontDestroyOnLoad(gameObject);
        }    
    }
}
