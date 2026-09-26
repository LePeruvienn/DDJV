using UnityEngine;
using System.Collections;

public class TargetSpawner : MonoBehaviour
{
	[SerializeField]
	private GameObject originalTarget;

	[SerializeField]
	private float maxSpawnTime = 3f;

	[SerializeField]
	private float minSpawnTime = 1f;

	[SerializeField]
	private float spawnRadius = 5f;

	private bool _canSpawnEntity = true;

	// Update is called once per frame
	void Update()
	{
		if(_canSpawnEntity)
		{
			SpawnEntity();
		}
	}

	void SpawnEntity()
	{
		float x = Random.Range(-spawnRadius, spawnRadius);
		float z = Random.Range(-spawnRadius, spawnRadius);

		Vector3 spawnPosition = new Vector3(x, 0f, z);

		GameObject target = Instantiate(originalTarget, spawnPosition, Quaternion.identity);

		target.SetActive(true);

		_canSpawnEntity = false;

		StartCoroutine(SpawnCooldown());
	}

	IEnumerator SpawnCooldown()
	{
		float waitTime = Random.Range(minSpawnTime, maxSpawnTime);

		yield return new WaitForSeconds(waitTime);

		_canSpawnEntity = true;
	}
}
