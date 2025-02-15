using Godot;

namespace DodgeTheCreeps.PlayerScene;

public record PlayerNodes
{
  public required AnimatedSprite2D AnimatedSprite2D { get; init; }
  public required CollisionShape2D CollisionShape2D { get; init; }
}
