extends Node3D

@export var rotation_speed: float = 3
@export var zoom_speed: float = 15

@export var min_zoom: float = 1
@export var max_zoom: float = 20

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	handle_camera_rotation(delta)
	handle_camera_zoom(delta)

func handle_camera_zoom(delta: float) -> void:
	var z: float = $Camera3D.position.z
	if Input.is_action_just_pressed("camera_zoom_up"):
		$Camera3D.position.z = clampf(z + zoom_speed * delta, min_zoom, max_zoom)
	if Input.is_action_just_pressed("camera_zoom_down"):
		$Camera3D.position.z = clampf(z - zoom_speed * delta, min_zoom, max_zoom)
	
func handle_camera_rotation(delta: float) -> void:
	var min_rotation = -deg_to_rad(90)
	var max_rotation = 0
	if Input.is_action_pressed("camera_right"):
		rotation.y += rotation_speed * delta
	if Input.is_action_pressed("camera_left"):
		rotation.y -= rotation_speed * delta
	if Input.is_action_pressed("camera_up"):
		rotation.x = clampf(rotation.x - rotation_speed * delta, min_rotation, max_rotation)
	if Input.is_action_pressed("camera_down"):
		rotation.x = clampf(rotation.x + rotation_speed * delta, min_rotation, max_rotation)
