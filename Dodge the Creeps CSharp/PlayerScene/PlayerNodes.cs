using Godot;

namespace DodgeTheCreeps.PlayerScene;

/// <summary>
/// Represents a collection of nodes for the Player scene.
/// </summary>
/// <remarks>
/// Attempting to see if I can strongly type all the Nodes in a scene in some
/// manageable way. Would have to manually maintain this collection, and
/// manually initialize it in <see cref="Node._Ready" />
/// </remarks>
public record PlayerNodes(AnimatedSprite2D AnimatedSprite2D, CollisionShape2D CollisionShape2D);
