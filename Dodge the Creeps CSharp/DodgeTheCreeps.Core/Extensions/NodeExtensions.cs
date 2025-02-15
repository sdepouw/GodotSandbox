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
}
