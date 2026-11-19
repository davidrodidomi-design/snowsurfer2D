using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("PlayerController Movement Settings")]
    [SerializeField] private float boostMultiplier = 30f; // Multiplier for the torque applied to the player
    [SerializeField] private float baseSpeed = 15f; // Base speed of the player

    [Header("Score Manager")]
    [SerializeField] private ScoreManager scoreManager; // Reference to the ScoreManager script

    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenu; // Reference to the pause menu object

   

    Rigidbody2D rb;
    SurfaceEffector2D surfaceEffector2D;


    InputAction moveAction;
    Vector2 moveValue ; 
    int flipCount;
    float totalRotation;
    float previousRotation;

    float maxTorque = 500f; // Maximum torque value to limit the rotation speed


    private bool isPaused = false; // Flag to track the pause state


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Time.timeScale = 1f; // Ensure the game starts unpaused

        //actve the selected character sprite
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);
        transform.GetChild(0).GetChild(selectedCharacter).gameObject.SetActive(true);
        


        // Get the Move action from the Input System
        moveAction = InputSystem.actions.FindAction("Move");
    
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindAnyObjectByType<SurfaceEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current != null && 
        Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
            
        }



        RotatePlayer();
        BoostPlayer();
        CalculateFlips();
    }

    private void TogglePause()
    {
        isPaused = !isPaused; 

        pauseMenu.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f; 
        
       
    }

    private void CalculateFlips()
    {
        float currentRotation = transform.rotation.eulerAngles.z;
        totalRotation += Mathf.DeltaAngle(previousRotation, currentRotation);
        if(Mathf.Abs(totalRotation) >= 340f)
        {
            flipCount++;
            totalRotation = 0f; // Reset the total rotation after a flip
            scoreManager.UpdateScore(flipCount * 100);
        }
        previousRotation = currentRotation;
    }

    private void BoostPlayer()
    {
       

        if (moveValue.y > 0)
        {
            surfaceEffector2D.speed = boostMultiplier; // Apply boost when moving up
        }
        else
        {
            surfaceEffector2D.speed = baseSpeed; // Reset to base speed when not moving up
        }
    }

    private void RotatePlayer()
    {
        // Read the value of the Move action and store it in moveValue
        moveValue = moveAction.ReadValue<Vector2>();

        if (moveValue.x > 0)
        {
            rb.AddTorque(-2f); // Apply torque to rotate the player clockwise
            if (rb.angularVelocity < -maxTorque)
            {
                rb.angularVelocity = -maxTorque; // Limit the maximum clockwise rotation speed
            }
        }
        else if (moveValue.x < 0)
        {
            rb.AddTorque(2f); // Apply torque to rotate the player counterclockwise
            if (rb.angularVelocity > maxTorque)
            {
                rb.angularVelocity = maxTorque; // Limit the maximum counterclockwise rotation speed
            }
        }
    }
    
}
