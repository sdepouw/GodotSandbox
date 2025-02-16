using Godot;

namespace DodgeTheCreeps.AnimatedLabelScene;

public partial class AnimatedLabel : Label
{
  [Signal] public delegate void FlashAnimationFinishedEventHandler();

  private AnimatedLabelNodes _nodes = null!;
  private const string FlashRedAnimationName = "flash_red";
  private const string BlinkAnimationName = "blink";

  public override void _Ready() => _nodes = new(this);

  public void ShakeRed()
  {
    _nodes.AnimationPlayer.Play(FlashRedAnimationName);
    Shake();
  }

  public void BlinkContinuously() => _nodes.AnimationPlayer.Play(BlinkAnimationName);
  public void StopBlinking() => _nodes.AnimationPlayer.Play("RESET");

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
    if (name == FlashRedAnimationName)
    {
      _nodes.AnimationPlayer.Play("RESET");
      EmitSignalFlashAnimationFinished();
    }
  }
}
