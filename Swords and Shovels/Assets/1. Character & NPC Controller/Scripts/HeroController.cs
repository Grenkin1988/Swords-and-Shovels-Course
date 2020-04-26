using UnityEngine;
using UnityEngine.AI;

public class HeroController : MonoBehaviour {
    private Animator animator;
    private NavMeshAgent agent;

    private void Awake() {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    public void SetDestination(Vector3 destination) {
        agent.destination = destination;
    }
}
