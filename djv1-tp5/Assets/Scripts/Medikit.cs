using UnityEngine;

public class Medikit : MonoBehaviour
{
	private bool _isDead = false;

	private Animator _animator = null;

	private int _dieID = 0;

	protected void Awake()
	{
		_animator = GetComponent<Animator>();


		_dieID = Animator.StringToHash("Die");
	}

	protected void OnTriggerEnter(Collider other)
	{
		if (_isDead)
		{
			return;
		}

		_animator.SetTrigger(_dieID);
		_isDead = true;
	}
}
