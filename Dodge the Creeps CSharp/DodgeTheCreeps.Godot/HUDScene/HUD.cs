using System.Threading.Tasks;
using Godot;

namespace DodgeTheCreeps.HUDScene;

public partial class HUD : CanvasLayer
{
  [Signal] public delegate void StartGameEventHandler();

  private string _defaultMessageText = "";
  
  public override void _Ready() => _defaultMessageText = GetNode<Label>("Message").Text;

  public override void _Process(double delta)
  {
  }

  public void ShowMessage(string text)
  {
    Label message = GetNode<Label>("Message");
    message.Text = text;
    message.Show();
    
    GetNode<Timer>("MessageTimer").Start();
  }

  public async Task ShowGameOverAsync()
  {
    ShowMessage("Game Over");
    
    Timer messageTimer = GetNode<Timer>("MessageTimer");
    await ToSignal(messageTimer, Timer.SignalName.Timeout);
    
    Label message = GetNode<Label>("Message");
    message.Text = _defaultMessageText;
    message.Show();
    
    await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
    GetNode<Button>("StartButton").Show();
  }

  public void UpdateScore(int score) => GetNode<Label>("ScoreLabel").Text = score.ToString();

  private void OnStartButtonPressed()
  {
    GetNode<Button>("StartButton").Hide();
    EmitSignal(SignalName.StartGame);
  }
  
  private void OnMessageTimerTimeout() => GetNode<Label>("Message").Hide();
}
