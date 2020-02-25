using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    public Animator animator;
    public PlayerMovement movement;
    public PlayerDash dash;
    public PlayerAttack attack;
    public bool canBlock;
    public bool isBlocking;
    public GameObject groundCheck;
    public bool onGround;
    public float groundCheckRadius = 0.2f; //same as in Controller2D
    [SerializeField] private LayerMask whatIsGround;	

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
        onGround = false;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, groundCheckRadius, whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)//check for below, or create new tag for walls etc
			{
				onGround = true;
			}
		}
        //determine if possible to block
        if(onGround && !dash.isDashing && !attack.attacking){
            canBlock = true;
        } else{
            canBlock = false;
        }
        //if on the ground, not blocking and pressing the block button
        Debug.Log(canBlock);
        if(canBlock && Input.GetButton("Fire2")){
            isBlocking = true;
            animator.SetBool("Blocking", true);
            setValues(false);
        }
        //stuff to happen when no longer blocking
        if(isBlocking && !Input.GetButton("Fire2")){
            isBlocking = false;
            setValues(true);
            animator.SetBool("Blocking", false);
        }
    }
    public void setValues(bool tf){
        if(tf){
            movement.canMove = true;
            dash.canDash = true;
            attack.canAttack = true;
            return;
        }
        movement.canMove = false;
        dash.canDash = false;
        attack.canAttack = false;
        
    }

}

