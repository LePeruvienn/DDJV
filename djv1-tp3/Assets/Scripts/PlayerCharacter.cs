using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacter : MonoBehaviour
{
	[Header("Player Movement")]
	[SerializeField] private float playerSpeed = 3f;
	[SerializeField] private float minSpeedFactor = 0.5f;
	[SerializeField] private float rotationSpeed = 3f;
	[SerializeField] private float minDistanceToReachTarget = 0.5f;

	private CharacterController _characterController = null;
	private Camera _mainCamera = null;
	private Vector3 _targetPosition = Vector3.zero;

	[Header("Explosion")]
	[SerializeField] private float explosionRadius = 5f;
	[SerializeField] float explosionForce = 10f;
	[SerializeField] private float upwardsModifier = 2.0f;
	[SerializeField] private uint maxColliders = 128;

	private Collider[] _hitColliders;

	void Awake()
	{
		_mainCamera = Camera.main;
		_targetPosition = transform.position;
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		_hitColliders = new Collider[maxColliders];

		_characterController = GetComponent<CharacterController>();

		if (_characterController == null)
			Debug.LogError("Failed to Get Component : CharacterController");
	}

	// Update is called once per frame
	void Update()
	{
		// Left Click
		if(Input.GetMouseButtonDown(1))
		{
 			MakeExplosion();
		}

		// Right Click
		if(Input.GetMouseButton(0))
		{
			UpdateTargetPosition();
		}

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
			// transform.position = Vector3.MoveTowards(transform.position, _targetPosition, playerSpeed * speedFactor * Time.deltaTime);

			_characterController.Move(direction * playerSpeed * speedFactor * Time.deltaTime);
		}
	}

	void UpdateTargetPosition()
	{
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

	void MakeExplosion()
	{
		Vector3 center = transform.position;
		float radius = explosionRadius;

		int numColliders = Physics.OverlapSphereNonAlloc(center, radius, _hitColliders);

		uint nullCount = 0;
		uint rbCount = 0;

		for (uint i = 0; i < numColliders; ++i)
		{
			Collider collider = _hitColliders[i];

			if(collider == null)
			{
				++nullCount;
				continue;
			}

			Rigidbody rb = collider.GetComponent<Rigidbody>();

			if (rb == null)
			{
				continue;
			}

			rbCount++;

			rb.AddExplosionForce(
				explosionForce,
				transform.position,
				explosionRadius,
				upwardsModifier,
				ForceMode.Impulse
			);

			Debug.Log($"Rigidbody explosion applied : {rb}");
		}


		Debug.Log($"Hitted {numColliders} colliders, array length {_hitColliders.Length}, null count {nullCount}, rb Count {rbCount}");
	}
}
