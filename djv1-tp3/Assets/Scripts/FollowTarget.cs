using UnityEngine;

public class FollowTarget : MonoBehaviour
{
	[SerializeField]
	private GameObject target = null;

	[SerializeField]
	private float smoothTime = 0.1f;

	// Update is called once per frame
	void Update()
	{
		Vector3 velocity = Vector3.zero;
		transform.position = Vector3.SmoothDamp(transform.position, target.transform.position, ref velocity, smoothTime);
	}
}
