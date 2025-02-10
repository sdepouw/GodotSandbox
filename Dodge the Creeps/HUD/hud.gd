extends CanvasLayer

# Notifies 'Main' node that the button has been pressed.
signal start_game

var defaultMessageText: String

func _ready() -> void:
  defaultMessageText = $Message.text

func show_message(text: String, temporary: bool = true) -> void:
  $Message.text = text
  $Message.show()
  if temporary:
    $MessageTimer.start()

func show_game_over() -> void:
  show_message("Game Over")
  # Wait until the MesageTimer has counted down.
  await $MessageTimer.timeout

  show_message(defaultMessageText, false)

  # Make a one-shot timer and wait for it to finish.
  await get_tree().create_timer(1.0).timeout
  $StartButton.show()

func update_score(score: int) -> void:
  $ScoreLabel.text = str(score)

func update_health(health: int) -> void:
  $HealthLabel.text = str(health)

func _on_start_button_pressed() -> void:
  $StartButton.hide()
  start_game.emit()

func _on_message_timer_timeout() -> void:
  $Message.hide()
