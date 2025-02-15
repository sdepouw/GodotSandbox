using System.Threading.Tasks;
using Godot;

namespace DodgeTheCreeps.HUDScene;

public partial class HUD : CanvasLayer
{
  [Signal] public delegate void StartGameEventHandler();

  private string _defaultMessageText = "";

  private HUDNodes _nodes = null!;
  
  public override void _Ready()
  {
    _nodes = new(this);
    _defaultMessageText = _nodes.Message.Text;
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

    ShowMessage(_defaultMessageText, false);

    await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
    _nodes.StartButton.Show();
  }

  public void UpdateScore(int score) => _nodes.ScoreLabel.Text = score.ToString();

  private void OnStartButtonPressed()
  {
    _nodes.StartButton.Hide();
    EmitSignal(SignalName.StartGame);
  }

  private void OnMessageTimerTimeout() => _nodes.Message.Hide();
}
