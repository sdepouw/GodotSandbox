using DodgeTheCreeps.Core.Extensions;
using Godot;

namespace DodgeTheCreeps.HUDScene;

public record HUDNodes
{
  public readonly Label ScoreLabel;
  public readonly Label Message;
  public readonly Button StartButton;
  public readonly Timer MessageTimer;

  public HUDNodes(HUD hud)
  {
    ScoreLabel = hud.GetNodeSafe<Label>("ScoreLabel");
    Message = hud.GetNodeSafe<Label>("Message");
    StartButton = hud.GetNodeSafe<Button>("StartButton");
    MessageTimer = hud.GetNodeSafe<Timer>("MessageTimer");
  }
}
