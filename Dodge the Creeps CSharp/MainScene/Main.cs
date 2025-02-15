using System;
using DodgeTheCreeps.Exceptions;
using DodgeTheCreeps.HUDScene;
using DodgeTheCreeps.MobScene;
using DodgeTheCreeps.PlayerScene;
using Godot;

namespace DodgeTheCreeps.MainScene;

public partial class Main : Node
{
  [Export] public PackedScene MobScene { get; set; } = null!;

  private int _score;
  
  public override void _Ready()
  {
    if (MobScene is null) throw new SceneNotInitializedException<PackedScene>(Name, nameof(MobScene));
  }

  // Event handlers must use "async void", else Godot signals won't call them
  private async void GameOver()
  {
    GetNode<Timer>("MobTimer").Stop();
    GetNode<Timer>("ScoreTimer").Stop();

    GetNode<AudioStreamPlayer2D>("Music").Stop();
    GetNode<AudioStreamPlayer2D>("DeathSound").Play();
    await GetNode<HUD>("HUD").ShowGameOverAsync();
  }
  
  private void NewGame()
  {
    _score = 0;
    
    Player player = GetNode<Player>("Player");
    Marker2D startPosition = GetNode<Marker2D>("StartPosition");
    player.Start(startPosition.Position);
    
    GetNode<Timer>("StartTimer").Start();
    
    HUD hud = GetNode<HUD>("HUD");
    hud.UpdateScore(_score);
    hud.ShowMessage("Get Ready!");
    
    GetTree().CallGroup("mobs", Node.MethodName.QueueFree);
    GetNode<AudioStreamPlayer2D>("Music").Play();
  }

  private void OnStartTimerTimeout()
  {
    GetNode<Timer>("MobTimer").Start();
    GetNode<Timer>("ScoreTimer").Start();
  }

  private void OnScoreTimerTimeout()
  {
    _score++;
    GetNode<HUD>("HUD").UpdateScore(_score);
  }

  private void OnMobTimerTimeout()
  {
    // Create a new instance of the Mob scene.
    Mob mob = MobScene.Instantiate<Mob>();
    
    // Choose a random location on Path2D.
    PathFollow2D mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
    mobSpawnLocation.ProgressRatio = GD.Randf();

    // Set the mob's direction perpendicular to the path direction.
    float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

    // Set the mob's position to a random location.
    mob.Position = mobSpawnLocation.Position;

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
