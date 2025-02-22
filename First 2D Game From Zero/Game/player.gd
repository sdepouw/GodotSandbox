extends CharacterBody2D

signal health_depleted

## Current health of the Player
@export var health: float = 150.0:
  set(value):
    health = value
    _health_bar.value = value
## How much damage enemies do when they collide with the Player
@export var damage_rate: float = 5.0

@onready var _happy_boo: Node2D = $HappyBoo
@onready var _hurt_box: Area2D = %HurtBox
@onready var _health_bar: ProgressBar = $HealthBar

func _ready() -> void:
  _health_bar.max_value = health
  _health_bar.value = health

# Delta is applied automatically by move_and_slide()
func _physics_process(delta: float) -> void:
  var direction: Vector2 = Input.get_vector("move_left", "move_right", "move_up", "move_down")
  velocity = direction * 600
  move_and_slide()
  if(velocity == Vector2.ZERO):
    _happy_boo.play_idle_animation()
  else:
    _happy_boo.play_walk_animation()

  var overlapping_mobs = _hurt_box.get_overlapping_bodies()
  if overlapping_mobs.size() > 0:
    health -= damage_rate * overlapping_mobs.size() * delta
    if health <= 0.0:
      health_depleted.emit()
