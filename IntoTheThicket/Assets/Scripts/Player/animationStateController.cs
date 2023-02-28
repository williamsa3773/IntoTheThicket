using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] playerController playerScript;
    int isForwardHash;
    int isBackwardHash;
    int isLeftHash;
    int isRightHash;
    int isSprintingHash;
    int isJumpingHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isForwardHash = Animator.StringToHash("isForward");
        isBackwardHash = Animator.StringToHash("isBackward");
        isLeftHash = Animator.StringToHash("isLeft");
        isRightHash = Animator.StringToHash("isRight");
        isSprintingHash = Animator.StringToHash("isSprinting");
        isJumpingHash = Animator.StringToHash("isJumping");
    }

    // Update is called once per frame
    void Update()
    {

        bool isSprinting = animator.GetBool(isSprintingHash);
        bool isForward = animator.GetBool(isForwardHash);
        bool isBackward = animator.GetBool(isBackwardHash);
        bool isLeft = animator.GetBool(isLeftHash);
        bool isRight = animator.GetBool(isRightHash);
        bool isJumping = animator.GetBool(isJumpingHash);
        bool forwardPressed = Input.GetKey("w");
        bool backwardPressed = Input.GetKey("s");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool sprintPressed = Input.GetKey("left shift");
        bool jumpPressed = Input.GetKey("space");

        //forward
        if (!isForward && forwardPressed && !rightPressed && !leftPressed)
            animator.SetBool(isForwardHash, true);

        if(isForward && !forwardPressed && !rightPressed && !leftPressed)
            animator.SetBool(isForwardHash, false);

        //forward left
        if (isForward && forwardPressed && rightPressed)
            animator.SetBool(isRightHash, true);

        if (isForward && forwardPressed && !rightPressed)
            animator.SetBool(isRightHash, false);

        //forward right
        if (isForward && forwardPressed && leftPressed)
            animator.SetBool(isLeftHash, true);

        if (isForward && forwardPressed && !leftPressed)
            animator.SetBool(isLeftHash, false);

        //backward
        if (!isBackward && backwardPressed)
            animator.SetBool(isBackwardHash, true);

        if (isBackward && !backwardPressed)
            animator.SetBool(isBackwardHash, false);

        //backward left
        if (isBackward && backwardPressed && rightPressed)
            animator.SetBool(isRightHash, true);

        if (isBackward && backwardPressed && !rightPressed)
            animator.SetBool(isRightHash, false);

        //backward right
        if (isBackward && backwardPressed && leftPressed)
            animator.SetBool(isLeftHash, true);

        if (isBackward && backwardPressed && !leftPressed)
            animator.SetBool(isLeftHash, false);

        //strafe left
        if (!isLeft && leftPressed)
            animator.SetBool(isLeftHash, true);

        if (isLeft && !leftPressed)
            animator.SetBool(isLeftHash, false);

        //strafe right
        if (!isRight && rightPressed)
            animator.SetBool(isRightHash, true);

        if (isRight && !rightPressed)
            animator.SetBool(isRightHash, false);

        //jumping
        if (!isJumping && playerScript.isGrounded == false)
            animator.SetBool(isJumpingHash, true);

        if (isJumping && playerScript.isGrounded == true)
            animator.SetBool(isJumpingHash, false);

        //sprinting
        if (!isSprinting && (forwardPressed && sprintPressed) && playerScript.canSprint == true)
            animator.SetBool(isSprintingHash, true);

        if (isSprinting && (!forwardPressed || !sprintPressed))
            animator.SetBool(isSprintingHash, false);
    }
}
