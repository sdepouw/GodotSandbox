using DodgeTheCreeps.Core.Extensions;
using Godot;

namespace DodgeTheCreeps.HUDScene;

public record HUDNodes
{
  public readonly AnimatedLabelScene.AnimatedLabel HealthLabelInstance;
  public readonly AnimatedLabelScene.AnimatedLabel HighScoreLabelInstance;
  
  public readonly Label ScoreLabel;
  public readonly Label Message;
  public readonly Label HighScoreBeatenMessage;
  public readonly Button StartButton;
  public readonly Button ClearHighScoreButton;
  public readonly Timer MessageTimer;

  public HUDNodes(HUD hud)
  {
    HealthLabelInstance = hud.GetNodeSafe<AnimatedLabelScene.AnimatedLabel>("HealthLabel");
    HighScoreLabelInstance = hud.GetNodeSafe<AnimatedLabelScene.AnimatedLabel>("HighScoreLabel");
    
    ScoreLabel = hud.GetNodeSafe<Label>("ScoreLabel");
    Message = hud.GetNodeSafe<Label>("Message");
    HighScoreBeatenMessage = hud.GetNodeSafe<Label>("HighScoreBeatenMessage");
    StartButton = hud.GetNodeSafe<Button>("StartButton");
    ClearHighScoreButton = hud.GetNodeSafe<Button>("ClearHighScoreButton");
    MessageTimer = hud.GetNodeSafe<Timer>("MessageTimer");
  }
}
