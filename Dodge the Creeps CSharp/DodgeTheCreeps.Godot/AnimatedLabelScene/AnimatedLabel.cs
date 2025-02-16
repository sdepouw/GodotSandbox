using Godot;

namespace DodgeTheCreeps.AnimatedLabelScene;

public partial class AnimatedLabel : Label
{
  [Signal] public delegate void AnimationFinishedEventHandler();

  private AnimatedLabelNodes _nodes = null!;
  private const string AnimationName = "flash_red";

  public override void _Ready() => _nodes = new(this);

  public void ShakeRed()
  {
    _nodes.AnimationPlayer.Play(AnimationName);
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
    if (name == AnimationName)
    {
      _nodes.AnimationPlayer.Play("RESET");
      EmitSignalAnimationFinished();
    }
  }
}
