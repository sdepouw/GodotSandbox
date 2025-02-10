class_name MusicPlayer
extends Node

@onready var _stageMusic: AudioStreamPlayer = $StageMusic
@onready var _deathMusic: AudioStreamPlayer = $DeathMusic

func play_stage_music() -> void:
  _stageMusic.play()

func play_death_music() -> void:
  _stageMusic.stop()
  _deathMusic.play()
