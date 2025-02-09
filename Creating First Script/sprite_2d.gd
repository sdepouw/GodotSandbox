extends Sprite2D

@export var speed: float = 400
@export var angular_speed: float = PI

func _process(delta: float):
  rotation += angular_speed * delta
  var velocity: Vector2 = Vector2.UP.rotated(rotation) * speed
  position += velocity * delta

func _on_button_pressed() -> void:
  set_process(not is_processing())

func _ready():
  # get_node looks at this node's children ("Sprite2D" is this node)
  # and gets nodes by their name
  var timer: Timer = get_node("Timer")
  timer.timeout.connect(_on_timer_timeout)

func _on_timer_timeout():
  print("Health: %d" % health)
  if (health <= 0):
    visible = false
    print("Game Over!")
  else:
    visible = not visible

signal health_depleted
var health: int = 10

# TODO: Attempt to create a "damaged" blinking animation using the Timer
# Start with a slow blink, then faster blink, then blink-per-frame, then solid
func _on_ouch_button_pressed() -> void:
  health -= 1
  health_depleted.emit()
