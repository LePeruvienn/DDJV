using UnityEngine;
using System.Collections;

public class PlayerCharacter : MonoBehaviour
{
	[SerializeField]
	private float playerSpeed = 3f;

	[SerializeField]
	private float minSpeedFactor = 0.5f;

	[SerializeField]
	private float rotationSpeed = 3f;

	[SerializeField]
	private float minDistanceToReachTarget = 0.5f;

	private Camera _mainCamera = null;

	private Vector3 _targetPosition = Vector3.zero;

	void Awake()
	{
		_mainCamera = Camera.main;

		_targetPosition = transform.position;
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		UpdateTargetPosition();

		float distance = Vector3.Distance(transform.position, _targetPosition);

		if (distance > minDistanceToReachTarget)
		{
			Vector3 position = transform.position;
			Vector3 targetPosition = _targetPosition;

			position.y = 0f;
			targetPosition.y = 0f;

			Vector3 relativePos = targetPosition - position;

			Vector3 forward = transform.forward;
			Vector3 direction = relativePos.normalized;

			float dot = Vector3.Dot(forward, direction);
			float speedFactor = Mathf.Clamp01(dot + minSpeedFactor);

			Quaternion targetRotation = Quaternion.LookRotation(relativePos, Vector3.up);

			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
			transform.position = Vector3.MoveTowards(transform.position, _targetPosition, playerSpeed * speedFactor * Time.deltaTime);
		}
	}

	void UpdateTargetPosition()
	{
		if(!Input.GetMouseButton(0))
		{
			return;
		}

		var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
		var plane = new Plane(Vector3.up, Vector3.zero);

		float enter = 0f;

		if (plane.Raycast(ray, out enter))
		{
			Vector3 hitPoint = ray.GetPoint(enter);
			Vector3 offset = new Vector3(0f, 0.5f, 0f);

			_targetPosition = hitPoint + offset;
		}
	}
}
