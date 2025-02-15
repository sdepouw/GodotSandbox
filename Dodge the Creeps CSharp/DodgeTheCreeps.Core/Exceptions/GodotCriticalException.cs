using Godot;

namespace DodgeTheCreeps.Core.Exceptions;

/// <summary>
/// Exceptions should inherit from this class when they should immediately terminate the program.
/// </summary>
/// <remarks>
/// Exceptions using this should only be for initialization or other startup-related checks,
/// so that the game does not crash at odd points.
/// </remarks>
public abstract class GodotCriticalException : ApplicationException
{
  protected GodotCriticalException(string message)
    : base(message)
  {
    SceneTree? tree = Engine.GetMainLoop() as SceneTree;
    tree?.Quit(1);
  }
}
