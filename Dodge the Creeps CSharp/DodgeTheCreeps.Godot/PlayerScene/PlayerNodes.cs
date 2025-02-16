using DodgeTheCreeps.Core.Extensions;
using Godot;

namespace DodgeTheCreeps.PlayerScene;

public record PlayerNodes
{
  public readonly AnimatedSprite2D PlayerSprite;
  public readonly CollisionShape2D PlayerCollisionShape;
  public readonly AnimationPlayer TakingDamageAnimation;
  public readonly AudioStreamPlayer2D PlayerDamagedSound;
  public readonly Timer TakingDamageAnimationTimer;

  public PlayerNodes(Player player)
  {
    PlayerSprite = player.GetNodeSafe<AnimatedSprite2D>("PlayerSprite");
    PlayerCollisionShape = player.GetNodeSafe<CollisionShape2D>("PlayerCollisionShape");
    TakingDamageAnimation = player.GetNodeSafe<AnimationPlayer>("TakingDamageAnimation");
    PlayerDamagedSound = player.GetNodeSafe<AudioStreamPlayer2D>("PlayerDamagedSound");
    TakingDamageAnimationTimer = player.GetNodeSafe<Timer>("TakingDamageAnimationTimer");
  }
}
