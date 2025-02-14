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

  private PlayerNodes _playerNodes = null!;
  
  public override void _Ready()
  {
    _screenSize = GetViewportRect().Size;
    _playerNodes = new(
      GetNode<AnimatedSprite2D>("AnimatedSprite2D"),
      GetNode<CollisionShape2D>("CollisionShape2D")
    );
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

    AnimatedSprite2D animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

    if (velocity.Length() > 0)
    {
      velocity = velocity.Normalized() * Speed;
      animatedSprite2D.Play();
    }
    else
    {
      animatedSprite2D.Stop();
    }

    Position += velocity * (float)delta;
    Position = Position.Clamp(Vector2.Zero, _screenSize);

    if (velocity.X != 0)
    {
      animatedSprite2D.Animation = "walk";
      animatedSprite2D.FlipV = false;
      animatedSprite2D.FlipH = velocity.X < 0;
    }
    else if (velocity.Y != 0)
    {
      animatedSprite2D.Animation = "up";
      animatedSprite2D.FlipV = velocity.Y > 0;
    }
  }

  private void OnBodyEntered(Node2D body)
  {
    Hide(); // Player disappears after being hit.
    EmitSignal(SignalName.Hit);
    // Must be deferred as we can't change physics properties on a physics callback.
    _playerNodes.CollisionShape2D.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
  }

  public void Start(Vector2 position)
  {
    Position = position;
    Show();
    _playerNodes.CollisionShape2D.Disabled = false;
  }
}
