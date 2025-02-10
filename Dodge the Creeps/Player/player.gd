extends Area2D

var player_is_dead: bool = false
var player_is_invincible: bool = false
var current_health: int
var screen_size: Vector2

signal hit(old_health: int, new_health: int)
signal death

## Starting health for Player
@export var starting_health: int = 3
## Speed Player travels
@export var speed: int = 400
## Length of time (in seconds) Player is invulnerable after taking damage
@export var iframe_time: float = 1.0

func start(startPosition: Vector2) -> void:
  position = startPosition
  current_health = starting_health
  show()
  $CollisionShape2D.disabled = false
  player_is_dead = false

func _process(delta: float) -> void:
  move_player(delta)
  if get_overlapping_bodies().any(is_mob_body):
    try_take_damage()

func is_mob_body(body: Node2D) -> bool:
  return body.is_in_group("mobs")

func try_take_damage(damage: int = 1) -> void:
  if player_is_invincible or player_is_dead:
    return
  var old_health: int = current_health
  current_health -= damage
  if current_health <= 0:
    current_health = 0
  hit.emit(old_health, current_health)
  if current_health == 0:
    death.emit()
  else:
    player_is_invincible = true
    $TakingDamageAnimation.play("take_damage")
    $AnimationTimer.start()

func move_player(delta: float) -> void:
  var velocity: Vector2 = Vector2.ZERO # The player's movement vector.
  if Input.is_action_pressed("move_right"):
    velocity.x += 1
  if Input.is_action_pressed("move_left"):
    velocity.x -= 1
  if Input.is_action_pressed("move_down"):
    velocity.y += 1
  if Input.is_action_pressed("move_up"):
    velocity.y -= 1

  if velocity.length() > 0:
    # Normalizing prevents faster movement with diagonal (i.e. multiple) input.
    velocity = velocity.normalized() * speed
    # '$' is shorthand for get_node(), but '$' is typed!
    $AnimatedSprite2D.play()
  else:
    $AnimatedSprite2D.stop()

  position += velocity * delta
  position = position.clamp(Vector2.ZERO, screen_size)

  if velocity.x != 0:
    $AnimatedSprite2D.animation = "walk"
    $AnimatedSprite2D.flip_v = false
    $AnimatedSprite2D.flip_h = velocity.x < 0
  elif velocity.y != 0:
    $AnimatedSprite2D.animation = "up"
    $AnimatedSprite2D.flip_v = velocity.y > 0

func _on_death() -> void:
  player_is_dead = true
  $CollisionShape2D.set_deferred("disabled", true)
  hide()

func _ready() -> void:
  screen_size = get_viewport_rect().size
  $AnimationTimer.wait_time = iframe_time
  hide()

func _on_animation_timer_timeout() -> void:
  player_is_invincible = false
  $TakingDamageAnimation.play("RESET")
