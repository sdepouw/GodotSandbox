using DodgeTheCreeps.Core.Extensions;
using DodgeTheCreeps.HUDScene;
using DodgeTheCreeps.PlayerScene;
using Godot;

namespace DodgeTheCreeps.MainScene;

public record MainNodes
{
  public readonly Player PlayerInstance;
  public readonly HUD HUDInstance;
  
  public readonly Timer MobTimer;
  public readonly Timer ScoreTimer;
  public readonly Timer StartTimer;
  public readonly Marker2D StartPosition;
  public readonly Path2D MobPath;
  public readonly PathFollow2D MobSpawnLocation;
  public readonly AudioStreamPlayer2D Music;
  public readonly AudioStreamPlayer2D DeathSound;

  public MainNodes(Node mainNode)
  {
    PlayerInstance = mainNode.GetNodeSafe<Player>("Player");
    HUDInstance = mainNode.GetNodeSafe<HUD>("HUD");
    
    MobTimer = mainNode.GetNodeSafe<Timer>("MobTimer");
    ScoreTimer = mainNode.GetNodeSafe<Timer>("ScoreTimer");
    StartTimer = mainNode.GetNodeSafe<Timer>("StartTimer");
    StartPosition = mainNode.GetNodeSafe<Marker2D>("StartPosition");
    MobPath = mainNode.GetNodeSafe<Path2D>("MobPath");
    MobSpawnLocation = MobPath.GetNodeSafe<PathFollow2D>("MobSpawnLocation");
    Music = mainNode.GetNodeSafe<AudioStreamPlayer2D>("Music");
    DeathSound = mainNode.GetNodeSafe<AudioStreamPlayer2D>("DeathSound");
  }
}
