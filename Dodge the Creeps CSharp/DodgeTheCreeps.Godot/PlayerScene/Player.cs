using System;
using System.Linq;
using System.Threading.Tasks;
using DodgeTheCreeps.Core.Extensions;
using DodgeTheCreeps.MobScene;
using Godot;

namespace DodgeTheCreeps.PlayerScene;

public partial class Player : Area2D
{
  /// <summary>
  /// Emitted when a hit is taken.
  /// </summary>
  [Signal] public delegate void HitEventHandler(int oldHealth, int currentHealth);
  /// <summary>
  /// Emitted when the Player dies.
  /// </summary>
  [Signal] public delegate void DeathEventHandler();

  /// <summary>
  /// How fast the player will move (pixels/sec).
  /// </summary>(PropertyHint.Range, "1, 10")
  [Export] public int Speed { get; set; } = 400;
  /// <summary>
  /// How many hits the player can take before dying.
  /// </summary>
  [Export(PropertyHint.Range, "1,999")] public int StartingHealth { get; set; } = 3;
  /// <summary>
  /// The length of time the player is invulnerable when taking damage.
  /// </summary>
  [Export] public double InvulnerabilityOnHitTime { get; set; } = 1.0;

  /// <summary>
  /// Size of the game window
  /// </summary>
  private Vector2 _screenSize;

  /// <summary>
  /// Houses all the child nodes of this scene
  /// </summary>
  private PlayerNodes _nodes = null!;

  private int _currentHealth;
  private bool _playerIsDead;
  private bool _playerIsInvulnerable;

  public override void _Ready()
  {
    _nodes = new(this);
    _screenSize = GetViewportRect().Size;
    _nodes.TakingDamageAnimationTimer.WaitTime = InvulnerabilityOnHitTime;
    Hide();
  }

  public override void _Process(double delta)
  {
    MovePlayer(delta);
    if (GetOverlappingBodies().Any(body => body.IsInGroup(MobSceneGroups.Mobs.Name)))
    {
      TryTakeDamage();
    }
  }

  public void Start(Vector2 position)
  {
    _currentHealth = StartingHealth;
    _playerIsDead = false;
    _playerIsInvulnerable = false;
    _nodes.PlayerCollisionShape.Disabled = false;
    Position = position;
    Show();
  }

  private bool _stunlocked;
  public async Task GotHitAsync()
  {
    _stunlocked = true;
    StringName previousAnimation = _nodes.PlayerSprite.Animation;
    _nodes.PlayerSprite.Animation = "hurt";
    await this.OneShotTimer(0.5);
    _nodes.PlayerSprite.Animation = previousAnimation;
    _stunlocked = false;
  }

  private void MovePlayer(double delta)
  {
    if (_stunlocked) return;
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
      _nodes.PlayerSprite.Play();
    }
    else
    {
      _nodes.PlayerSprite.Stop();
    }

    Position += velocity * (float)delta;
    Position = Position.Clamp(Vector2.Zero, _screenSize);

    if (velocity.X != 0)
    {
      _nodes.PlayerSprite.Animation = "walk";
      _nodes.PlayerSprite.FlipV = false;
      _nodes.PlayerSprite.FlipH = velocity.X < 0;
    }
    else if (velocity.Y != 0)
    {
      _nodes.PlayerSprite.Animation = "up";
      _nodes.PlayerSprite.FlipV = velocity.Y > 0;
    }
  }

  private void TryTakeDamage(int damage = 1)
  {
    if (_playerIsDead || _playerIsInvulnerable) return;
    int oldHealth = _currentHealth;
    _currentHealth = Math.Clamp(_currentHealth - damage, 0, StartingHealth);
    EmitSignalHit(oldHealth, _currentHealth);
    if (_currentHealth == 0)
    {
      EmitSignalDeath();
    }
    else
    {
      _playerIsInvulnerable = true;
      _nodes.PlayerDamagedSound.Play();
      _nodes.TakingDamageAnimation.Play("take_damage");
      _nodes.TakingDamageAnimationTimer.Start();
    }
  }

  private void OnTakingDamageAnimationTimerTimeout()
  {
    _playerIsInvulnerable = false;
    _nodes.TakingDamageAnimation.Play("RESET");
  }

  private void OnDeath()
  {
    _playerIsDead = true;
    _nodes.PlayerCollisionShape.Disabled = true;
    Hide();
  }
}
