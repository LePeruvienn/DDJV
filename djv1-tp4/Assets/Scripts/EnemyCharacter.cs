using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyCharacter : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;

    protected void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected void OnEnable()
    {
        StartCoroutine(Coroutine());
        IEnumerator Coroutine()
        {
            yield return null;
            _navMeshAgent.enabled = true;

            while (enabled)
            {
                _navMeshAgent.SetDestination(new Vector3(
                    Random.Range(-12f, 12f),
                    0f,
                    Random.Range(-12f, 12f)));

                do yield return null;
                while (_navMeshAgent.hasPath);

                // Destination reached, wait before moving again.
                yield return new WaitForSeconds(3f);
            }
        }
    }
}
