extends Area2D

@export var speed: int = 1000
@export var bullet_range: int = 1200

var travelled_distance = 0

func _physics_process(delta: float) -> void:
  var direction: Vector2 = Vector2.RIGHT.rotated(rotation)
  var distance: float = speed * delta
  position += direction * distance
  travelled_distance += distance
  if travelled_distance > bullet_range:
    queue_free()

func _on_body_entered(body: Node2D) -> void:
  queue_free()
  if body.has_method("take_damage"): # Duck typing!
    body.take_damage()
