extends CharacterBody2D

@onready var _happy_boo: Node2D = $HappyBoo

func _physics_process(_delta: float) -> void:
  var direction: Vector2 = Input.get_vector("move_left", "move_right", "move_up", "move_down")
  velocity = direction * 600
  move_and_slide()
  if(velocity == Vector2.ZERO):
    _happy_boo.play_idle_animation()
  else:
    _happy_boo.play_walk_animation()
