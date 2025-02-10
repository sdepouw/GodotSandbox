class_name MobSpawnLocationPath
extends PathFollow2D

## A path that outlines the border of the game area

## Places the given mob on a random point of the location path
func place_mob_on_path(mob: RigidBody2D) -> void:
  # Choose a random location on Path2D.
  self.progress_ratio = randf()

  # Set the mob's direction perpendicular to the path direction.
  var direction: float = self.rotation + (PI / 2)

  # Set the mob's position to a random location.
  mob.position = self.position

  # Add some randomness to the direction.
  direction += randf_range(-PI / 4, PI / 4)
  mob.rotation = direction

  # Choose the velocity for the mob.
  var velocity: Vector2 = Vector2(randf_range(150.0, 250.0), 0.0)
  mob.linear_velocity = velocity.rotated(direction)
