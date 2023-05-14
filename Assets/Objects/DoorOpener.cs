using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{

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
