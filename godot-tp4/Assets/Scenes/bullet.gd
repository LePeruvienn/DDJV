extends Node3D

@export var impulse_force: float = 20.0

@onready var rigid_body: RigidBody3D = $RigidBody3D

func _ready() -> void:
	# 1. Configurer la détection de collision sur le RigidBody enfant
	rigid_body.contact_monitor = true
	rigid_body.max_contacts_reported = 1
	
	# 2. Utiliser la rotation du RigidBody (ou de la scène) pour donner l'impulsion
	# Dans Godot 3D, l'avant d'un objet est toujours -basis.z (Vector3.FORWARD est égal à (0,0,-1))
	rigid_body.linear_velocity = -transform.basis.z * impulse_force

# Signal connecté depuis le RigidBody3D enfant :
func _on_rigid_body_3d_body_entered(body: Node) -> void:
	print("Touché : ", body.name)
	
	if body.has_method("recevoir_degats"):
		body.recevoir_degats(25)
		
	queue_free() # Détruit tout le nœud de la balle
