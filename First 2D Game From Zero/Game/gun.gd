extends Area2D

const BULLET: PackedScene = preload("res://bullet.tscn")

@onready var _shooting_point: Marker2D = %ShootingPoint

func _physics_process(_delta: float) -> void:
  var enemies_in_range: Array[Node2D] = get_overlapping_bodies()
  if enemies_in_range.size() > 0:
    var target_enemy: CharacterBody2D = enemies_in_range.front()
    look_at(target_enemy.global_position)

func shoot() -> void:
  var new_bullet: Area2D = BULLET.instantiate()
  # Can "make unique" in Scene tree, and in this script can use %
  new_bullet.global_position = _shooting_point.global_position
  new_bullet.global_rotation = _shooting_point.global_rotation
  _shooting_point.add_child(new_bullet)
