using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb; //to make velocity = 0;
    public PlayerMovement moveScript; //to make canMove = false;
    public PlayerAttack attackScript;//to stop the player from attacking when dashing
    public bool canDash;
    public float dashDuration;
    private float dashDurationReset;
    public float dashSpeed;
    public bool isDashing;
    public float dashCooldown;
    private float dashCooldownReset;
    private Vector2 dirFacing;
    private bool once = true;

    // Start is called before the first frame update
    void Start()
    {
        //canDash = true;
        dashDurationReset = dashDuration;
        dashCooldownReset = dashCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if(canDash && Input.GetButtonDown("Fire3")){//if the button is pressed and canDash is true...
            dirFacing = transform.localScale;//force player to move only in the direction they were facing beforehand
            moveScript.canMove = false;//stop other inputs
            isDashing = true;
            canDash = false;//stop from dashing while dashing
            attackScript.canAttack = false;//stop from attacking while dashing
        }
        if(isDashing){
            animator.SetBool("Dashing", true);
            dashDuration -= Time.deltaTime;
            rb.velocity = Vector2.zero;//stop movement vertically and horizontally
            rb.gravityScale = 0f;//turns off gravity
            rb.velocity = new Vector2(dashSpeed*dirFacing.x, 0);//stop moving completely on the y axis, move forward on the x axis
        }
        if(dashDuration <= 0){
            isDashing = false;
            if(once)
                attackScript.canAttack = true;//allow attacking again
            once = false;   
            rb.gravityScale = 1f;//turns on gravity
            animator.SetBool("Dashing", false);
            moveScript.canMove = true;
            dashCooldown -= Time.deltaTime;
        }
        if(dashCooldown < 0){
            canDash = true;
            dashCooldown = dashCooldownReset;
            dashDuration = dashDurationReset;
            once = true; //to make sure canAttack = true only happens once
        }
    }
}
