extends Node3D

@export var impulse_force: float = 20.0

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	position += transform.basis * Vector3.FORWARD * impulse_force * delta * -1
