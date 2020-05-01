using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale;
    public CharacterController controller;
    public Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();    
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // moveDir = new Vector3(Input.GetAxis("Horizontal")*moveSpeed, moveDir.y, Input.GetAxis("Vertical")*moveSpeed);
        
        // Transform.forward takes whatever direction is being faced forward (or backward)
        // transform.right takes whatever direction is being faces right (or left)
        // normalized normalizes the 2 vectors on either axis
        float ySave = moveDir.y;
        moveDir = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        moveDir = Vector3.ClampMagnitude(moveDir, 1) * moveSpeed;
        moveDir.y = ySave;           

        if (controller.isGrounded){
            moveDir.y = 0;
            if (Input.GetButtonDown("Jump")){
                moveDir.y = jumpForce;
            }
        }

        moveDir.y = moveDir.y + (Physics.gravity.y*gravityScale*Time.deltaTime);
        controller.Move(moveDir*Time.deltaTime);
    }
}
