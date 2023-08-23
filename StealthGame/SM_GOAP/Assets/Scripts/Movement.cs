using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;
    bool accelerate;
    Vector3 moveDir;
    Rigidbody rigidbody;
    public float StartingSpeed = 10f;
    public Transform orientation;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody= GetComponent<Rigidbody>();
        rigidbody.freezeRotation= true;
        rigidbody.drag = 1f;
    }       
    
    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            accelerate= true;
        }
        else
        {
            accelerate= false;
        }
        //Debug.Log(accelerate);
    }
    private void FixedUpdate()
    {
        moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        float adjustedSpeed = accelerate? StartingSpeed + 5: StartingSpeed;
        rigidbody.AddForce(moveDir.normalized * adjustedSpeed, ForceMode.Force);
    }
}
