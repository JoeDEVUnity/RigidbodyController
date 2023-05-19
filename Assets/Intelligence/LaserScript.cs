using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    // Script to control instances of lasers shot out

    public GameObject bulletObj, bulletSpawn;

    private GameObject tempEmission;

    private float bulletTimer;
    public float bulletTimerSet;

    public GameObject player;
    public Intelligence intel;

    public float forceVelocity, yOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {

        bulletTimer += Time.deltaTime;

        if(intel != null)
        {
        if(intel.isAware)
        {


            if (bulletTimer > bulletTimerSet)
            {
                
                // instance bullet
                GameObject tempObj = Instantiate(bulletObj, bulletSpawn.transform.position, Quaternion.identity);

                tempObj.transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y + yOffset, player.transform.position.z));

                Rigidbody tempRb = tempObj.GetComponent<Rigidbody>();

                tempRb.AddForce(tempObj.transform.forward * forceVelocity, ForceMode.Impulse);
                Destroy(tempEmission);
                // set timer to 0

                bulletTimer = 0f;
            } 
        }

        }



    }
}
