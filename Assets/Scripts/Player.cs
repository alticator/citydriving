using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody rigidbodyComponent;
    private float horizontalInput;
    private float verticalInput;
    private bool respawnRequest;
    private bool respawnToOriginRequest;

    private float steeringAngle;
    private bool brakes;
    private float brakeForce = 0f;

    [Header("Car Settings")]
    public float motorForce;
    public float maximumBrakeForce;
    public float maximumSteerAngle;

    [Header("Wheel Colliders")]
    public WheelCollider wheel1Collider;
    public WheelCollider wheel2Collider;
    public WheelCollider wheel3Collider;
    public WheelCollider wheel4Collider;

    [Header("Wheel Transforms")]
    public Transform wheel1Transform;
    public Transform wheel2Transform;
    public Transform wheel3Transform;
    public Transform wheel4Transform;

    [Header("Speedometer")]
    public Text speedometer;

    void Motor()
    {
        wheel1Collider.motorTorque = verticalInput * motorForce;
        wheel2Collider.motorTorque = verticalInput * motorForce;
        wheel3Collider.motorTorque = verticalInput * motorForce;
        wheel4Collider.motorTorque = verticalInput * motorForce;

        brakeForce = brakes ? maximumBrakeForce : 0f;
        wheel1Collider.brakeTorque = brakeForce;
        wheel2Collider.brakeTorque = brakeForce;
        wheel3Collider.brakeTorque = brakeForce;
        wheel4Collider.brakeTorque = brakeForce;
    }

    void Steer()
    {
        steeringAngle = horizontalInput * maximumSteerAngle;
        wheel1Collider.steerAngle = steeringAngle;
        wheel2Collider.steerAngle = steeringAngle;
    }
    
    void UpdateWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 position;
        Quaternion rotation;
        wheelCollider.GetWorldPose(out position, out rotation);
        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }

    void UpdateWheels()
    {
        UpdateWheel(wheel1Collider, wheel1Transform);
        UpdateWheel(wheel2Collider, wheel2Transform);
        UpdateWheel(wheel3Collider, wheel3Transform);
        UpdateWheel(wheel4Collider, wheel4Transform);
    }

    void HandleRespawn()
    {
        if (respawnRequest)
        {
            var carRotation = new Vector3(transform.rotation.x, transform.rotation.y, 0);
            transform.rotation = Quaternion.Euler(carRotation);
            rigidbodyComponent.velocity = new Vector3(0f, 0f, 0f);

        }
        else if (respawnToOriginRequest)
        {
            var carRotation = new Vector3(0, 90, 0);
            var carPosition = new Vector3(10, 1, -8);
            transform.position = carPosition;
            transform.rotation = Quaternion.Euler(carRotation);
            rigidbodyComponent.velocity = new Vector3(0f, 0f, 0f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.Space))
        {
            brakes = true;
        }
        else
        {
            brakes = false;
        }

        if (Input.GetKey(KeyCode.R))
        {
            respawnRequest = true;
        }
        else
        {
            respawnRequest = false;
        }

        if (Input.GetKey(KeyCode.B))
        {
            respawnToOriginRequest = true;
        }
        else
        {
            respawnToOriginRequest = false;
        }
        
        // Update Speedometer
        var kph = rigidbodyComponent.velocity.magnitude * 3.6;
        speedometer.text = kph.ToString("0") + " km/h";
    }

    void FixedUpdate()
    {
        HandleRespawn();
        Motor();
        Steer();
        // UpdateWheels disabled
        //UpdateWheels();
    }
}
