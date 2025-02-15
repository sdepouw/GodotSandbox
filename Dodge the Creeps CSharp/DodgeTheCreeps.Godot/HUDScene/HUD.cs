using System.Threading.Tasks;
using DodgeTheCreeps.Core.Extensions;
using Godot;

namespace DodgeTheCreeps.HUDScene;

public partial class HUD : CanvasLayer
{
  [Signal] public delegate void StartGameEventHandler();

  private string _defaultMessageText = "";

  private HUDNodes _childNodes = null!;
  
  public override void _Ready()
  {
    _childNodes = new()
    {
      Message = this.GetNodeSafe<Label>("Message"),
      MessageTimer = this.GetNodeSafe<Timer>("MessageTimer"),
      ScoreLabel = this.GetNodeSafe<Label>("ScoreLabel"),
      StartButton = this.GetNodeSafe<Button>("StartButton")
    };
    _defaultMessageText = _childNodes.Message.Text;
  }

  public void ShowMessage(string text, bool autoTimeout = true)
  {
    _childNodes.Message.Text = text;
    _childNodes.Message.Show();

    if (autoTimeout)
    {
      _childNodes.MessageTimer.Start();  
    }
  }

  public async Task ShowGameOverAsync()
  {
    ShowMessage("Game Over");

    await ToSignal(_childNodes.MessageTimer, Timer.SignalName.Timeout);

    ShowMessage(_defaultMessageText, false);

    await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
    _childNodes.StartButton.Show();
  }

  public void UpdateScore(int score) => GetNode<Label>("ScoreLabel").Text = score.ToString();

  private void OnStartButtonPressed()
  {
    _childNodes.StartButton.Hide();
    EmitSignal(SignalName.StartGame);
  }

  private void OnMessageTimerTimeout() => _childNodes.Message.Hide();
}
