using UnityEngine;
using System.Collections.Generic;

public class MoveTowardsTarget : MonoBehaviour
{
	[SerializeField]
	private Transform[] targets;

	[SerializeField]
	private float moveSpeed = 1f;

	[SerializeField]
	private float stopDistance = 2f;

	[SerializeField]
	private bool closestTargetFirst = false;

	private uint _currentTargetIndex = 0;

	private HashSet<int> _alreadyVisited = new HashSet<int>();

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		if(closestTargetFirst == false)
			return;

		int closestIndex = FindClosestTargetIndex();

		if (closestIndex < 0)
		{
			Debug.LogError("Failed to get closests index in Start()");
		}
		else
		{
			_currentTargetIndex = (uint) closestIndex;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (targets.Length == 0)
		{
			Debug.LogWarning("Target Array is Empty!!");
			return;
		}

		Vector3 position = gameObject.transform.position;
		Vector3 target = targets[_currentTargetIndex].position;

		if (Vector3.Distance(target, position) > stopDistance)
		{
			HandleMoveTowardsTarget(target);
			return;
		}

		if (closestTargetFirst)
		{
			int closestIndex = FindClosestTargetIndex();

			if (closestIndex < 0)
			{
				Debug.LogError("Failed to get closests index in Start()");
			}
			else
			{
				_currentTargetIndex = (uint) closestIndex;
			}
		}
		else
		{
			if(++_currentTargetIndex == targets.Length)
			{
				_currentTargetIndex = 0;
			}
		}
	}

	int FindClosestTargetIndex()
	{
		if (_alreadyVisited.Count == targets.Length)
		{
			_alreadyVisited.Clear();
		}

		int minIndex = -1;
		float minDistance = float.MaxValue;

		Vector3 position = gameObject.transform.position;

		for (int i = 0; i < targets.Length; ++i)
		{
			Vector3 target = targets[i].position;

			float distance = Vector3.Distance(position, target);

			if (distance < minDistance && _alreadyVisited.Contains(i) == false)
			{
				minDistance = distance;
				minIndex = i;
			}
		}

		if (minIndex != -1)
		{
			_alreadyVisited.Add(minIndex);
		}

		return minIndex;
	}

	void HandleMoveTowardsTarget(Vector3 target)
	{
		Vector3 direction = Vector3.Normalize(target - gameObject.transform.position);
		gameObject.transform.position += direction * moveSpeed * Time.deltaTime;
	}
}
