using DodgeTheCreeps.Core.Exceptions;
using DodgeTheCreeps.MobScene;
using Godot;

namespace DodgeTheCreeps.MainScene;

public partial class Main : Node
{
  [Export] public PackedScene MobScene { get; set; } = null!;

  private int _score;
  private MainNodes _nodes = null!;

  public override void _Ready()
  {
    if (MobScene is null) throw new ScenePropertyNotInitializedException<PackedScene>(Name, nameof(MobScene));
    _nodes = new(this);
  }
  
  private void NewGame()
  {
    _score = 0;

    _nodes.PlayerInstance.Start(_nodes.StartPosition.Position);

    _nodes.StartTimer.Start();

    _nodes.HUDInstance.UpdateScore(_score);
    _nodes.HUDInstance.UpdateHealth(_nodes.PlayerInstance.StartingHealth, _nodes.PlayerInstance.StartingHealth);
    _nodes.HUDInstance.ShowMessage("Get Ready!");

    GetTree().CallGroup(MobSceneGroups.Mobs.Name, Node.MethodName.QueueFree);
    _nodes.Music.Play();
  }

  private void OnHit(int _, int newHealth) => _nodes.HUDInstance.UpdateHealth(newHealth);

  // ReSharper disable once AsyncVoidMethod
  // Event handlers must use "async void" when calling asynchronous code, else Godot signals won't call them.
  private async void GameOver()
  {
    _nodes.MobTimer.Stop();
    _nodes.ScoreTimer.Stop();

    _nodes.Music.Stop();
    _nodes.DeathSound.Play();
    await _nodes.HUDInstance.ShowGameOverAsync();
  }

  private void OnStartTimerTimeout()
  {
    _nodes.MobTimer.Start();
    _nodes.ScoreTimer.Start();
  }

  private void OnScoreTimerTimeout()
  {
    _score++;
    _nodes.HUDInstance.UpdateScore(_score);
  }

  private void OnMobTimerTimeout()
  {
    // Create a new instance of the Mob scene.
    Mob mob = MobScene.Instantiate<Mob>();

    // Choose a random location on Path2D.
    _nodes.MobSpawnLocation.ProgressRatio = GD.Randf();

    // Set the mob's direction perpendicular to the path direction.
    float direction = _nodes.MobSpawnLocation.Rotation + Mathf.Pi / 2;

    // Set the mob's position to a random location.
    mob.Position = _nodes.MobSpawnLocation.Position;

    // Add some randomness to the direction.
    direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
    mob.Rotation = direction;

    // Choose the velocity.
    Vector2 velocity = new((float)GD.RandRange(150.0, 250.0), 0);
    mob.LinearVelocity = velocity.Rotated(direction);

    // Spawn the mob by adding it to the Main scene.
    AddChild(mob);
  }
}
