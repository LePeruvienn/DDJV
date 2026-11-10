using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacter : Entity
{
	enum ControllerConfig
	{
		Old,
		New
	}

	[SerializeField] private ControllerConfig controllerConfig = ControllerConfig.Old; 

	private Camera _mainCamera;
	private CharacterController _characterController;

	protected void Awake()
	{
		_mainCamera = Camera.main;
		_characterController = GetComponent<CharacterController>();
	}

	protected void Update()
	{
		switch (controllerConfig)
		{
			case ControllerConfig.Old:
				OldHandleMovement();
				OldHandleAttack();
				break;

			case ControllerConfig.New:
				HandleRotation();
				HandleMovement();
				HandleAttack();
				break;
		}
	}

	private void HandleRotation()
	{
		var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

		var plane = new Plane(Vector3.up, Vector3.zero);
		if (plane.Raycast(ray, out var x))
		{
			var targetPosition = ray.GetPoint(x);
			var position = transform.position;

			var directionToTarget = targetPosition - position;
			directionToTarget.y = 0;

			transform.rotation = Quaternion.RotateTowards(
				transform.rotation,
				Quaternion.LookRotation(directionToTarget),
				angularSpeed * Time.deltaTime);
		}
	}

	private void HandleMovement()
	{
		Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		Vector3 motion = new Vector3(input.x, 0f, input.y) * speed * Time.deltaTime;

		_characterController.Move(motion);
	}

	private void HandleAttack()
	{
		if (Input.GetMouseButtonDown(0))
		{
			ShootBullet();
		}
	}

	private void OldHandleMovement()
	{
		if (Input.GetMouseButton(0))
		{
			var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

			var plane = new Plane(Vector3.up, Vector3.zero);
			if (plane.Raycast(ray, out var x))
			{
				var targetPosition = ray.GetPoint(x);
				var position = transform.position;

				var directionToTarget = targetPosition - position;
				directionToTarget.y = 0;

				var dot = Vector3.Dot(transform.forward, directionToTarget.normalized);
				var speedPenalty = (dot + 1f) / 2f;

				var newPosition = Vector3.MoveTowards(position, targetPosition, speedPenalty * speed * Time.deltaTime);
				_characterController.Move(newPosition - position);

				transform.rotation = Quaternion.RotateTowards(
					transform.rotation,
					Quaternion.LookRotation(directionToTarget),
					angularSpeed * Time.deltaTime);
			}
		}
	}

	private void OldHandleAttack()
	{
		if (Input.GetMouseButtonDown(1))
		{
			ShootBullet();
		}
	}
}
