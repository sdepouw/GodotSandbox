extends Area2D

# Will be emitted when a Player collides with an enemy.
signal hit

@export var speed: int = 400
var screen_size: Vector2

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
  screen_size = get_viewport_rect().size
  hide()

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
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

# TODO: We need to continue to take damge on collission, not just on entry
func _on_body_entered(body: Node2D) -> void:
  hit.emit()
  # TODO: Disable this for a period of invulnerability
  # $CollisionShape2D.set_deferred("disabled", true)

func start(startPosition: Vector2) -> void:
  position = startPosition
  show()
  $CollisionShape2D.disabled = false

func _on_main_game_over_reached() -> void:
  $CollisionShape2D.set_deferred("disabled", true)
  hide()
