using UnityEngine;

public class SelfRotate : MonoBehaviour
{
	enum RotationAxis
	{
		X,
		Y,
		Z
	}

	[SerializeField] 
	private float angularSpeed = 180f;

	[SerializeField] 
	private RotationAxis axis = RotationAxis.Y;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		float speed = angularSpeed * Time.deltaTime;

		float x = (axis == RotationAxis.X) ? speed : 0f;
		float y = (axis == RotationAxis.Y) ? speed : 0f;
		float z = (axis == RotationAxis.Z) ? speed : 0f;

		transform.rotation *= Quaternion.Euler(x, y, z);

		Debug.Log($"{gameObject.name} position: {transform.position}");
	}
}
