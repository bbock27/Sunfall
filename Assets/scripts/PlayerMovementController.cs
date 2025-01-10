using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IsometricPlayerMovementController : MonoBehaviour
{

    private float movementSpeed = 3.5f;
    private float dashSpeed = 15f;
    private Vector2 dashDir = new Vector2 (0,0);
    private int dashCounter = 0;
    private float dashTime = 0.2f; //amount of time (in seconds) spent going the increased speed when dash button (space) is pressed
    private float dashCooldown = 1f; //amount of time (seconds) before you can dash again
    private bool isDashing;
    private bool canDash = true; //in case we want to limit when dashing can happen (for example if character is dashing, cannot dash)
    private bool isDashButtonPressed;
    private TrailRenderer tr;

    private InputSystem_Actions playerInputActions;
    
    IsometricCharacterRenderer isoRenderer;

    public Rigidbody2D rbody; 

    private void Awake()
    {
        playerInputActions = new InputSystem_Actions();
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
        tr = GetComponent<TrailRenderer>();
        tr.emitting = false;
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable();
    }

    private void FixedUpdate() //use FixedUpdate and not Update since rigidbody should be updated in FixedUpdate
    {
        isDashButtonPressed = playerInputActions.Player.Dash.IsPressed();
        
        Movement();

        if (isDashButtonPressed && canDash)
        {
            if(dashCounter == 0) { dashDir = playerInputActions.Player.Move.ReadValue<Vector2>(); }
            StartCoroutine(Dash());
        }
        
    }

    private void Movement()
    {
        Vector2 input = playerInputActions.Player.Move.ReadValue<Vector2>(); //unit vector by default
        Vector2 currentPos = rbody.position;
    
        Vector2 movement = new Vector2 (0,0);
        
        if (isDashing) 
        { 
            movement = dashDir * dashSpeed;
            dashCounter++;
        }
        else { movement = input * movementSpeed; } //want to always update drection of movement for this one

        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        isoRenderer.SetDirection(movement);
        rbody.MovePosition(newPos);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        tr.emitting = true;
        playerInputActions.Player.Move.Disable();//so cant change dash direction
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        tr.emitting = false;
        playerInputActions.Player.Move.Enable(); //end of dash, player can move again
        dashDir = new Vector2 (0,-1);
        dashCounter = 0;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

}
