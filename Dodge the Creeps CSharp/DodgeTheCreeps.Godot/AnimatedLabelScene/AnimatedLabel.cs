using Godot;

namespace DodgeTheCreeps.AnimatedLabelScene;

public partial class AnimatedLabel : Label
{
  [Signal] public delegate void AnimationFinishedEventHandler();

  private AnimatedLabelNodes _nodes = null!;

  public override void _Ready() => _nodes = new(this);

  public void ShakeRed()
  {
    _nodes.AnimationPlayer.Play("shake_red");
    Shake();
  }

  private void Shake()
  {
    NodePath positionPath = new("position");
    using Tween tween = GetTree().CreateTween();
    for (int i = 0; i < 2; i++)
    {
      tween.TweenProperty(this, positionPath, new Vector2(Position.X + 5, Position.Y), .05);
      tween.TweenProperty(this, positionPath, new Vector2(Position.X, Position.Y), .05);
      tween.TweenProperty(this, positionPath, new Vector2(Position.X - 5, Position.Y), .05);
      tween.TweenProperty(this, positionPath, new Vector2(Position.X, Position.Y), .05);
    }
  }

  private void OnAnimationFinished(string name)
  {
    if (name == "shake_red")
    {
      _nodes.AnimationPlayer.Play("RESET");
      EmitSignalAnimationFinished();
    }
  }
}
