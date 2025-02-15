using System;
using Godot;

namespace DodgeTheCreeps.Exceptions;

public class NodeNotFoundException(Node sourceNode, NodePath path)
  : Exception($"The node \"{sourceNode.Name}\" does not have a child node in the path \"{path}\"");
