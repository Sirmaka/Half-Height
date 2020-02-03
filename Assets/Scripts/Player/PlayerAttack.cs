using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public PlayerMovement moveScript;
    public Animator animator;
    public PlayerDash dashScript;//so canDash is turned off
    public float attackTime;
    private float attackTimeReset;
    public bool attacking;
    public bool canAttack;
    private bool inAttackState;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    // Start is called before the first frame update
    void Start()
    {
        attackTimeReset = attackTime;
        canAttack = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetButtonDown("Fire1") && canAttack){
            animator.SetTrigger("Attacking");
            dashScript.canDash = false;
            canAttack = false;//stops player from attacking infinitely
        }
        //reset canMove by checking the state of the animation
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerSlash")){
            inAttackState = true;
        }else if(inAttackState){
            inAttackState = false;
            
            //moveScript.canMove = true;
        }
        //stopping the player from attacking infinitely
        if(!canAttack){
            attackTime -= 1;
        }
        if(attackTime <= 0){
            canAttack = true;
            dashScript.canDash = true;
            attackTime = attackTimeReset;   
        }
        
        //Detect Enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage enemies
        foreach(Collider2D enemy in hitEnemies){
            Debug.Log ("We hit " + enemy.name);
        }
    }
    
    void OnDrawGizmosSelected(){
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
