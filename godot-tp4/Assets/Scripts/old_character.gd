extends Node3D

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	handle_movement(delta)
	
func handle_movement(delta: float) -> void:
	if not Input.is_action_pressed("move_to"):
		return
	var camera: Camera3D = get_viewport().get_camera_3d()
	if not camera:
		return
		
	var mouse_pos: Vector2 = get_viewport().get_mouse_position()
	var ray_origin: Vector3 = camera.project_ray_origin(mouse_pos)
	var ray_normal: Vector3 = camera.project_ray_normal(mouse_pos)
	
	var plane: Plane = Plane(Vector3.UP, Vector3.ZERO)
	var hit_point = plane.intersects_ray(ray_origin, ray_normal)
	
	if hit_point:
		position = hit_point
