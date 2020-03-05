using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is to control everything regarding the Hunter. It will call the methods from HunterController.

public class HunterCaller : MonoBehaviour
{
    public HunterController Controller;
    public float interval;
    private float intervalReset;
    public bool attacking = false;
    private int attackOption;
    public bool canAttack;
    public float PSwindUp;//Powerswipe windup time
    // Start is called before the first frame update
    void Start()
    {
        intervalReset = interval;
    }

    // Update is called once per frame
    void Update()
    {   //if not attacking, but can attack, wait until the cooldown ends.
        if(!attacking && canAttack){
            interval -= 1;
        }
        if(interval <= 0){
            attacking = true;
            interval = intervalReset;
        }
        if(attacking && canAttack){
            //call Powerswipe
            int choice = Random.Range(0,1); //guarantees Powerswipe option;
            switch(choice){
                default:
                    Controller.Powerswipe(PSwindUp);
                    break;
            }
            canAttack = false;
            //intention: this gets called only once, and only starts again once canAttack = true again;
        }
    }
}
