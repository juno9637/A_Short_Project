using UnityEngine;

[CreateAssetMenu(fileName = "MovementProfile", menuName = "Scriptable Objects/MovementProfile")]
public class MovementProfile : ScriptableObject
{
    [Header("Ground")] 
    public float groundSpeed = 7f;
    public float groundAcceleration = 70f;
    public AnimationCurve AccelFromDot = AnimationCurve.Linear(-1f, 3f, 1f, 1f);
    
    public LayerMask groundMask;
    public float verticalFloatDistance = 1.3f - 0.8f;
    public float extendedFloatDistance = 0.6f;
    public float springStrength = 700f;
    public float springDampening = 0.1f;

    public float jumpHeight = 2.2f;
    public float timeToAPex= 0.18f;
    public float timeToFall= 0.08f;
    public float gravity => 2f * jumpHeight / (timeToAPex * timeToAPex);
    public float fallGravity => 2f * jumpHeight / (timeToFall * timeToFall);
    public float jumpVelocity => 2f * jumpHeight / timeToAPex;

    public float coyoteTime = 0.5f;
    public float jumpBuffer = 0.2f;

    public float AirTurnRate = 180f;
    public float AirAcceleration = 12f;
}
