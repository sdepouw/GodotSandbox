using System;

namespace DodgeTheCreeps.Exceptions;

public class SceneNotInitializedException<T>(string sceneName, string propertyName)
  : Exception($"The \"{typeof(T).Name}\" Property \"{propertyName}\" was not initialized for the \"{sceneName}\" scene");
