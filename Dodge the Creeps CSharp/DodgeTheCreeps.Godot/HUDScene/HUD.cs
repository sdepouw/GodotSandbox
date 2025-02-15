using System.Threading.Tasks;
using DodgeTheCreeps.Core.Extensions;
using Godot;

namespace DodgeTheCreeps.HUDScene;

public partial class HUD : CanvasLayer
{
  [Signal] public delegate void StartGameEventHandler();

  private string _defaultMessageText = "";

  private HUDNodes _nodes = null!;
  
  public override void _Ready()
  {
    _nodes = new()
    {
      Message = this.GetNodeSafe<Label>("Message"),
      MessageTimer = this.GetNodeSafe<Timer>("MessageTimer"),
      ScoreLabel = this.GetNodeSafe<Label>("ScoreLabel"),
      StartButton = this.GetNodeSafe<Button>("StartButton")
    };
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

  public void UpdateScore(int score) => GetNode<Label>("ScoreLabel").Text = score.ToString();

  private void OnStartButtonPressed()
  {
    _nodes.StartButton.Hide();
    EmitSignal(SignalName.StartGame);
  }

  private void OnMessageTimerTimeout() => _nodes.Message.Hide();
}
