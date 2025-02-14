using Godot;

namespace DodgeTheCreeps;

public sealed class InputAction
{
  public static readonly InputAction Right = new("move_right");
  public static readonly InputAction Left = new("move_left");
  public static readonly InputAction Up = new("move_up");
  public static readonly InputAction Down = new("move_down");

  private readonly StringName _action;
  private InputAction(string actionName) => _action = new(actionName);

  public bool IsActionPressed() => Input.IsActionPressed(_action);
}
