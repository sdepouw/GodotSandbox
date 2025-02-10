extends RigidBody2D

@onready var _animatedSprite: AnimatedSprite2D = $AnimatedSprite2D

func _ready() -> void:
  var frames: SpriteFrames = _animatedSprite.sprite_frames;
  var mob_types: PackedStringArray = frames.get_animation_names()
  _animatedSprite.play(mob_types[randi() % mob_types.size()])

func _on_visible_on_screen_notifier_2d_screen_exited() -> void:
  queue_free() # deletes this instance
