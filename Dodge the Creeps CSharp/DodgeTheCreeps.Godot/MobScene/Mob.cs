using Godot;

namespace DodgeTheCreeps.MobScene;

public partial class Mob : RigidBody2D
{
  private MobNodes _nodes = null!;

  public override void _Ready()
  {
    _nodes = new(this);
    string[] mobTypes = _nodes.AnimatedSprite2D.SpriteFrames.GetAnimationNames();
    _nodes.AnimatedSprite2D.Play(mobTypes[GD.Randi() % mobTypes.Length]);
  }

  private void OnVisibleOnScreenNotifier2DScreenExited() => QueueFree();
}
