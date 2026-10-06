using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private GameObject enemyPrefab;

	[SerializeField] private float spawnCooldown = 3f;
	[SerializeField] private int maxEnemyAmount = 10;

	private uint _enemyCount = 0;

	private void Awake()
	{
		StartCoroutine(SpawnCoroutine());
	}

	public void EnemyCountDecrease()
	{
		if (_enemyCount != 0)
		{
			--_enemyCount;
		}
	}

	IEnumerator SpawnCoroutine()
	{
		yield return null;

		while(enabled)
		{
			if (_enemyCount < maxEnemyAmount)
			{
				++_enemyCount;

				Vector3 position = new Vector3(Random.Range(-12f, 12f), 0f, Random.Range(-12f, 12f));
	
				Instantiate(enemyPrefab, position, Quaternion.identity);

				yield return new WaitForSeconds(spawnCooldown);
			}

			yield return null;
		}
	}
}
