using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float angularSpeed = 360f;

    private Camera _mainCamera;
    private CharacterController _characterController;

    protected void Awake()
    {
        _mainCamera = Camera.main;
        _characterController = GetComponent<CharacterController>();
    }

    protected void Update()
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
}
