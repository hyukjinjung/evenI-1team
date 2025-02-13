using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetJumping(bool isJumping)
    {
        animator.SetBool("IsJumping", isJumping);
    }
}
