class_name MusicPlayer
extends Node

@onready var _stageMusic: AudioStreamPlayer = $StageMusic
@onready var _deathMusic: AudioStreamPlayer = $DeathMusic

func play_stage_music() -> void:
  _stageMusic.play()

func _on_player_death() -> void:
  _stageMusic.stop()
  _deathMusic.play()
