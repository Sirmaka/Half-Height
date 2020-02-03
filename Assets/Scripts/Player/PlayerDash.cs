using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb; //to make velocity = 0;
    public PlayerMovement moveScript; //to make canMove = false;
    public bool canDash;
    public float dashDuration;
    private float dashDurationReset;
    public float dashSpeed;
    private bool isDashing;
    public float dashCooldown;
    private float dashCooldownReset;
    private Vector2 dirFacing;

    // Start is called before the first frame update
    void Start()
    {
        canDash = true;
        dashDurationReset = dashDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if(canDash && Input.GetButtonDown("Fire3")){
            dirFacing = transform.localScale;
            moveScript.canMove = false;
            isDashing = true;
            canDash = false;
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
            rb.gravityScale = 1f;//turns on gravity
            animator.SetBool("Dashing", false);
            dashDuration = dashDurationReset;
            moveScript.canMove = true;
        }
        if(!canDash){
            dashCooldown -= 1;
        }
        if(dashCooldown < 0){
            canDash = true;
            dashCooldown = dashCooldownReset;
        }
    }
}
