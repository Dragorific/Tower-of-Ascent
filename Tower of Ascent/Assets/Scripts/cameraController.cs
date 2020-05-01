using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public Transform pivot;

    public bool useOffset;
    public float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if (!useOffset){
            offset = target.position - transform.position;
        }

        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;

        // Lock the cursor to the center of the window
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Getting the X position of the mouse and rotating
        float horizontal = Input.GetAxis("Mouse X")*rotateSpeed;
        target.Rotate(0, horizontal, 0);

        // Getting the Y position of the mouse and rotating
        float vertical = Input.GetAxis("Mouse Y")*rotateSpeed*-1;
        pivot.Rotate(vertical, 0, 0);

        // Limiting the camera from rotating too far
        if(pivot.rotation.eulerAngles.x > 45f && pivot.rotation.eulerAngles.x < 180f){
            pivot.rotation = Quaternion.Euler(45f, 0, 0);
        }

        if(pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 350f){
            pivot.rotation = Quaternion.Euler(350f, 0, 0);
        }

        // Do some pre-built euler angle stuff to get the angle based on the rotation
        float desiredXAngle = pivot.eulerAngles.x;
        float desiredYAngle = target.eulerAngles.y;

        // Use the angle to create an object that can turn the angles into a position for the camera and apply it
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);

        // Lock the camera from going any lower
        if (transform.position.y < target.position.y){
            transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        }
        transform.LookAt(new Vector3(target.transform.position.x, target.transform.position.y + 1.0f, target.transform.position.z));
    }
}
