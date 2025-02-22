extends CharacterBody2D

const SMOKE_SCENE: PackedScene = preload("res://smoke_explosion/smoke_explosion.tscn")

@export var health: int = 3

@onready var _player: CharacterBody2D = $/root/Game/Player
@onready var _slime: Node2D = $Slime

func _ready() -> void:
  _slime.play_walk()

func _physics_process(_delta: float) -> void:
  var direction: Vector2 = global_position.direction_to(_player.global_position)
  velocity = direction * 300
  move_and_slide()

func take_damage() -> void:
  _slime.play_hurt()
  health -= 1
  if health <= 0:
    queue_free()
    var smoke = SMOKE_SCENE.instantiate()
    smoke.global_position = global_position
    get_parent().add_child(smoke)
