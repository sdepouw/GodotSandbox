using Godot;

namespace DodgeTheCreeps.Core.Exceptions;

/// <summary>
/// Thrown when attempting to get a node from a scene tree, and said node is not found.
/// </summary>
public class NodeNotFoundException(Node sourceNode, NodePath path)
  : GodotCriticalException($"The node \"{sourceNode.Name}\" does not have a child node in the path \"{path}\"");
