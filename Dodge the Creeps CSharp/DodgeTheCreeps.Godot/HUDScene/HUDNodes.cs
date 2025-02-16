using DodgeTheCreeps.Core.Extensions;
using Godot;

namespace DodgeTheCreeps.HUDScene;

public record HUDNodes
{
  public readonly AnimatedLabelScene.AnimatedLabel HealthAnimatedLabelInstance;
  public readonly AnimatedLabelScene.AnimatedLabel HighScoreAnimatedLabelInstance;
  
  public readonly Label ScoreLabel;
  public readonly Label Message;
  public readonly Label HighScoreBeatenMessage;
  public readonly Button StartButton;
  public readonly Button ClearHighScoreButton;
  public readonly Timer MessageTimer;

  public HUDNodes(HUD hud)
  {
    HealthAnimatedLabelInstance = hud.GetNodeSafe<AnimatedLabelScene.AnimatedLabel>("HealthAnimatedLabel");
    HighScoreAnimatedLabelInstance = hud.GetNodeSafe<AnimatedLabelScene.AnimatedLabel>("HighScoreAnimatedLabel");
    
    ScoreLabel = hud.GetNodeSafe<Label>("ScoreLabel");
    Message = hud.GetNodeSafe<Label>("Message");
    HighScoreBeatenMessage = hud.GetNodeSafe<Label>("HighScoreBeatenMessage");
    StartButton = hud.GetNodeSafe<Button>("StartButton");
    ClearHighScoreButton = hud.GetNodeSafe<Button>("ClearHighScoreButton");
    MessageTimer = hud.GetNodeSafe<Timer>("MessageTimer");
  }
}
