using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public DoorOpener keyScript;

    private Vector3 startPos;

    public float posMaxY;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (keyScript.isActive)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }
    }

    void Activate()
    {
        if(transform.position.y < startPos.y + posMaxY)
        {

            transform.Translate(new Vector3(0, 2, 0) * Time.deltaTime , Space.Self);


        }


    }

    void Deactivate()
    {
        if(transform.position.y > startPos.y)
        {

            transform.Translate(new Vector3(0, -startPos.y, 0) * Time.deltaTime, Space.Self);

        }
        
    }

    private void OnDrawGizmos()
    {
        
    }
}
