using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour {
    public float patrolTime = 10;
    public float aggroRange = 10;
    public Transform[] waypoints;
    private int index;
    private float speed, agentSpeed;
    private Transform player;
    private Animator animator;
    private NavMeshAgent agent;

    private void Awake() {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agentSpeed = agent.speed;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        index = Random.Range(0, waypoints.Length);

        InvokeRepeating("Tick", 0, 0.5f);

        if (waypoints.Length > 0) {
            InvokeRepeating("Patrol", Random.Range(0, patrolTime), patrolTime);
        }
    }

    private void Update() {
        speed = Mathf.Lerp(speed, agent.velocity.magnitude, Time.deltaTime * 10);
        animator.SetFloat("Speed", speed);
    }

    private void Patrol() {
        index = index == waypoints.Length - 1 ? 0 : index + 1;
    }

    private void Tick() {
        agent.destination = waypoints[index].position;
        agent.speed = agentSpeed / 2;

        if (player != null && Vector3.Distance(transform.position, player.transform.position) < aggroRange) {
            agent.speed = agentSpeed;
            agent.destination = player.position;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
