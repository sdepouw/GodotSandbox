using System.Threading.Tasks;
using DodgeTheCreeps.Core.Extensions;
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

  public async Task HitPlayerAsync()
  {
    _nodes.CollisionShape2D.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
    _nodes.DestroyedAnimation.Play("Sprite Take Damage/take_damage");
    await this.OneShotTimer(0.5);
    QueueFree();
  }

  private void OnVisibleOnScreenNotifier2DScreenExited() => QueueFree();
}
