using Godot;

namespace DodgeTheCreeps.AnimatedLabelScene;

public partial class AnimatedLabel : Label
{
  private AnimatedLabelNodes _nodes = null!;

  public override void _Ready() => _nodes = new(this);

  public void ShakeRed() => _nodes.AnimationPlayer.Play("shake_red");

  private void OnAnimationFinished(string name)
  {
    if (name == "shake_red")
    {
      _nodes.AnimationPlayer.Play("RESET");
    }
  }
}
