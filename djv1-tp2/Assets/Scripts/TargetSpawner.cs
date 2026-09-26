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

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		

	}

	IEnumerator DeathCoroutine()
	{
		while(true)
		{
			float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
			yield return new WaitForSeconds(waitTime);

			float x = Random.Range(-10f, 10f);
			float y = Random.Range(-10f, 10f);

			GameObject target = Instantiate(originalTarget);
		}
	}
}
