using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShootManual : MonoBehaviour
{
    public bool objectPickup;
    public float maxPickupDist;


    public LayerMask pickupLayer;
    public Transform pickupPos;
    private GameObject heldObj;

    public Movement player;
    public RayScript playerRay;
    public TMP_Text tmp;


    public GameObject playerCam;

    public float pickupForce;

    Ray pickupRay;
    RaycastHit pickupInfo;
    Rigidbody heldRB;


    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Set Text for active mode
        tmp.text = "PICKUP MODE";


        playerRay.stateTimer += Time.deltaTime;

        pickupRay = new Ray(pickupPos.position, pickupPos.forward);
        // Material of ray will be yellow
        playerRay.rayMat.material.color = Color.yellow;
        // Create an instance of pickup particle and disintegrate after pickupTimer 
        playerRay.pickupParticle.gameObject.SetActive(player.fireValue > 0 && player.currentHeat > 0);


        objectPickup = Physics.Raycast(pickupRay, out pickupInfo, maxPickupDist, pickupLayer);

        if(player.fireValue > 0) // pickup obj every 3 seconds
        {

            if(heldObj == null)
            {
            
                if (objectPickup)
                {
                    // Pickup Object
                    PickupOBJ(pickupInfo.transform.gameObject);
                }

            }
            if(pickupInfo.transform != null)
            {
                // Move object

                MoveObject();
            }

        
        }   
        else if(heldRB != null)
        {
            DropOBJ();
            // Drop Object
        }


        void MoveObject()
        {
            if(Vector3.Distance(heldObj.transform.position, pickupPos.position) > 3f)
            {
                Vector3 moveDirection = (pickupPos.position - heldObj.transform.position);
                heldRB.AddForce(moveDirection * pickupForce);
            }
        }

        void PickupOBJ(GameObject pickupObj)
        {
            if (pickupObj.GetComponent<Rigidbody>())
            {
                heldRB = pickupObj.GetComponent<Rigidbody>();
                heldRB.useGravity = false;
                heldRB.drag = 10.0f;
                heldRB.constraints = RigidbodyConstraints.FreezeRotation;

                pickupObj.transform.parent = pickupPos;
                heldObj = pickupObj;
                
            }
        }

        void DropOBJ()
        {

            heldRB.useGravity = true;
            heldRB.drag = 1.0f;
            heldRB.constraints = RigidbodyConstraints.None;

            if(heldObj != null)
            heldObj.transform.parent = null;
            heldObj = null;
            
            
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(pickupPos.position, pickupPos.forward * maxPickupDist);
    }

}
