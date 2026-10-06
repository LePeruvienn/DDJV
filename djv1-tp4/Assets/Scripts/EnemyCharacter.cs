using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyCharacter : Entity
{
	[SerializeField] private GameObject player;
	[SerializeField] private EnemySpawner spawner;
	[SerializeField] private UIHealthBar healthbar;

	[SerializeField] private float attackCooldown = 1.0f;

	private NavMeshAgent _navMeshAgent;

	private LayerMask _raycastLayer;

	protected void Awake()
	{
		_navMeshAgent = GetComponent<NavMeshAgent>();

		_navMeshAgent.updateRotation = false;
		_navMeshAgent.angularSpeed = 0f;
 
		_raycastLayer = LayerMask.GetMask("Player", "Default");
	}

	protected void OnEnable()
	{
		StartCoroutine(MovementCoroutine());
		StartCoroutine(AttackCoroutine());
		StartCoroutine(RotationCoroutine());
	}

	public override bool ApplyDamage(int value)
	{
		UpdateHealthbar();

		return base.ApplyDamage(value);
	}

	public override void Die()
	{
		base.Die();
		spawner.EnemyCountDecrease();
	}

	private void UpdateHealthbar()
	{
		float healthPercent = (float) _health / maxHealth;
		healthbar.SetHealthBarStatus(healthPercent);
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

	IEnumerator RotationCoroutine()
	{
		yield return null;

		while(enabled)
		{
			if (player.activeInHierarchy)
			{
				RotateTowardsPlayer();
			}

			yield return null;
		}
	}

	IEnumerator AttackCoroutine()
	{
		yield return null;

		while(enabled)
		{
			if (player.activeInHierarchy && CanShootTowardsPlayer())
			{
				ShootBullet();
			}

			yield return new WaitForSeconds(attackCooldown);
		}
	}

	private void RotateTowardsPlayer()
	{
		Vector3 target = player.transform.position;
		Vector3 current = transform.position;

		target.y = 0;
		current.y = 0;

		Vector3 targetDirection = target - current;

		Vector3 rotation = Vector3.RotateTowards(transform.forward, targetDirection, angularSpeed * Time.deltaTime, 0f);

		transform.rotation = Quaternion.LookRotation(rotation);
	}

	private bool CanShootTowardsPlayer()
	{
		Vector3 direction = transform.forward;

		Vector3 origin = transform.position;
		origin.y += bulletHeight;

		RaycastHit hit;

		if (Physics.Raycast(origin, direction, out hit, Mathf.Infinity, _raycastLayer))
		{ 
			// Debug.DrawRay(transform.position, direction * hit.distance, Color.red); 
			return (hit.collider.gameObject == player);
		}
		
		// Debug.DrawRay(transform.position, direction * 1000, Color.white); 

		return false;
	}
}
