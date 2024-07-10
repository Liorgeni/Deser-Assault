using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    [Header("General Setup Settings")]
    [Tooltip("Ship speed movements")] [SerializeField] float controlSpeed = 30f;
    [Tooltip("How fast player movers horizontally")] [SerializeField] float xRange = 10f;
    [Tooltip("How fast player movers vertically")] [SerializeField] float yRange = 7f;

    [Header("Laser Guns")]
    [Tooltip("Add all lasers here")] [SerializeField] GameObject[] lasers;

    
    
    [Header("Screen Position Tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float postionYawFactor = 2f;

    [Header("Player Input Tuning")]
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float controlRollFactor = -20f;


    float xThrow, yThrow;
    
    void Start()
    {
        
    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotaion();
        ProcessFiring();

    }

    private void ProcessTranslation()
    {
         xThrow = Input.GetAxis("Horizontal");
         yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);


        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);




        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }


    private void ProcessRotaion()
    {
        float PitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float PItchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = PitchDueToPosition +  PItchDueToControlThrow;
        float yaw = transform.localPosition.x * postionYawFactor;
        float roll = xThrow * controlRollFactor;;

        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
    }

    private void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }



     void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

}
