using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    [Header("----- Components -----")]
    [SerializeField] CharacterController controller;

    [Header("----- Movement Field -----")]
    [Range(5, 30)] [SerializeField] int playerSpeed;
    [Range(0,50)] [SerializeField] int gravity;
    [Range(1, 3)] [SerializeField] int maxJumps;
    [Range(3, 50)] [SerializeField] int jumpSpeed;
    Vector3 moveInput;
    Vector3 playerVelocity;
    int defaultSpeed;
    public int jumpsCurrent = 0;

    // Start is called before the first frame update
    void Start()
    {
        defaultSpeed = playerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement()
    {
        if(controller.isGrounded && playerVelocity.y <= 0)
        {
            playerVelocity.y = 0;
            jumpsCurrent = 0;
        }

        moveInput = (transform.right * Input.GetAxis("Horizontal")) + transform.forward * Input.GetAxis("Vertical");

        controller.Move(moveInput * Time.deltaTime * playerSpeed);

        if(Input.GetButtonDown("Jump") && jumpsCurrent < maxJumps)
        {
            jumpsCurrent++;
            playerVelocity.y = jumpSpeed;
        }

        playerVelocity.y -= (gravity * Time.deltaTime);
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
