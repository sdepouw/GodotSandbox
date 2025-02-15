using Godot;

namespace DodgeTheCreeps.HUDScene;

public record HUDNodes
{
  public required Label ScoreLabel { get; init; }
  public required Label Message { get; init; }
  public required Button StartButton { get; init; }
  public required Timer MessageTimer { get; init; }
}
