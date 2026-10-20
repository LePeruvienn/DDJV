using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	[Header("Statistics")]
	[SerializeField] protected int maxHealth = 5;

	[Header("Tank Parts")]
	[SerializeField] private Transform turretPivot;

	[Header("Movement")]
	[SerializeField] protected float speed = 4f;
	[SerializeField] protected float angularSpeed = 360f;

	[Header("Bullet")]
	[SerializeField] protected Bullet bulletPrefab;
	[SerializeField] protected float maxChargeTime = 2f;

	[Header("AimArrow")]
	[SerializeField] protected Transform aimArrowTransform;
	[SerializeField] protected Vector2 aimArrowScaleFactor = new Vector2(0.5f, 1f);

	[Header("UI")]
	[SerializeField] private HealthBar healthBar;

	[Header("Camera Shake")]
	[SerializeField] private float cameraShakeMagnitude = 1f;
	[SerializeField] private float cameraShakeDuration = 0.5f;

	[Header("FX")]
	[SerializeField] private GameObject characterExplosion;

	[Header("UI")]
	[SerializeField] private GameUI gameUI;

	private Camera _mainCamera;
	private CharacterController _characterController;

	private Coroutine _cameraShakeCoroutine = null;

	private float _chargeTime = 0f;

	private int _health = 1;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		_health = maxHealth;

		healthBar.SetValues(_health, maxHealth);

		_mainCamera = Camera.main;
		_characterController = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update()
	{
		HandleRotation();
		HandleMovement();
		HandleAttack();
	}

	private void HandleMovement()
	{
		Vector2 input = new Vector2(Input.GetAxis("Horizontal1"), Input.GetAxis("Vertical1"));
		Vector3 motion = new Vector3(input.x, 0f, input.y) * speed * Time.deltaTime;

		_characterController.Move(motion);
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

			turretPivot.rotation = Quaternion.RotateTowards(
				turretPivot.rotation,
				Quaternion.LookRotation(directionToTarget),
				angularSpeed * Time.deltaTime);
		}
	}

	private void HandleAttack()
	{
		if (Input.GetMouseButton(0))
		{
			_chargeTime += Time.deltaTime;

			float chargeAmount = Mathf.Clamp(_chargeTime, 0f, 2f);
			
			SetArrowScale(chargeAmount);
		}
		else if(_chargeTime > 0f)
		{
			float chargeAmount = Mathf.Clamp(_chargeTime, 0f, 2f);

			ShootBullet(chargeAmount + 1f);

			SetArrowScale(0f);

			_chargeTime = 0f;
		}
	}

	private void ShootBullet(float power)
	{
		Vector3 spawnPosition = transform.position;
		Quaternion spawnRotation = turretPivot.rotation;

		Bullet bullet = Instantiate(bulletPrefab, spawnPosition, spawnRotation);

		bullet.SetCaster(gameObject);
		bullet.SetPower(power);
	}

	private void SetArrowScale(float value)
	{
		Vector3 arrowScale = aimArrowTransform.localScale;

		arrowScale.x = value * aimArrowScaleFactor.x;
		arrowScale.z = value * aimArrowScaleFactor.y;

		aimArrowTransform.localScale = arrowScale;
	}

	private IEnumerator ShakeCamera()
	{
		Vector3 originalLocalPos = _mainCamera.transform.localPosition;

		float shakeTime = 0f;

		while(shakeTime < cameraShakeDuration)
		{
			float magnitude = cameraShakeMagnitude * Time.deltaTime;
			_mainCamera.transform.localPosition = originalLocalPos + (Random.insideUnitSphere * magnitude);

			yield return null;

			shakeTime += Time.deltaTime;
		}

		_mainCamera.transform.localPosition = originalLocalPos;
	
		yield return null;
	}

	public bool TakeDamage(int value)
	{
		Debug.Log($"Player taked {value} damage, health={_health}");

		if(value <= 0)
		{
			return false;
		}

		StartCoroutine(ShakeCamera());

		_health -= value;
		
		if (_health <= 0)
			_health = 0;

		healthBar.SetHealth(_health);

		if (_health == 0)
		{
			Kill();
			return true;
		}

		return false;
	}

	public void Kill()
	{
		_health = 0;

		Instantiate(characterExplosion, transform.position, Quaternion.identity);

		gameUI.ShowGameOver("Tu as perdu :'(");

		gameObject.SetActive(false);
	}
}
