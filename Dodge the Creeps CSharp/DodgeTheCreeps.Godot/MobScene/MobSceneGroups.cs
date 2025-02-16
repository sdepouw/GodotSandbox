using Godot;

namespace DodgeTheCreeps.MobScene;

public sealed class MobSceneGroups
{
  public static readonly MobSceneGroups Mobs = new("mobs");
  
  public readonly StringName Name;
  private MobSceneGroups(string name) => Name = new(name);
}
