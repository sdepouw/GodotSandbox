extends CharacterBody2D

@onready var player: CharacterBody2D = $/root/Game/Player

func _physics_process(_delta: float) -> void:
  var direction: Vector2 = global_position.direction_to(player.global_position)
  velocity = direction * 300
  move_and_slide()
