using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterController : MonoBehaviour
{
    public Animator Animator;
    public Animation anim;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Powerswipe(float windUp){
        //animation is handled here
        Animator.SetBool("Powerswipe", true);
        //freeze animation until windUp = 0;
        if(windUp > 0){
            windUp--;
        }

        
    }
}
