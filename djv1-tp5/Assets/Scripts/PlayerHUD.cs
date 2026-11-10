using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI killCountLabel;

	private uint _killCount = 0;

	public void IncreaseKillCount()
	{
		++_killCount;
		killCountLabel.SetText($"Enemies tué : {_killCount}");
	}
}
