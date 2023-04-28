using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManual : MonoBehaviour
{

    public RayScript playerRay;
    public Movement player;

    public float fireTimer;

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        fireTimer += Time.deltaTime;
        playerRay.stateTimer += Time.deltaTime;
        if (player.fireValue > 0 && fireTimer > .5)
        {
            playerRay.tempBullet = Instantiate(playerRay.bullet, playerRay.pointOut.transform.position, playerRay.pointOut.transform.rotation, null);
            playerRay.tempBullets.Add(playerRay.tempBullet);
            fireTimer = 0;
        }
    }
}
