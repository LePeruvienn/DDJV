using UnityEngine;
using System.Collections;

public class Destroyable : MonoBehaviour
{
	[Header("Statistics")]
	[SerializeField] private int hitAmountBeforeDestroy = 3;

	[Header("FX")]
	[SerializeField] private GameObject explodeFX;

	[Header("Blink")]
	[SerializeField] private float blinkDuration = 0.1f;
	[SerializeField] private bool enableBlink = false;

	[Header("Healthbar")]
	[SerializeField] private HealthBar healthBar;

	[Header("Game UI")]
	[SerializeField] private GameUI gameUI;
	[SerializeField] bool incrementRoomNumberOnDestroy = false;

	private Renderer _renderer = null;

	private int _hitAmount = 0;
	private bool _isImmune = false;

	void Start()
	{
		if (healthBar != null)
		{
			healthBar.SetValues(hitAmountBeforeDestroy, hitAmountBeforeDestroy);
		}

		if (enableBlink)
		{
			_renderer = GetComponent<Renderer>();

			if (_renderer == null)
			{
				_renderer = GetComponentInChildren<Renderer>();
			}
		}
	}

	void OnDestroy()
	{
		if (incrementRoomNumberOnDestroy)
		{
			gameUI.IncrementCurrentRoom();
		}
	}

	public void TakeHit()
	{
		if(_isImmune)
		{
			return;
		}

		++_hitAmount;

		if (healthBar != null)
		{
			healthBar.SetHealth(hitAmountBeforeDestroy - _hitAmount);
		}

		if (_hitAmount >= hitAmountBeforeDestroy)
		{
			Instantiate(explodeFX, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
		else
		{
			if (enableBlink)
				StartCoroutine(Blink());
		}
	}

	private IEnumerator Blink()
	{
		if (_renderer != null)
		{
			Material material = _renderer.material;

			Color baseColor = material.color;

			material.color = Color.white;

			yield return new WaitForSeconds(blinkDuration);

			material.color = baseColor;
		}

		yield return null;
	}

	public void SetIsImmune(bool value)
	{
		_isImmune = value;
	}

	public bool GetIsImmune()
	{
		return _isImmune;
	}
}
