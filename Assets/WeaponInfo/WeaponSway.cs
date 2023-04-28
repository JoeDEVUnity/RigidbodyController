using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{

    public Movement player;

    [Header("Sway Parameters")]
    public float smooth;
    public float swayMultiplier;

    public float sinMultipler;


    private void Awake()
    {

    }
    private void Update()
    {

        // Get input
        float mouseX = player.mouseInput.x * swayMultiplier;
        float mouseY = player.mouseInput.y * swayMultiplier;

        // Calculate (X)_ rotation

        Quaternion rotateX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotateY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotateX * rotateY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }

}
