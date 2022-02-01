using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Rigidbody[] AllRigidbody;


    private void Awake()
    {
        DisableRagdoll(true);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            other.gameObject.SetActive(false);
            StartCoroutine(DieNPCAction(gameObject));
        }
    }

    private void DisableRagdoll(bool activ)
    {
        foreach (Rigidbody rigidbody in AllRigidbody)
            rigidbody.isKinematic = activ;
    }

    private IEnumerator DieNPCAction(GameObject NPC)
    {
        animator.enabled = false;
        DisableRagdoll(false);
        yield return new WaitForSeconds(2f);
        Destroy(NPC);
        yield break;
    }
}
