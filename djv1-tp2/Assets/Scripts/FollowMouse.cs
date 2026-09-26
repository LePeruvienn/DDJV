using UnityEngine;

public class FollowMouse : MonoBehaviour
{
	private Camera _mainCamera;

	void Awake()
	{
		_mainCamera = Camera.main;
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
		var plane = new Plane(Vector3.up, Vector3.zero);

		float enter = 0f;

		if (plane.Raycast(ray, out enter) && Input.GetMouseButton(0))
		{
			Vector3 hitPoint = ray.GetPoint(enter);
			Vector3 offset = new Vector3(0f, 0.5f, 0f);

			gameObject.transform.position = hitPoint + offset;
		}
	}
}
