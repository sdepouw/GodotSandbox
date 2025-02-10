extends Node

@export var mob_scene: PackedScene

var score: int

@onready var _highScore: HighScore = $HighScore
@onready var _hud: HUD = $HUD
@onready var _mobTimer: Timer = $MobTimer
@onready var _musicPlayer: MusicPlayer = $MusicPlayer
@onready var _player: Player = $Player
@onready var _scoreTimer: Timer = $ScoreTimer
@onready var _startPosition: Marker2D = $StartPosition
@onready var _startTimer: Timer = $StartTimer

func new_game() -> void:
  score = 0
  _player.start(_startPosition.position)
  _startTimer.start()
  _hud.update_score(score)
  _hud.update_health(_player.current_health)
  _hud.show_message("Get Ready")
  _musicPlayer.play_stage_music()
  get_tree().call_group("mobs", "queue_free")

func _on_player_death() -> void:
  _scoreTimer.stop()
  _mobTimer.stop()
  _hud.show_game_over()
  _musicPlayer.play_death_music()
  if score > _highScore.get_high_score():
    _hud.update_high_score(score)
    _highScore.save_high_score(score)

func _on_player_hit(_old_health: int, new_health: int) -> void:
  _hud.update_health(new_health)

func _on_high_score_loaded(highScore: int) -> void:
  await self.ready
  _hud.update_high_score(highScore)

func _on_score_timer_timeout() -> void:
  score += 1
  _hud.update_score(score)

func _on_start_timer_timeout() -> void:
  _mobTimer.start()
  _scoreTimer.start()

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
