class_name HUD
extends CanvasLayer

signal start_game

@onready var _healthLabel: Label = $HealthLabel
@onready var _highScoreLabel: Label = $HighScoreLabel
@onready var _message: Label = $Message
@onready var _messageTimer: Timer = $MessageTimer
@onready var _scoreLabel: Label = $ScoreLabel
@onready var _startButton: Button = $StartButton

var defaultMessageText: String

func _ready() -> void:
  defaultMessageText = _message.text

func show_message(text: String, temporary: bool = true) -> void:
  _message.text = text
  _message.show()
  if temporary:
    _messageTimer.start()

func show_game_over() -> void:
  show_message("Game Over")
  # Wait until the MesageTimer has counted down.
  await _messageTimer.timeout

  show_message(defaultMessageText, false)

  # Make a one-shot timer and wait for it to finish.
  await get_tree().create_timer(1.0).timeout
  _startButton.show()

func update_score(score: int) -> void:
  _scoreLabel.text = str(score)

func update_health(health: int) -> void:
  _healthLabel.text = str(health)

func update_high_score(highScore: int) -> void:
  _highScoreLabel.text = str(highScore)

func _on_start_button_pressed() -> void:
  _startButton.hide()
  start_game.emit()

func _on_message_timer_timeout() -> void:
  _message.hide()
