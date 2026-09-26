using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{
	[SerializeField]
	private Transform playerTransform;

	[SerializeField]
	private float hitDistance = 0.2f;

	[SerializeField]
	private float deathDuration = 2f;

	[SerializeField]
	private float spawnDuration = 1f;

	private bool _isDead = false;

	private Coroutine _spawnCoroutine;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		_spawnCoroutine = StartCoroutine(SpawnCoroutine());
	}

	// Update is called once per frame
	void Update()
	{
		if (_isDead) return;

		Vector3 player = playerTransform.position;
		Vector3 target = gameObject.transform.position;

		float distance = Vector3.Distance(player, target);

		if (distance < hitDistance)
		{
			_isDead = true;
			StartCoroutine(DeathCoroutine());
		}
	}

	IEnumerator DeathCoroutine()
	{
		StopCoroutine(_spawnCoroutine);

		float timeElapsed = deathDuration;

		while(timeElapsed > 0f)
		{
			timeElapsed -= Time.deltaTime;

			gameObject.transform.localScale *= timeElapsed / deathDuration;

			yield return null;
		}
	}

	IEnumerator SpawnCoroutine()
	{
		float timeElapsed = spawnDuration;

		while(timeElapsed > 0f)
		{
			timeElapsed -= Time.deltaTime;

			gameObject.transform.localScale *= 1 - (timeElapsed / deathDuration);

			yield return null;
		}
	}
}
