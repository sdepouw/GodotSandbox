using Godot;

namespace DodgeTheCreeps.Core.Exceptions;

/// <summary>
/// Thrown when a property (usually one attributed with <see cref="ExportAttribute" />) is not properly
/// initialized in a scene.
/// </summary>
public class ScenePropertyNotInitializedException<T>(string sceneName, string propertyName)
  : GodotCriticalException($"The \"{typeof(T).Name}\" Property \"{propertyName}\" was not initialized for the \"{sceneName}\" scene");
