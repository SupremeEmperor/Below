using UnityEngine;
using UnityEngine.AI;

public class EnemyMovemenScript : MonoBehaviour
{
    
    [SerializeField]
    NavMeshAgent agent;

    [SerializeField]
    LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    Vector3 walkPoint;
    bool walkPointSet = false;
    [SerializeField]
    float walkPointRange;

    //Attacking
    [SerializeField]
    float timeBetweenAttacks;
    bool hasAttacked;

    //States
    [SerializeField]
    float sightRange, attackRange;
    bool playerInSightRange, playerInAttackRange;
    

    

    [SerializeField]
    GameObject target;

    float waitTime = .5f;
    float lastTime;
    Vector3 lastPosition;

    //Awake
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        lastPosition = this.transform.position;
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();

        if(lastPosition == transform.position)
        {
            walkPointSet = false;
        }
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(target.transform);
    }

    private void ChasePlayer()
    {
        Debug.Log("Chaseing");
        agent.SetDestination(target.transform.position);
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPointSet = transform.position - walkPoint;

        if (distanceToWalkPointSet.magnitude < 1f)
            walkPointSet = false;
        Debug.Log("Patroling");
    }

    private void SearchWalkPoint()
    {
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y,
            transform.position.z + randomZ);

        //if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            //walkPointSet = true;
        walkPointSet = true;
    }
}
