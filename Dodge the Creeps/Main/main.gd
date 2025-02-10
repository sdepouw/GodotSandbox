extends Node

@export var mob_scene: PackedScene
@export var starting_health: int = 3

signal game_over_reached

var score: int
var current_health: int

func game_over() -> void:
  $ScoreTimer.stop()
  $MobTimer.stop()
  $HUD.show_game_over()
  $Music.stop()
  $DeathSound.play()
  game_over_reached.emit()

func new_game() -> void:
  score = 0
  current_health = starting_health;
  $Player.start($StartPosition.position)
  $StartTimer.start()
  $HUD.update_score(score)
  $HUD.update_health(current_health)
  $HUD.show_message("Get Ready")
  $Music.play()
  get_tree().call_group("mobs", "queue_free")

func _on_score_timer_timeout() -> void:
  score += 1
  $HUD.update_score(score)

func _on_start_timer_timeout() -> void:
  $MobTimer.start()
  $ScoreTimer.start()

func _on_mob_timer_timeout() -> void:
  # Create a new instance of the Mob scene.
  var mob: RigidBody2D = mob_scene.instantiate() as RigidBody2D
  assert(mob is RigidBody2D and mob != null, "Mob instance must be of type RigidBody2D")

  # Choose a random location on Path2D.
  var mob_spawn_location: PathFollow2D = $MobPath/MobSpawnLocation
  mob_spawn_location.progress_ratio = randf()

  # Set the mob's direction perpendicular to the path direction.
  var direction: float = mob_spawn_location.rotation + (PI / 2)

  # Set the mob's position to a random location.
  mob.position = mob_spawn_location.position

  # Add some randomness to the direction.
  direction += randf_range(-PI / 4, PI / 4)
  mob.rotation = direction

  # Choose the velocity for the mob.
  var velocity: Vector2 = Vector2(randf_range(150.0, 250.0), 0.0)
  mob.linear_velocity = velocity.rotated(direction)

  # Spawn the mob by adding it to the Main scene.
  add_child(mob)

func _on_player_hit() -> void:
  current_health -= 1
  $HUD.update_health(current_health)
  if current_health <= 0:
    game_over()
