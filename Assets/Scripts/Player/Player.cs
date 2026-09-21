using UnityEngine;

public class Player : MonoBehaviour
{
    public enum GravityType{Normal, JumpHeld, Falling}
    public enum MovementState{ Grounded, Airborne, Gliding, Climbing }
    MovementState state = MovementState.Grounded;
    
    [SerializeField] Transform mainCamera;
    [SerializeField] Rigidbody body;
    [SerializeField] MovementProfile movement;
    
    InputController input;
    
    float lastJumpPressedTime = -99f, lastOnGroundTime = -99f;
    
    
    void Awake()
    {
        input = new InputController(mainCamera);
    }

    void Update()
    {
        input.GetInput();
        if (input.jumpPressed) lastJumpPressedTime =  Time.time;
    }

    void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;
        bool grounded = CastGround(out RaycastHit hit);
        if(grounded) lastOnGroundTime = Time.time;
        
        switch(state)
        {
            case MovementState.Grounded:
                if (!grounded) { state = MovementState.Airborne; break; }
                lastOnGroundTime = Time.time;

                if (TryConsumeJump()) { DoJump(); state = MovementState.Airborne; break; }

                ApplyGravity();
                ApplySpring(hit);
                MoveOnGround(delta);
                break;
            case MovementState.Airborne:
                if (TryConsumeJump()) { DoJump(); break; }
                if (grounded && body.linearVelocity.y <= 0f) { state = MovementState.Grounded; break; }
                ApplyAirGravity();
                MoveAirborne(delta);
                break;
            case MovementState.Gliding:
                break;
            case MovementState.Climbing:
                break;
        }
    }

    void MoveOnGround(float delta)
    {   
        //How much the joystick is pointing in the players current velocity
        Vector3 currentVelocity = body.linearVelocity;
        Vector3 currentHorizontalVelocity = new Vector3(currentVelocity.x, 0, currentVelocity.z);
        float acceleration = movement.groundAcceleration * movement.AccelFromDot.Evaluate(Vector3.Dot(currentHorizontalVelocity.normalized, input.moveDirection.normalized));

        //Velocity is a vector, speed is the scalar
        Vector3 targetVelocity = input.moveDirection * movement.groundSpeed;
        Vector3 currentVelocityDifference = targetVelocity - currentHorizontalVelocity;
        Vector3 forceToTarget = Vector3.ClampMagnitude(currentVelocityDifference/delta, acceleration);
        
        body.AddForce(forceToTarget, ForceMode.Acceleration);
    }
    
    void MoveAirborne(float delta)
    {   
        if (input.moveDirection.sqrMagnitude < 0.01f) return;
        Vector3 v = body.linearVelocity;
        Vector3 horiz = new Vector3(v.x, 0f, v.z);
        Vector3 wanted = input.moveDirection.normalized * Mathf.Max(horiz.magnitude, 1f);

        Vector3 steered = Vector3.RotateTowards(
            horiz, wanted,
            movement.AirTurnRate * Mathf.Deg2Rad * delta,
            movement.AirAcceleration * delta);

        body.linearVelocity = new Vector3(steered.x, v.y, steered.z);
    }

    bool CastGround(out RaycastHit hit)
    {
        return Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z), Vector3.down, out hit, movement.verticalFloatDistance + movement.extendedFloatDistance, movement.groundMask, QueryTriggerInteraction.Ignore);
    }

    void ApplySpring(in RaycastHit hit)
    {
        float springStretch = hit.distance - movement.verticalFloatDistance;
        float relativeVelocity = Vector3.Dot(Vector3.down, body.linearVelocity);
        float springAcceleration = springStretch * movement.springStrength - relativeVelocity * movement.springDampening;
        
        body.AddForce(Vector3.down * springAcceleration, ForceMode.Acceleration);
    }

    void ApplyGravity()
    {
        body.AddForce(Vector3.down * movement.gravity, ForceMode.Acceleration);
    }

    void ApplyAirGravity()
    {
        bool rising = body.linearVelocity.y > 0.0f;
        //Add clamping to avoid infinite speed gathering
        float gravity = (rising && input.jumpHeld) ? movement.gravity : movement.fallGravity;
        body.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
    }

    bool TryConsumeJump()
    {
        bool canJump = Time.time - lastOnGroundTime    <= movement.coyoteTime
                       && Time.time - lastJumpPressedTime <= movement.jumpBuffer;

        if (canJump) { lastJumpPressedTime = -99f; lastOnGroundTime = -99f; }
        return canJump;
    }

    void DoJump()
    {
        Vector3 velocity = body.linearVelocity;
        velocity.y = movement.jumpVelocity;
        body.linearVelocity = velocity;
    }
}
