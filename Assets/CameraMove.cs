using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMove : MonoBehaviour
{

    public Transform orientation;

    public Movement playerScript;

    public float sens = 2f;

    public float multiplier = .5f;

    
    private float verticalRot;
    private float horizontalRot;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y + offsetY, player.transform.position.z);
        

        HandleRotation();

        transform.rotation = Quaternion.Euler(verticalRot,horizontalRot,0);

        orientation.transform.localRotation = Quaternion.Euler(verticalRot,horizontalRot, 0);

        
    }


    


    void HandleRotation()
    {
        float mouseX = playerScript.mouseInput.x;
        float mouseY = playerScript.mouseInput.y;

        horizontalRot += mouseX * sens * multiplier;
        verticalRot -= mouseY * sens * multiplier;

        verticalRot = Mathf.Clamp(verticalRot, -90, 90);




    }
}
