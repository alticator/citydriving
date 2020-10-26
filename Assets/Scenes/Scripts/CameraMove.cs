using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public Transform targetObject;
    public Vector3 offset;
    private Vector3 cameraPosition;

    public float followSpeed;
    public float rotationSpeed;

    // Update is called once per frame
    void FixedUpdate()
    {
        var targetPosition = targetObject.TransformPoint(offset);
        cameraPosition = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        transform.position = cameraPosition;

        var direction = targetObject.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation  = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
