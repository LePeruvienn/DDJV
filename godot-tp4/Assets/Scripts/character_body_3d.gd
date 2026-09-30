extends CharacterBody3D

@export var speed: float = 5.0
@export var rotation_speed: float = 10.0
@export var jump_velocity: float = 4.5
@export var distance_to_reach_destination: float = 1.0

@export var bullet_scene: PackedScene

var destination: Vector3 = Vector3.ZERO

func _ready() -> void:
	destination = position

func _physics_process(delta: float) -> void:
	update_destination()
	handle_gravity(delta)
	handle_jump(delta)
	handle_movement(delta)
	move_and_slide()
	handle_attack()

func handle_gravity(delta: float) -> void:
	if not is_on_floor():
		velocity += get_gravity() * delta
	
func handle_jump(delta: float) -> void:
	if Input.is_action_just_pressed("player_jump") and is_on_floor():
		velocity.y = jump_velocity
		
func handle_movement(delta: float) -> void:
	var direction: Vector3 = (destination - position).normalized()
	var target_angle: float = atan2(direction.x, direction.z)
	# Rotate Player
	rotation.y = lerp_angle(rotation.y, target_angle, rotation_speed * delta)
	# Move Player
	if position.distance_to(destination) < distance_to_reach_destination:
		velocity.x = 0
		velocity.z = 0
		return
	velocity.x = direction.x * speed
	velocity.z = direction.z * speed
	
func update_destination() -> void:
	if not Input.is_action_pressed("player_move"):
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
		destination = hit_point

func handle_attack() -> void:
	if not Input.is_action_just_pressed("player_attack"):
		return
	var bullet: Node3D = bullet_scene.instantiate()
	get_tree().root.add_child(bullet)
	bullet.position = position
	bullet.rotation = rotation
