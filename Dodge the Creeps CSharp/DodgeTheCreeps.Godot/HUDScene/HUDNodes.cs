using DodgeTheCreeps.Core.Extensions;
using Godot;

namespace DodgeTheCreeps.HUDScene;

public record HUDNodes
{
  public readonly Label HealthLabel;
  public readonly Label ScoreLabel;
  public readonly Label HighScoreLabel;
  public readonly Label Message;
  public readonly Label HighScoreBeatenMessage;
  public readonly Button StartButton;
  public readonly Button ClearHighScoreButton;
  public readonly Timer MessageTimer;

  public HUDNodes(HUD hud)
  {
    HealthLabel = hud.GetNodeSafe<Label>("HealthLabel");
    ScoreLabel = hud.GetNodeSafe<Label>("ScoreLabel");
    HighScoreLabel = hud.GetNodeSafe<Label>("HighScoreLabel");
    Message = hud.GetNodeSafe<Label>("Message");
    HighScoreBeatenMessage = hud.GetNodeSafe<Label>("HighScoreBeatenMessage");
    StartButton = hud.GetNodeSafe<Button>("StartButton");
    ClearHighScoreButton = hud.GetNodeSafe<Button>("ClearHighScoreButton");
    MessageTimer = hud.GetNodeSafe<Timer>("MessageTimer");
  }
}
