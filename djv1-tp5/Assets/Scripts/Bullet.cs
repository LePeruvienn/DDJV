using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	[Header("Statistics")] 
	[SerializeField] private int bulletDamage = 10;
	[SerializeField] private float bulletSpeed = 20f;
	[SerializeField] private float bulletMaxLifeTime = 5f;

	[Header("FX")]
	[SerializeField] private GameObject explosionPrefab;

	[Header("UI")]
	[SerializeField] private PlayerHUD playerHUD;

	private Rigidbody _rb = null;

	private GameObject _caster = null;

	protected void Start()
	{
		_rb = GetComponent<Rigidbody>();

		if (_rb == null)
		{
			Debug.LogError("Failed to get Rigidbody from bullet");
		}
		else
		{
			_rb.linearVelocity = transform.forward * bulletSpeed;
		}

		StartCoroutine(LifeTimeCoroutine());
	}

	IEnumerator LifeTimeCoroutine()
	{
		yield return new WaitForSeconds(bulletMaxLifeTime);

		Destroy(gameObject);
	}

	protected void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == _caster)
		{
			return;
		}
		
		if (other.gameObject.TryGetComponent<Entity>(out Entity entity))
		{
			bool isDead = entity.ApplyDamage(bulletDamage);

			if(isDead && _caster.CompareTag("Player"))
			{
				playerHUD.IncreaseKillCount();
			}
		}

		Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	public void SetCaster(GameObject caster)
	{
		_caster = caster;
	}
}
