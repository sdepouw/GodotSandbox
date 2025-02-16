using DodgeTheCreeps.Core.Extensions;
using Godot;

namespace DodgeTheCreeps.MobScene;

public record MobNodes
{
  public readonly AnimatedSprite2D AnimatedSprite2D;
  public readonly CollisionShape2D CollisionShape2D;
  public readonly AnimationPlayer DestroyedAnimation;

  public MobNodes(Mob mob)
  {
    AnimatedSprite2D = mob.GetNodeSafe<AnimatedSprite2D>("AnimatedSprite2D");
    CollisionShape2D = mob.GetNodeSafe<CollisionShape2D>("CollisionShape2D");
    DestroyedAnimation = mob.GetNodeSafe<AnimationPlayer>("DestroyedAnimation");
  }
}
