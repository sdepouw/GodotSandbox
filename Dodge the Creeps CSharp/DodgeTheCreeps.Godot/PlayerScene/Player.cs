using System;
using Godot;

namespace DodgeTheCreeps.PlayerScene;

public partial class Player : Area2D
{
  /// <summary>
  /// Emitted when a hit is taken.
  /// </summary>
  [Signal] public delegate void HitEventHandler();
  /// <summary>
  /// Emitted when the Player dies.
  /// </summary>
  [Signal] public delegate void DeathEventHandler();

  /// <summary>
  /// How fast the player will move (pixels/sec).
  /// </summary>
  [Export] public int Speed { get; set; } = 400;
  /// <summary>
  /// How many hits the player can take before dying.
  /// </summary>
  [Export] public int StartingHealth { get; set; } = 3;

  /// <summary>
  /// Size of the game window
  /// </summary>
  private Vector2 _screenSize;

  /// <summary>
  /// Houses all the child nodes of this scene
  /// </summary>
  private PlayerNodes _nodes = null!;

  private int _currentHealth;

  public override void _Ready()
  {
    _screenSize = GetViewportRect().Size;
    _nodes = new(this);
    Hide();
  }

  public override void _Process(double delta)
  {
    Vector2 velocity = Vector2.Zero; // The player's movement vector.

    if (InputAction.Right.IsActionPressed())
    {
      velocity.X += 1;
    }
    if (InputAction.Left.IsActionPressed())
    {
      velocity.X -= 1;
    }
    if (InputAction.Down.IsActionPressed())
    {
      velocity.Y += 1;
    }
    if (InputAction.Up.IsActionPressed())
    {
      velocity.Y -= 1;
    }

    if (velocity.Length() > 0)
    {
      velocity = velocity.Normalized() * Speed;
      _nodes.AnimatedSprite2D.Play();
    }
    else
    {
      _nodes.AnimatedSprite2D.Stop();
    }

    Position += velocity * (float)delta;
    Position = Position.Clamp(Vector2.Zero, _screenSize);

    if (velocity.X != 0)
    {
      _nodes.AnimatedSprite2D.Animation = "walk";
      _nodes.AnimatedSprite2D.FlipV = false;
      _nodes.AnimatedSprite2D.FlipH = velocity.X < 0;
    }
    else if (velocity.Y != 0)
    {
      _nodes.AnimatedSprite2D.Animation = "up";
      _nodes.AnimatedSprite2D.FlipV = velocity.Y > 0;
    }
  }

  private void OnBodyEntered(Node2D _)
  {
    _currentHealth = Math.Max(_currentHealth - 1, 0);
    if (_currentHealth > 0) return;
    
    // TODO: Make player blink and be invulnerable during that time.
    Hide(); // Player disappears after dying.
    EmitSignal(SignalName.Death);
    // Must be deferred as we can't change physics properties on a physics callback.
    _nodes.CollisionShape2D.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
  }

  public void Start(Vector2 position)
  {
    _currentHealth = StartingHealth;
    Position = position;
    Show();
    _nodes.CollisionShape2D.Disabled = false;
  }
}
