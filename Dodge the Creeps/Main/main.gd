extends Node

@export var mob_scene: PackedScene

var score: int

#region Child Nodes

@onready var _highScore: HighScore = $HighScore
@onready var _hud: HUD = $HUD
@onready var _mob_spawn_location: MobSpawnLocationPath = $MobPath/MobSpawnLocation
@onready var _mobTimer: Timer = $MobTimer
@onready var _musicPlayer: MusicPlayer = $MusicPlayer
@onready var _player: Player = $Player
@onready var _scoreTimer: Timer = $ScoreTimer
@onready var _startPosition: Marker2D = $StartPosition
@onready var _startTimer: Timer = $StartTimer

func _ensure_children_exist() -> void:
  assert(_highScore != null)
  assert(_hud != null)
  assert(_mob_spawn_location != null)
  assert(_mobTimer != null)
  assert(_musicPlayer != null)
  assert(_player != null)
  assert(_scoreTimer != null)
  assert(_startPosition != null)
  assert(_startTimer != null)

func _ready() -> void:
  _ensure_children_exist()

#endregion

func new_game() -> void:
  score = 0
  _player.start(_startPosition.position)
  _startTimer.start()
  _hud.update_score(score)
  _hud.update_health(_player.current_health)
  _hud.show_message("Get Ready")
  _musicPlayer.play_stage_music()
  get_tree().call_group("mobs", "queue_free")

func game_over() -> void:
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
  # This can get signalled before this node is ready, so we have to
  # await the 'ready' signal for this node in case this signal fires
  # ahead of time
  await self.ready
  _hud.update_high_score(highScore)

func _on_score_timer_timeout() -> void:
  score += 1
  _hud.update_score(score)

func _on_start_timer_timeout() -> void:
  _mobTimer.start()
  _scoreTimer.start()

func _on_mob_timer_timeout() -> void:
  var mob: RigidBody2D = mob_scene.instantiate() as RigidBody2D
  assert(mob is RigidBody2D and mob != null, "Mob instance must be of type RigidBody2D")

  _mob_spawn_location.place_mob_on_path(mob)

  # Spawn the mob by adding it to the Main scene.
  add_child(mob)
