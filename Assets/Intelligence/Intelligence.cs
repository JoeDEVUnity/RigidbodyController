using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Intelligence : MonoBehaviour
{
    
    [Header("Wander Parameters")]
    private float timerBetweenWander;

    public float timeWanderSet, collisionDistance;

    public int currentWanderPoint;
    private GameObject activePoint;

    public List<GameObject> wanderPoints = new List<GameObject>();

    [Header("Agent Parameters")]

    private Vector3 startSinPos;
    public NavMeshAgent agent;
    private LaserScript laserScript;
    private Renderer materialRenderer;
    private Color passiveColor, awareColor, aggressiveColor;
    public float speed, amplitude, frequency;
    public bool inRange { get; private set; } 
    public bool isAware { get; private set; }

    [Header("Detection Parameters")]

    [SerializeField] private float checkRadius;
    public LayerMask playerLayer;
    public GameObject player;
    Movement playerScript;


    public GameObject main, objectOfRotation;
    public float speedOfRotation = 10f;

    private int startPoint;

    public StatsManager stats;

    // Start is called before the first frame update
    void Awake()
    {
        startPoint = currentWanderPoint;
        ComponentInstance();
    }

    // Function made to gather components
    void ComponentInstance()
    {
        startSinPos = main.transform.position + new Vector3(0, 4, 0);

        agent = GetComponent<NavMeshAgent>();

       // anim = main.GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Movement>();
        laserScript = this.GetComponentInChildren<LaserScript>();

    }

    

    // Update is called once per frame
    void Update()
    {
        timerBetweenWander += Time.deltaTime;
        Wander();
        Detections();

        // Calculate the new position based on the sine wave
        float time = Time.time * speed;
        float yOffset = amplitude * Mathf.Sin(2f * Mathf.PI * frequency * time);
        Vector3 newPosition = new Vector3(transform.position.x, 4 + yOffset, transform.position.z);

        // Move the object to the new position
        transform.position = newPosition;


        // HP CHECKER
        

        if (stats.currentHP <= 1)
        {
            playerScript.combatEnabled = false;
            Destroy(gameObject);

        }

    }

    void Wander()
    {
            if(wanderPoints.Count > 0)
            {
                activePoint = wanderPoints[currentWanderPoint];

                currentWanderPoint = Random.Range(0, wanderPoints.Count);
        
                if (timerBetweenWander > timeWanderSet)
                {

                    print("wanderPoint: " + activePoint);

                    Quaternion lookAt = Quaternion.LookRotation(activePoint.transform.position - transform.position);

                    Quaternion rotation = Quaternion.Slerp(transform.rotation, lookAt, Time.deltaTime * speedOfRotation);

                    transform.rotation = rotation;
                    //transform.LookAt(activePoint.transform.position, Vector3.forward);

                } 

                if(timerBetweenWander > timeWanderSet + .2f)
                {
                    timerBetweenWander = 0f;

                    agent.SetDestination(activePoint.transform.position);
                }
            }
        }


    void Detections()
    {
        inRange = Physics.CheckSphere(transform.position, checkRadius, playerLayer) && Vector3.Distance(this.transform.position, player.transform.position) > 8;
        isAware =  Vector3.Distance(transform.position, player.transform.position) < collisionDistance;

        

        if (isAware)
        {
            LookAt();
            playerScript.combatEnabled = true;
            if (inRange)
            {
                timerBetweenWander = 0f;
                //materialRenderer.material.color = aggressiveColor;
                agent.SetDestination(player.transform.position);
                
            }
            else
            {
                agent.velocity = Vector3.zero;
            }
        }
        else
        {
            // Instance a combat timer as the player is not in range,
            playerScript.combatTimer += Time.deltaTime;
            
        }

    }
    void LookAt()
    {
        Quaternion lookAt = Quaternion.LookRotation(player.transform.position - objectOfRotation.transform.position);

        Quaternion rotation = Quaternion.Slerp(objectOfRotation.transform.rotation, lookAt, Time.deltaTime * speedOfRotation);

        objectOfRotation.transform.rotation = rotation;
    }


    private void OnDrawGizmos()
    {
        foreach(GameObject wanderPoint in wanderPoints)
        {
            Gizmos.DrawWireSphere(wanderPoint.transform.position, 1f);  
        }

        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
    


}
