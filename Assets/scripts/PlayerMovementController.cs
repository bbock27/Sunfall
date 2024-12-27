using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IsometricPlayerMovementController : MonoBehaviour
{

    
    private float movementSpeed = 3.5f;

    private float dashSpeed = 15f;
    private float dashTime = 0.2f; //amount of time (in seconds) spent going the increased speed when dash button (space) is pressed
    private float dashCooldown = 1f; //amount of time (seconds) before you can dash again
    private bool isDashing;
    private bool canDash = true; //in case we want to limit when dashing can happen (for example if character is dashing, cannot dash)
    private bool isDashButtonPressed;
    private TrailRenderer tr;

    private InputSystem_Actions playerInputActions;
    
    IsometricCharacterRenderer isoRenderer;

    Rigidbody2D rbody;

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
            StartCoroutine(Dash());
        }
        
    }

    private void Movement()
    {
        Vector2 input = playerInputActions.Player.Move.ReadValue<Vector2>(); //unit vector by default
        Vector2 currentPos = rbody.position;
        //Debug.Log(input);

        float speed;
        if (isDashing) { speed = dashSpeed; }
        else {speed = movementSpeed; }

        Debug.Log("speed = " + speed);

        Vector2 movement = input * speed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        isoRenderer.SetDirection(movement);
        rbody.MovePosition(newPos);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        tr.emitting = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }


    // public float movementSpeed = 1f;

    // //variables for dash
    // //public TrailRenderer trailRenderer; //uncomment and use if/when trail renderer is set up
    // public float dashSpeed = 25f;
    // public float dashTime = 5f; //amount of time (in seconds) spent going the increased speed when dash button (space) is pressed
    // public bool isDashing;
    // public bool canDash = true; //in case we want to limit when dashing can happen (for example if character is dashing, cannot dash)

    // InputAction moveAction;

    // IsometricCharacterRenderer isoRenderer;

    // Rigidbody2D rbody;

    // private void Awake()
    // {
    //     rbody = GetComponent<Rigidbody2D>();
    //     isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    //     moveAction = InputSystem.actions.FindAction("Move");
    //     //trailRenderer = GetComponent<TrailRenderer>(); //uncomment and use if/when trail renderer is set up
    // }




    // // Update is called once per frame
    // void FixedUpdate()
    // {
    //     //bool isDashButtonPressed = Input.GetButtonDown("Dash"); //true if Dash button (spacebar) is pressed

    //     Vector2 currentPos = rbody.position;
    //     // float horizontalInput = Input.GetAxis("Horizontal");
    //     // float verticalInput = Input.GetAxis("Vertical");
    //     //Vector2 inputVector = new Vector2(horizontalInput, verticalInput); //direction character is facing
    //     Vector2 inputVector = moveAction.ReadValue<Vector2>();
    //     inputVector = Vector2.ClampMagnitude(inputVector, 1); //make direction a unit vector

    //     check if dashing before doing regular movement
    //     if(isDashButtonPressed && canDash)
    //     {
    //         Debug.Log("dash button pressed && canDash is true!");
    //         isDashing = true;
    //         canDash = false; //cant dash if already dashing
    //         //trailRenderer.emitting = true; uncomment for trail renderer

    //         StartCoroutine(stopDashing()); //starts the stopDashing coroutine, which controls the amount of time the player dashes for
    //     }

    //     if(isDashing) //if dashing, move the character with the same logic as normal, just with the faster move speed
    //     {
    //         Debug.Log("fr dashing rn");
    //         Vector2 movementDash = inputVector * dashSpeed;
    //         Vector2 newPosDash = currentPos + movementDash * Time.fixedDeltaTime;
    //         isoRenderer.SetDirection(movementDash);
    //         rbody.MovePosition(newPosDash);
    //         return; //return here so that if dashing, the player *only* dashes, no additional movement
    //     }
    //     //maybe want to add a short dash cooldown, but i just want to ge tthe dash itself working first
    //     //also probably want invincibility during the dash, but that can be implemented later

    //     //for now just set canDash to be true right here
    //     canDash = true;

    //     Vector2 movement = inputVector * movementSpeed;
    //     Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
    //     isoRenderer.SetDirection(movement);
    //     rbody.MovePosition(newPos);
    // }

    // private IEnumerator stopDashing()//coroutine to make sure the dash stops at the appropriate time
    // {
    //     //Debug.Log("start of stop dashing: isDashing = " + isDashing);
    //     yield return new WaitForSeconds(dashTime);
    //     //trailRenderer.emitting = false; //disable the dash trail
    //     isDashing = false; //not dashing anymore
    // }
}
