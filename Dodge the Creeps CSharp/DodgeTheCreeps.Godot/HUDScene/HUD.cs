using System.Threading.Tasks;
using DodgeTheCreeps.Core.Extensions;
using Godot;

namespace DodgeTheCreeps.HUDScene;

public partial class HUD : CanvasLayer
{
  [Signal] public delegate void StartGameEventHandler();
  [Signal] public delegate void ClearHighScoreEventHandler();
  [Export] public Color ScoreHighlightColor { get; set; } = Colors.Green;

  private string _defaultMessageText = "";

  private HUDNodes _nodes = null!;

  private int _maxHealth;

  public override void _Ready()
  {
    _nodes = new(this);
    _defaultMessageText = _nodes.Message.Text;
    _nodes.HealthAnimatedLabelInstance.Hide();
    _nodes.HighScoreBeatenMessage.Hide();
  }

  public void ShowMessage(string text, bool autoTimeout = true)
  {
    _nodes.Message.Text = text;
    _nodes.Message.Show();

    if (autoTimeout)
    {
      _nodes.MessageTimer.Start();
    }
  }

  public async Task ShowGameOverAsync(bool highScoreBeaten)
  {
    if (highScoreBeaten)
    {
      _nodes.HighScoreBeatenMessage.Show();
    }
    _nodes.HealthAnimatedLabelInstance.StopBlinking();
    ShowMessage("Game Over");
    await ToSignal(_nodes.MessageTimer, Timer.SignalName.Timeout);
    _nodes.HealthAnimatedLabelInstance.Hide();
    ShowMessage(_defaultMessageText, false);

    await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
    _nodes.StartButton.Show();
    _nodes.ClearHighScoreButton.Show();
  }

  public void InitializeHealth(int health, int maxHealth)
  {
    _maxHealth = maxHealth;
    _nodes.HealthAnimatedLabelInstance.Text = $"{health}/{maxHealth} HP";
  }

  public void UpdateHealth(int health)
  {
    InitializeHealth(health, _maxHealth);
    _nodes.HealthAnimatedLabelInstance.ShakeRed();
    if (health <= 1)
    {
      _nodes.HealthAnimatedLabelInstance.BlinkContinuously();
    }
  }

  public void UpdateScore(int score, bool highlight = false)
  {
    _nodes.ScoreLabel.Text = score.ToString("D5");
    _nodes.ScoreLabel.ApplyHighlight(highlight, ScoreHighlightColor);
  }

  public void UpdateHighScore(int highScore, bool highlight = false)
  {
    _nodes.HighScoreAnimatedLabelInstance.Text = highScore.ToString("D5");
    _nodes.HighScoreAnimatedLabelInstance.ApplyHighlight(highlight, ScoreHighlightColor);
  }
  
  public void ClearHighlighting()
  {
    _nodes.ScoreLabel.ClearHighlight();
    _nodes.HighScoreAnimatedLabelInstance.ClearHighlight();
  }

  private void OnStartButtonPressed()
  {
    _nodes.StartButton.Hide();
    _nodes.ClearHighScoreButton.Hide();
    _nodes.HighScoreBeatenMessage.Hide();
    _nodes.HealthAnimatedLabelInstance.Show();
    EmitSignalStartGame();
  }

  private void OnClearButtonPressed()
  {
    ClearHighlighting();
    _nodes.HighScoreAnimatedLabelInstance.ShakeRed();
    EmitSignalClearHighScore();
    _nodes.ClearHighScoreButton.SetDisabled(true);
  }

  private void OnMessageTimerTimeout() => _nodes.Message.Hide();
  
  private void OnHighScoreFlashAnimationFinished() => _nodes.ClearHighScoreButton.SetDisabled(false);
}
