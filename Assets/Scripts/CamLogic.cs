using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLogic : MonoBehaviour
{
    public float rotSpeed = 1;
    public Transform target, player;
    private float mouseX, mouseY;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        mouseX += Input.GetAxis("Mouse X") * rotSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        target.rotation = Quaternion.Euler(mouseY, mouseX, 0);

        transform.LookAt(target); // default should be players head
        //player.rotation = Quaternion.Euler(0, mouseX, 0);
    }
}
