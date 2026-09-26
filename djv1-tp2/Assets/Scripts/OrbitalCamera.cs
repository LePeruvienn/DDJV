using UnityEngine;

public class OrbitalCamera : MonoBehaviour
{

	[SerializeField]
	private float angularSpeed = 20f;

	[SerializeField]
	private float zoomSpeed = 0.1f;


	[SerializeField] 
	private Transform cameraTransform;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		HandleZoom();
		HandleRotation();
	}

	void HandleZoom()
	{
		Vector3 position = cameraTransform.localPosition;

		position.z += zoomSpeed * Input.mouseScrollDelta.y;;

		if (position.z > 0f) position.z = 0f;
		if (position.z < -50f) position.z = -50f;

		cameraTransform.localPosition = position;
	}

	void HandleRotation()
	{
		Vector3 rotate = Vector3.zero;

		if (Input.GetKey(KeyCode.W)) rotate.x += 1f;
		if (Input.GetKey(KeyCode.A)) rotate.y -= 1f;
		if (Input.GetKey(KeyCode.S)) rotate.x -= 1f;
		if (Input.GetKey(KeyCode.D)) rotate.y += 1f;

		rotate *= angularSpeed * Time.deltaTime;

		Vector3 rotation = gameObject.transform.eulerAngles;

		rotation += rotate;

		if (rotation.x < 0f) rotation.x = 0f;
		if (rotation.x > 90f) rotation.x = 90f;

		gameObject.transform.eulerAngles = rotation;
	}
}
