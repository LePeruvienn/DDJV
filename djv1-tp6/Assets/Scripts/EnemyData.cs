using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Game/Enemy")]
public class EnemyData : ScriptableObject
{
	[SerializeField] private string label = "Enemy Name";
	[SerializeField] private int health = 50;
	[SerializeField] private int xp = 20;

	public int Health
	{
		get { return health; }
	}

	public string Label
	{
		get { return label; }
	}
	
	public int Xp
	{
		get { return xp; }
	}
}
