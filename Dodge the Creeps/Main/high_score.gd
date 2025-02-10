class_name HighScore
extends Node

const _saveDataLocation: String = "user://highscore.dat"
var _highScore: int

signal loaded(highScore: int)

func get_high_score() -> int:
  return _highScore

func save_high_score(newHighScore: int) -> void:
  var save_file: FileAccess = FileAccess.open(_saveDataLocation, FileAccess.WRITE)
  save_file.store_32(newHighScore)

func _ready() -> void:
  var highScoreLoaded: bool = _load_high_score()
  if highScoreLoaded:
    self.loaded.emit(_highScore)

func _load_high_score() -> bool:
  if not FileAccess.file_exists(_saveDataLocation):
    return false
  var save_file: FileAccess = FileAccess.open(_saveDataLocation, FileAccess.READ)
  _highScore = save_file.get_32()
  return true
