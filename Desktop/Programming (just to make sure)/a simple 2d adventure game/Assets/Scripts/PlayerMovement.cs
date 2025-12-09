using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputAction playerControls;
   public CharacterController2D controller;
   public Animator animator;


   public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;

    public void OnLanding()
    {
       animator.SetBool("New Bool", false);
   }

     public void IsCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
   }

   // Update is called once per frame;
    void Update()
    {

      horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

       animator.SetFloat("New Float", Mathf.Abs(horizontalMove));

       if (Input.GetButtonDown("Jump"))
       {
          jump = true;
            animator.SetBool("New Bool", true);
        }

   }

    void FixedUpdate()
   {
       // Move our character;
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
       jump = false;
   }

 }

