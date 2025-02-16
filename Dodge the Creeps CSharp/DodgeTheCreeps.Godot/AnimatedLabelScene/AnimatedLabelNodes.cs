using DodgeTheCreeps.Core.Extensions;
using Godot;

namespace DodgeTheCreeps.AnimatedLabelScene;

public record AnimatedLabelNodes
{
  public readonly AnimationPlayer AnimationPlayer;

  public AnimatedLabelNodes(AnimatedLabel label)
  {
    AnimationPlayer = label.GetNodeSafe<AnimationPlayer>("AnimationPlayer");
  }
}
