using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyTank : MonoBehaviour
{
	[SerializeField] protected GameObject player;
	[SerializeField] protected float speed = 4f;

	[Header("UI")]
	[SerializeField] private GameUI gameUI;

	private NavMeshAgent _navMeshAgent;

	void Awake()
	{
		_navMeshAgent = GetComponent<NavMeshAgent>();

		_navMeshAgent.updateRotation = false;
		_navMeshAgent.angularSpeed = 0f;
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		StartCoroutine(MovementCoroutine());
	}

	void OnDestroy()
	{
		gameUI.ShowGameOver("Tu as gagné !!");
	}

	IEnumerator MovementCoroutine()
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
