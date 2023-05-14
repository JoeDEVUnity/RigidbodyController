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

    public NavMeshAgent agent;
    private LaserScript laserScript;
    private Renderer materialRenderer;
    private Color passiveColor, awareColor, aggressiveColor;

    public bool inRange { get; private set; } 
    public bool isAware { get; private set; }

    [Header("Detection Parameters")]

    [SerializeField] private float checkRadius;
    public LayerMask playerLayer;
    public GameObject player;

    public GameObject main, objectOfRotation;
    public float speedOfRotation = 10f;

    private int startPoint;

   // private Animator anim;

    public StatsManager stats;


    [Header("Physical Values")]
    public float hpMax, currentHp;

    // Start is called before the first frame update
    void Awake()
    {
        startPoint = currentWanderPoint;
        ComponentInstance();
    }

    // Function made to gather components
    void ComponentInstance()
    {
        agent = GetComponent<NavMeshAgent>();

       // anim = main.GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");

        laserScript = this.GetComponentInChildren<LaserScript>();

        currentHp = hpMax;
        
    }

    

    // Update is called once per frame
    void Update()
    {
        timerBetweenWander += Time.deltaTime;
        Wander();
        Detections();
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

        // Animator values

        if(agent.velocity != Vector3.zero)
        {
            //anim.SetBool("isWalking", true);
        }
        else
        {
           // anim.SetBool("isWalking", false);
        }


        if (isAware)
        {
            LookAt();

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
