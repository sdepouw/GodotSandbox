using Godot;

namespace DodgeTheCreeps.PlayerScene;

public partial class Player : Area2D
{
  /// <summary>
  /// Emitted when a hit is taken.
  /// </summary>
  [Signal] public delegate void HitEventHandler();
  
  /// <summary>
  /// How fast the player will move (pixels/sec).
  /// </summary>
  [Export] public int Speed { get; set; } = 400;

  /// <summary>
  /// Size of the game window
  /// </summary>
  private Vector2 _screenSize;

  /// <summary>
  /// Houses all the child nodes of this scene
  /// </summary>
  private PlayerNodes _childNodes = null!;
  
  public override void _Ready()
  {
    _screenSize = GetViewportRect().Size;
    _childNodes = new()
    {
      AnimatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D"),
      CollisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D")
    };
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
      _childNodes.AnimatedSprite2D.Play();
    }
    else
    {
      _childNodes.AnimatedSprite2D.Stop();
    }

    Position += velocity * (float)delta;
    Position = Position.Clamp(Vector2.Zero, _screenSize);

    if (velocity.X != 0)
    {
      _childNodes.AnimatedSprite2D.Animation = "walk";
      _childNodes.AnimatedSprite2D.FlipV = false;
      _childNodes.AnimatedSprite2D.FlipH = velocity.X < 0;
    }
    else if (velocity.Y != 0)
    {
      _childNodes.AnimatedSprite2D.Animation = "up";
      _childNodes.AnimatedSprite2D.FlipV = velocity.Y > 0;
    }
  }

  private void OnBodyEntered(Node2D _)
  {
    Hide(); // Player disappears after being hit.
    EmitSignal(SignalName.Hit);
    // Must be deferred as we can't change physics properties on a physics callback.
    _childNodes.CollisionShape2D.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
  }

  public void Start(Vector2 position)
  {
    Position = position;
    Show();
    _childNodes.CollisionShape2D.Disabled = false;
  }
}
