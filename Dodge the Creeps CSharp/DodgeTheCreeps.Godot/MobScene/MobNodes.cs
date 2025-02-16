using DodgeTheCreeps.Core.Extensions;
using Godot;

namespace DodgeTheCreeps.MobScene;

public record MobNodes
{
  public readonly AnimatedSprite2D AnimatedSprite2D;

  public MobNodes(Mob mob)
  {
    AnimatedSprite2D = mob.GetNodeSafe<AnimatedSprite2D>("AnimatedSprite2D");
  }
}
