using Lean.Pool;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LeanPooledRigidbody
{
    [SerializeField] [Tag] private string playerTag, bulletTag; 
    private NavMeshAgent _agent;

    protected override void Awake()
    {
        base.Awake();
        _agent = GetComponent<NavMeshAgent>();
        enabled = false; // disable movement at start
    }

    private void Update()
    {
        _agent.destination = GameManager.Instance.CurrentPlayer.PlayerPhase < PlayerController.Phase.Dying
            ? GameManager.Instance.CurrentPlayer.transform.position
            : transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            enabled = true; // enable movement and player chase when in range of vision
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(bulletTag))
        {
            enabled = true; // enable movement and player chase when being shot
        }
    }
}
