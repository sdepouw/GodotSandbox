extends Area2D

var touching_a_creep: bool = false
var player_is_dead: bool = false
var current_health: int
var screen_size: Vector2

# Will be emitted when a Player collides with an enemy.
signal hit(old_health: int, new_health: int)
signal death

@export var starting_health: int = 3
@export var speed: int = 400

func start(startPosition: Vector2) -> void:
  position = startPosition
  current_health = starting_health
  show()
  $CollisionShape2D.disabled = false
  player_is_dead = false

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
  move_player(delta)
  if touching_a_creep:
    take_damage()

func take_damage(damage: int = 1) -> void:
  if player_is_dead:
    return
  var old_health: int = current_health
  current_health -= damage
  if current_health <= 0:
    current_health = 0
  hit.emit(old_health, current_health)
  if current_health == 0:
    death.emit()

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

func _on_body_entered(body: Node2D) -> void:
  take_damage()
  touching_a_creep = true
  $CollisionShape2D.set_deferred("disabled", true)

func _on_body_exited(body: Node2D) -> void:
  touching_a_creep = false
  $CollisionShape2D.set_deferred("disabled", false)

func _on_death() -> void:
  player_is_dead = true
  $CollisionShape2D.set_deferred("disabled", true)
  hide()

func _ready() -> void:
  screen_size = get_viewport_rect().size
  hide()
