using System.Threading.Tasks;
using Godot;

namespace DodgeTheCreeps.HUDScene;

public partial class HUD : CanvasLayer
{
  [Signal] public delegate void StartGameEventHandler();

  private string _defaultMessageText = "";

  private HUDNodes _nodes = null!;

  private int _maxHealth = 0;

  public override void _Ready()
  {
    _nodes = new(this);
    _defaultMessageText = _nodes.Message.Text;
    _nodes.HealthLabel.Hide();
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

  public async Task ShowGameOverAsync()
  {
    ShowMessage("Game Over");

    await ToSignal(_nodes.MessageTimer, Timer.SignalName.Timeout);
    _nodes.HealthLabel.Hide();
    ShowMessage(_defaultMessageText, false);

    await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
    _nodes.StartButton.Show();
  }

  public void UpdateHealth(int health, int? maxHealth = null)
  {
    if (maxHealth.HasValue)
    {
      _maxHealth = maxHealth.Value;
    }
    else
    {
      maxHealth = _maxHealth;
    }
    _nodes.HealthLabel.Text = $"{health}/{maxHealth} HP";
  }

  public void UpdateScore(int score) => _nodes.ScoreLabel.Text = score.ToString("D5");
  public void UpdateHighScore(int highScore) => _nodes.HighScoreLabel.Text = highScore.ToString("D5");

  private void OnStartButtonPressed()
  {
    _nodes.StartButton.Hide();
    _nodes.HealthLabel.Show();
    EmitSignal(SignalName.StartGame);
  }

  private void OnMessageTimerTimeout() => _nodes.Message.Hide();
}
