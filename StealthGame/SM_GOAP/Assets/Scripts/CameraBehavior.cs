using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public float sensitivityX;
    public float sensitivityY;
    public Transform orientation;
    public Transform playerPosition;
    float rotationX;
    float rotationY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible= false;
    }

    // Update is called once per frame
    void Update()
    {
        // follow the player 
        this.transform.position = playerPosition.position;

        //get mouse input 
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;
        rotationX -= mouseY;
        rotationY += mouseX;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); //limit the rotation on the y axis

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f); // torate the camera
        orientation.rotation = Quaternion.Euler(0f, rotationY, 0f); //the game object should rotate as well 


    }
}
