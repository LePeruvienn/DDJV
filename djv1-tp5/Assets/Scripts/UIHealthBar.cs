using UnityEngine;

public class UIHealthBar : MonoBehaviour
{
	[SerializeField] private RectTransform fillRect;

	private void Update()
	{
		transform.rotation = Camera.main.transform.rotation;
	}

	public void SetHealthBarStatus(float percent)
	{
		Vector2 anchor = fillRect.anchorMax;
		anchor.x = percent;

		fillRect.anchorMax = anchor;
	}
}
