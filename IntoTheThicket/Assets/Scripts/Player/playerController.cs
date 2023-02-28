using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    [Header("----- Components -----")]
    [SerializeField] CharacterController controller;

    [Header("----- Movement Field -----")]
    [Range(5, 30)] [SerializeField] float playerSpeed;
    [Range(1,3)][SerializeField] float sprintSpeed;
    [Range(0, 1)] [SerializeField] float overEncumberedSpeed;
    [Range(0,50)] [SerializeField] int gravity;
    [Range(1, 3)] [SerializeField] int maxJumps;
    [Range(3, 50)] [SerializeField] int jumpSpeed;
    [SerializeField] bool isOverEncumbered;
    [SerializeField] public bool canSprint;
    [SerializeField] bool isSprinting;
    [SerializeField] public bool isGrounded;
    Vector3 moveInput;
    Vector3 playerVelocity;
    float defaultSpeed;
    public int jumpsCurrent = 0;
    int defaultJumps;


    [Header("----- Upgradable Stats -----")]
    [SerializeField] int health;
    [SerializeField] int stamina;
    [SerializeField] int strength;

    [Header("----- Mutable Stats -----")]
    [SerializeField] float maxWeight;
    [SerializeField] float currentWeight;


    // Start is called before the first frame update
    void Start()
    {
        defaultSpeed = playerSpeed;
        defaultJumps = maxJumps;
    }

    // Update is called once per frame
    void Update()
    {
        calculateWeight();
        movement();
        
    }

    void movement()
    {
        if(controller.isGrounded && playerVelocity.y <= 0)
        {
            playerVelocity.y = 0;
            jumpsCurrent = 0;
            isGrounded = true;
        }

        moveInput = (transform.right * Input.GetAxis("Horizontal")) + transform.forward * Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") && jumpsCurrent < maxJumps)
        {
            isGrounded = false;
            jumpsCurrent++;
            playerVelocity.y = jumpSpeed;
        }

        if (Input.GetButtonDown("Sprint") && !isSprinting && canSprint)
        {
            isSprinting = !isSprinting;
            playerSpeed = defaultSpeed * sprintSpeed;
        }
        else if (Input.GetButtonUp("Sprint") && isSprinting)
        {
            isSprinting = !isSprinting;
            playerSpeed = defaultSpeed;
        }

        if(!isOverEncumbered)
        {
            if(!isSprinting)
            {
                controller.Move(moveInput * Time.deltaTime * playerSpeed);
            }
            else
            {
                playerSpeed = defaultSpeed * sprintSpeed;
                controller.Move(moveInput * Time.deltaTime * playerSpeed);
            }
        }
        else
        {
            controller.Move(moveInput * Time.deltaTime * playerSpeed);
        }

        playerVelocity.y -= (gravity * Time.deltaTime);
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void calculateWeight()
    {
        maxWeight = 5 + (strength * 2.25f);

        if (currentWeight >= maxWeight)
        {
            isOverEncumbered = true;
            canSprint = false;
            maxJumps = 0;
            playerSpeed = defaultSpeed * overEncumberedSpeed;
        }
        else
        {
            isOverEncumbered = false;
            canSprint = true;
            maxJumps = defaultJumps;
        }
    }
}
