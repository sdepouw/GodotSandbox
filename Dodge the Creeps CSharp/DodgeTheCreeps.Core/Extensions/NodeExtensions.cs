using DodgeTheCreeps.Core.Exceptions;
using Godot;

namespace DodgeTheCreeps.Core.Extensions;

public static class NodeExtensions
{
  /// <summary>
  /// Calls <see cref="Node.GetNode{T}" /> and either returns the found node
  /// or throws <see cref="NodeNotFoundException" /> and terminates the game.
  /// </summary>
  /// <exception cref="NodeNotFoundException">
  /// Thrown when the given <paramref name="path" /> is not found in <paramref name="node" />'s
  /// children.
  /// </exception>
  public static T GetNodeSafe<T>(this Node node, NodePath path) where T : class
  {
    T? childNode = node.GetNode<T>(path);
    if (childNode == null) throw new NodeNotFoundException(node, path);
    return childNode;
  }

  /// <summary>
  /// Creates a one-shot <see cref="Godot.Timer" /> that lasts for <paramref name="timeSec" />
  /// before calling <see cref="Godot.Timer.EmitSignalTimeout" />, then disposing itself.
  /// </summary>
  public static SignalAwaiter OneShotTimer(this Node node, double timeSec)
  {
    return node.ToSignal(node.GetTree().CreateTimer(timeSec), SceneTreeTimer.SignalName.Timeout);
  }
}
