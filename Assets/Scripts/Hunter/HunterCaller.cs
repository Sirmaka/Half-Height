using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is to control everything regarding the Hunter.

//TODO: flip()

public class HunterCaller : MonoBehaviour
{
    public Animator Animator;
    public Rigidbody2D Rigidbody;
    public float interval;
    private float intervalReset;
    public bool attacking = false;
    private int attackOption;
    public bool canAttack;
    public float PSwindUp;//Powerswipe windup time
    public float PSMove;
    public float PSSmoothTime;
    private bool Once = true;
    private bool Once2 = true;
    private bool Once3 = true;
    private int choice = 0;
    private Vector3 target = new Vector2();
    private Vector3 velocity = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        intervalReset = interval;
        
    }

    // Update is called once per frame
    void Update()
    {   //if not attacking, but can attack, wait until the cooldown ends.
        if (!attacking && canAttack)
        {
            interval -= 1 * Time.deltaTime;
        }
        if (interval <= 0)
        {
            attacking = true;
            interval = intervalReset;
        }
        if (attacking && canAttack)
        {
            //call Powerswipe
            if (Once)
            {
                choice = Random.Range(0, 1); //guarantees Powerswipe option;
                Once = false;//remember to reset
            }

            switch (choice)
            {
                default://Powerswipe
                    //animation is handled here
                    if (Once2)
                    {//might not be needed anymore
                        Animator.SetBool("Powerswipe", true);
                        Once2 = false;
                    }

                    if (PSwindUp > 0 && Animator.GetCurrentAnimatorStateInfo(0).IsName("HunterPowerSwipe")) //freeze animation until windUp = 0;
                    {
                        PSwindUp = PSwindUp - 1 * Time.deltaTime;//remember to reset
                        Animator.enabled = false; //hacky, but works?
                    }

                    if (PSwindUp <= 0) //start animation, move forward
                    {
                        Animator.enabled = true;
                        
                        if (Once3)
                        {
                            target = new Vector2(transform.position.x + PSMove, transform.position.y);
                            Once3 = false;
                        }
                        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, PSSmoothTime);
                        // Vector2 movement = new Vector2(PSMove, 0);
                        // Rigidbody.AddForce(movement, ForceMode2D.Impulse);
                        // //Vector2 movement = new Vector2(PSMove, 0);//negative placed here for testing, add code for moving in the right direction
                        // //Rigidbody.velocity = movement;
                    }
                    break;
            }

        }
    }
}

