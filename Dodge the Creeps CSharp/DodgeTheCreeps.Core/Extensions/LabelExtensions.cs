using Godot;

namespace DodgeTheCreeps.Core.Extensions;

public static class LabelExtensions
{
  /// <summary>
  /// Adds/removes a "font_color" override to the given Label.
  /// </summary>
  public static void ApplyHighlight(this Label label, bool highlight, Color color)
  {
    if (highlight)
    {
      if (!label.HasThemeColorOverride("font_color"))
      {
        label.AddThemeColorOverride("font_color", color);
      }
    }
    else
    {
      label.ClearHighlight();
    }
  }

  /// <summary>
  /// Clears any "font_color" override that is set on the given Label.
  /// </summary>
  public static void ClearHighlight(this Label label)
  {
    if (label.HasThemeColorOverride("font_color"))
    {
      label.RemoveThemeColorOverride("font_color");
    }
  }
}
