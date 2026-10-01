using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class MovingSphere : MonoBehaviour
{
    InputSystem_Actions controls;
    [SerializeField] Transform mainCamera;
    [SerializeField, Range(0, 100)] float maxSpeed = 10f; //speed is a scalar quantity, also seen as the magnitude of a velocity
    [SerializeField, Range(0, 100)] float maxAcceleration = 10f; 
    private Vector3 velocity;
    
    [SerializeField]
    Rect allowedArea = new Rect(0f,0f, 10f, 10f);
    
    private void Awake()
    {
        controls = new InputSystem_Actions();
        controls.Player.Enable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 rawInput = controls.Player.Move.ReadValue<Vector2>();
        rawInput = Vector2.ClampMagnitude(rawInput, 1);
        
        Vector3 desiredVelocity = new Vector3(rawInput.x, 0, rawInput.y) * maxSpeed; //The velocity cap we want to reach based on input
        float maxSpeedChange = maxAcceleration * Time.deltaTime; //Per frame, how much we can change the velocity by
       
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
        
        Vector3 displacement = velocity * Time.deltaTime;
        Vector3 newPosition = transform.localPosition + displacement;
        if (!allowedArea.Contains(new Vector2(newPosition.x, newPosition.z)))
        {
            newPosition.x = Mathf.Clamp(newPosition.x, allowedArea.xMin, allowedArea.xMax);
            newPosition.z = Mathf.Clamp(newPosition.z, allowedArea.yMin, allowedArea.yMax);
            velocity.x = 0;
            velocity.z = 0;
        }
        
        transform.localPosition = newPosition;
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 center = new Vector3(allowedArea.center.x, transform.position.y, allowedArea.center.y);
        Vector3 size = new Vector3(allowedArea.width, 0f, allowedArea.height);

        Gizmos.DrawWireCube(center, size);
    }
}
