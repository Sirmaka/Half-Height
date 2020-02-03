using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
public bool canMove;
public Controller2D controller;
public float runSpeed = 40f;
float horizontalMove = 0f;
bool jump = false;
float vertSpeed;
Rigidbody2D rb;
public Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
    }

    // Update is called once per frame
    //Update is used to recieve input. FixedUpdate is used to execute input
    void Update()
    {
        JumpAnimation();
        //movement + animation
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        
        //jumping
        if(Input.GetButtonDown("Jump")){
            jump = true;
        }
        
    }   

    void FixedUpdate(){
        //move the character;
        if(canMove){
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        } else{
            controller.Move(0, false, false);
        }
        jump = false;
    }

    void JumpAnimation(){
        /*
        ** jump animation:
        */
        vertSpeed = rb.velocity.y;
        if(vertSpeed == 0){
            animator.SetBool("JumpDown", false);
            animator.SetBool("JumpUp", false);
        }
        if(vertSpeed > 0){
            animator.SetBool("JumpUp", true);
            animator.SetBool("JumpDown", false);
        }
        if(vertSpeed < 0){
            animator.SetBool("JumpDown", true);
            animator.SetBool("JumpUp", false);
        }
    }
}
