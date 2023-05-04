using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAuto : MonoBehaviour
{
    public RayScript playerRay;
    public Movement player;

    public float fireTimer, fireRate;
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        fireTimer += Time.deltaTime;
        playerRay.stateTimer += Time.deltaTime;
        if (player.fireValue > 0 && fireTimer > fireRate)
        {
            playerRay.tempBullet = Instantiate(playerRay.bullet, playerRay.pointOut.transform.position, playerRay.pointOut.transform.rotation, null);
            playerRay.tempBullets.Add(playerRay.tempBullet);
            fireTimer = 0;
        }
    }
}
