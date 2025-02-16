using Godot;
using FileAccess = Godot.FileAccess;

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
    using FileAccess saveFile = FileAccess.Open(SaveDataLocation, FileAccess.ModeFlags.Write);
    saveFile.Store32((uint)Value);
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
    if (!FileAccess.FileExists(SaveDataLocation)) return false;
    using FileAccess saveFile = FileAccess.Open(SaveDataLocation, FileAccess.ModeFlags.Read);
    Value = (int)saveFile.Get32();
    return true;
  }
}
