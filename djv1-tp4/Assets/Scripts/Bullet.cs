using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private float bulletSpeed = 20f;
	[SerializeField] private GameObject explosionPrefab;

	private Rigidbody _rb = null;

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
	}

	void OnCollisionEnter(Collision other)
	{
		Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
