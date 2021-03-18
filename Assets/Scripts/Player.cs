using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour //this script is for moving the player, I would rename it but it's too much work :/
{
    [Header("General Components")]
    [Tooltip("in ms^-1")][SerializeField] float Speed = 4f;
    [Tooltip("in ms")][SerializeField] float xRange = 8f;
    [Tooltip("in ms")][SerializeField] float yRange = 3f;
    [SerializeField] GameObject[] guns;

    [Header("Screen Position Based")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = -2.5f;

    [Header("Throw Control Related")]
    [SerializeField] float controlPitchFactor = 20f;
    [SerializeField] float controlRollFactor = -5f;
    

    float xThrow, yThrow;
    bool isControlEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isControlEnabled)
        {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
        }
    }

    //called when the player dies
    void OnPlayerDeath()
    {
        isControlEnabled = false;
    }

    //Processes movement to the ship
    void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
    
        float xOffset = xThrow * Speed * (Time.deltaTime * 5);
        float yOffset = yThrow * Speed * (Time.deltaTime * 5);

        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange); //clamps position on the x-axis so player can't move beyond borders

        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange); //clamps position on the y-axis so player can't move beyond borders
        
        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z); //transforms players position according to the clamped positions and the z position
    }

    //Processes rotation to the ship
    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow; //determines the pitch

        float yaw = transform.localPosition.x * positionYawFactor; //determines the yaw

        float roll = xThrow * controlRollFactor; //determines the roll

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll); //rotates ship based on pitch, yaw and roll using a Quaternion
    }

    //Processes gun-firing in the ship
    void ProcessFiring()
    {
        if(CrossPlatformInputManager.GetButton("Fire"))
        {
            ActivateGuns();
        }
        else
        {
            DeactivateGuns();
        }
    }

    void ActivateGuns()
    {
        foreach(GameObject gun in guns)
        {
            gun.SetActive(true);
        }
    }

    void DeactivateGuns()
    {
        foreach(GameObject gun in guns)
        {
            gun.SetActive(false);
        }
    }
}