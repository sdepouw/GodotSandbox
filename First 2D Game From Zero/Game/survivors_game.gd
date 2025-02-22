extends Node2D

const MOB = preload("res://mob.tscn")

@onready var _path_follow: PathFollow2D = %PathFollow2D
@onready var _game_over: CanvasLayer = $GameOver

func _ready():
  spawn_mob()

func spawn_mob():
  var mob = MOB.instantiate()
  _path_follow.progress_ratio = randf()
  mob.global_position = _path_follow.global_position
  add_child(mob)


func _on_player_health_depleted() -> void:
  _game_over.show()
  get_tree().paused = true
