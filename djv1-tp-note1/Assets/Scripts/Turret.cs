using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{
	[Header("Behaviour")]
	[SerializeField] private GameObject target;
	[SerializeField] protected float angularSpeed = 180f;

	[Header("Attack")]
	[SerializeField] private float attackDistance = 5f;
	[SerializeField] private float attackCooldown = 1f;
	[SerializeField] private int attackDamage = 1;

	[Header("Bullet")]
	[SerializeField] private float bulletPower = 1.5f;
	[SerializeField] protected Bullet bulletPrefab;

	[Header("Turret Parts")]
	[SerializeField] private Transform turretPivot;

	[Header("Sealed Door")]
	[SerializeField] private Destroyable sealedDoor = null;
	[SerializeField] private Material unlockedMat = null;

	private bool _canAttack = true;

	void Start()
	{
		if (sealedDoor != null)
			sealedDoor.SetIsImmune(true);
	}

	void OnDestroy()
	{
		if (sealedDoor != null)
		{
			sealedDoor.SetIsImmune(false);

			Renderer renderer = sealedDoor.GetComponent<Renderer>();

			if (renderer != null && unlockedMat != null)
			{
				renderer.material = unlockedMat;
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		float distance = Vector3.Distance(transform.position, target.transform.position);

		if (distance < attackDistance)
		{
			HandleRotation();
			HandleAttack();
		}
	}

	private void HandleRotation()
	{
		Vector3 targetPosition = target.transform.position;
		Vector3 position = transform.position;

		Vector3 directionToTarget = targetPosition - position;
		directionToTarget.y = 0;

		turretPivot.rotation = Quaternion.RotateTowards(
			turretPivot.rotation,
			Quaternion.LookRotation(directionToTarget),
			angularSpeed * Time.deltaTime);
	}

	private void HandleAttack()
	{
		if(_canAttack)
		{
			ShootBullet(bulletPower);
			_canAttack = false;
			StartCoroutine(WaitAttackCooldown());
		}
	}

	private void ShootBullet(float power)
	{
		Vector3 spawnPosition = transform.position;
		Quaternion spawnRotation = turretPivot.rotation;

		Bullet bullet = Instantiate(bulletPrefab, spawnPosition, spawnRotation);

		bullet.SetCaster(gameObject);
		bullet.SetPower(power);
		bullet.SetDamage(attackDamage);
	}

	private IEnumerator WaitAttackCooldown()
	{
		yield return new WaitForSeconds(attackCooldown);
		_canAttack = true;
	}
}
