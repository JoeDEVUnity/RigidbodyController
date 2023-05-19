using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DoorOpener : MonoBehaviour
{

    public TMP_Text tmp;

    public bool isActive, isNeutral;

    private float stateTimer;
    public float timer;

    Renderer areaRenderer;

    private Color startColor;

    public enum DoorStates { Active, Neutral, CloseDoor }

    public DoorStates currentState;

    // Start is called before the first frame update
    void Start()
    {
        areaRenderer = GetComponent<Renderer>();
        startColor = areaRenderer.material.color;
        currentState = DoorStates.Neutral;
        isNeutral = true;
    }

    // Update is called once per frame
    void Update()
    {
        // If statements to determine between the active states

        

        if (isActive && !isNeutral)
        {
            currentState = DoorStates.Active;
        }
        else if(!isNeutral && !isActive)
        {
            currentState = DoorStates.CloseDoor;
        }
        else if(isNeutral)
        {
            currentState = DoorStates.Neutral;
            if(tmp != null)
            {
                tmp.text = "Drop the green key onto the platform.";
                tmp.color = Color.red;
            }
        }

        switch (currentState)
        {
            case DoorStates.Active:
                AllowAccess();
                break;
            case DoorStates.CloseDoor:
                CloseDoor();
                break;
            case DoorStates.Neutral:
                // Inactive Code
                areaRenderer.material.color = startColor;
                break;
        }
    }

    void AllowAccess()
    {
        // All code that allows access
        isNeutral = false;
        areaRenderer.material.color = new Color(.4f, 1f, .03f, .3f);
        
        if (tmp != null)
        {
            tmp.text = "<--- Door has opened!";
            tmp.color = Color.green;

        }

        stateTimer += Time.deltaTime;

        if(stateTimer > timer)
        {
            CloseDoor();
            stateTimer = 0f;
        }
    }

    void CloseDoor()
    {

        isNeutral = false;
        isActive = false;
        stateTimer += Time.deltaTime;

        areaRenderer.material.color = new Color(1f, .069f, .03f, .29f);

        if(stateTimer > timer)
        {
            isNeutral = true;
            stateTimer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.transform.tag == "Pickup")
        {
            isActive = true;
            isNeutral = false;
        }

    }



}
