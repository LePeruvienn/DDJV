using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
	[SerializeField]
	private GameObject orignalCube;

	[SerializeField]
	private Transform spawnOrigin;

	[SerializeField]
	private float spawnRadius = 5f;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	void OnTriggerEnter(Collider other)
	{
		Vector3 position = (Random.insideUnitSphere * spawnRadius);

		if (position.y < 0)
			position.y *= -1;

		position += spawnOrigin.position;

		GameObject cube = Instantiate(orignalCube, position, Quaternion.identity);

		cube.SetActive(true);
	}
}
