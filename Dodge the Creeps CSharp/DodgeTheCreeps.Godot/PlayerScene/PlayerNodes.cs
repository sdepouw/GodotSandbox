using DodgeTheCreeps.Core.Extensions;
using Godot;

namespace DodgeTheCreeps.PlayerScene;

public record PlayerNodes
{
  public readonly AnimatedSprite2D AnimatedSprite2D;
  public readonly CollisionShape2D CollisionShape2D;

  public PlayerNodes(Player player)
  {
    AnimatedSprite2D = player.GetNodeSafe<AnimatedSprite2D>("AnimatedSprite2D");
    CollisionShape2D = player.GetNodeSafe<CollisionShape2D>("CollisionShape2D");
  }
}
