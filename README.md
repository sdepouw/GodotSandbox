# GodotSandbox
Learning Godot

## Creating First Script

A sandbox project that started from the ["Creating your first script" Godot documentation](https://docs.godotengine.org/en/stable/getting_started/step_by_step/scripting_first_script.html), and a place where I experiment with various things.

## Dodge the Creeps

A complete basic game tutorial from the [Godot documentation](https://docs.godotengine.org/en/stable/getting_started/first_2d_game/index.html). Intend to expand after the basic tutorial is complete.

### Enhancements

Added additional features to the base game to experiment with them.

- Instead of one hit instant death, the player can now take multiple hits (3 by default)
- High Score is now maintained, and persists between play sessions
- Code reorganized, ideally trying to shuffle everything out from being in a single class (usually `Main`)
  - Experiment with creating scripts in child nodes, or creating new nodes solely to encapsulate single responsibilities
  - Experiment with Best Practices in terms of sending singals up, calling down, and more complex signal traversal (such as child -> parent -> other child)
  - Other various refactors, experiments, comments, and more
