extends MeshInstance3D

@export var targets: Array[Node3D] = []
@export var rotationSpeed = 2
@export var moveSpeed = 2
@export var reachedDistance = 0.5
@export var targetClosestFirst: bool = true

var currentTarget = 0

var reachedTargets: Array[int] = []

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	if targetClosestFirst == true:
		currentTarget = GetClosestTarget()
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	HandleRotate(delta)
	MoveTowardsTarget(delta)
	pass
	
func HandleRotate(delta: float) -> void:
	var axis = Vector3(1, 0, 1).normalized()
	transform.basis = transform.basis.rotated(axis, rotationSpeed * delta)

func GetClosestTarget() -> int:
	if len(targets) == len(reachedTargets):
		reachedTargets.clear()
	var closestTarget = 0
	var closestDistance = transform.origin.distance_to(targets[closestTarget].transform.origin)
	for i in range(1, len(targets)):
		var origin = targets[i].transform.origin
		var distance = transform.origin.distance_to(origin)
		if distance < closestDistance and reachedTargets.find(i) == -1:
			closestTarget = i
			closestDistance = distance
	reachedTargets.push_back(closestTarget)
	return closestTarget

func GetNewTarget() -> int:
	if targetClosestFirst == true:
		return GetClosestTarget()
	else:
		var target = currentTarget + 1
		if (target == len(targets)):
			target = 0
		return target

func MoveTowardsTarget(delta: float) -> void:
	if len(targets) == 0:
		return
		
	var targetPosition: Vector3 = targets[currentTarget].transform.origin
	
	if transform.origin.distance_to(targetPosition) < reachedDistance:
		currentTarget = GetNewTarget()
	else:
		transform.origin = transform.origin.move_toward(targetPosition, moveSpeed * delta)
	
