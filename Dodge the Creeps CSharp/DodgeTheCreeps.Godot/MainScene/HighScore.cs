using System;
using System.IO;
using Godot;

namespace DodgeTheCreeps.MainScene;

public partial class HighScore : Node
{
  private const string SaveDataLocation = "user://highscore.dat";
  /// <summary>
  /// The current High Score
  /// </summary>
  public int Value { get; private set; }

  [Signal] public delegate void LoadedEventHandler(int highScore);

  public void SaveHighScore(int newHighScore)
  {
    Value = newHighScore;
    File.WriteAllBytes(SaveDataLocation, BitConverter.GetBytes(Value));
  }

  public override void _Ready()
  {
    if (LoadHighScore())
    {
      EmitSignalLoaded(Value);
    }
  }
  
  private bool LoadHighScore()
  {
    if (!File.Exists(SaveDataLocation)) return false;
    Value = BitConverter.ToInt32(File.ReadAllBytes(SaveDataLocation));
    return true;
  }
}
