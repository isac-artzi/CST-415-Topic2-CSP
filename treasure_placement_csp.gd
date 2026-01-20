extends Node2D

## CSP-based Treasure Placement System
## In-Class Activity - CST-415 Topic 2
##
## This script demonstrates Constraint Satisfaction Problem solving
## for placing treasures in a dungeon grid with multiple constraints.

# Grid Configuration
@export var grid_size: int = 8
@export var num_treasures: int = 5
@export var cell_size: float = 64.0

# Visualization
@export var show_metrics: bool = true
@export var cell_scene: PackedScene
@export var treasure_scene: PackedScene

# Colors for different cell types
const COLOR_VALID = Color(0.8, 0.8, 0.8, 1.0)
const COLOR_INVALID = Color(0.5, 0.3, 0.3, 1.0)
const COLOR_SPAWN_ZONE = Color(0.3, 0.5, 0.8, 1.0)
const COLOR_TREASURE = Color(1.0, 0.84, 0.0, 1.0)

# Performance Metrics
var backtrack_count: int = 0
var nodes_explored: int = 0

# Data structures for CSP
var treasure_positions: Array[Vector2i] = []
var invalid_positions: Dictionary = {}
var grid_cells: Array = []
var treasure_objects: Array = []

func _ready() -> void:
	initialize_grid()
	solve_treasure_placement()

#region Grid Initialization

## Creates the visual grid and marks invalid positions
func initialize_grid() -> void:
	grid_cells.clear()
	invalid_positions.clear()
	treasure_objects.clear()

	for x in range(grid_size):
		grid_cells.append([])
		for y in range(grid_size):
			var cell = create_cell(x, y)
			grid_cells[x].append(cell)

			# Mark invalid positions
			if is_invalid_cell(x, y):
				invalid_positions[Vector2i(x, y)] = true
				set_cell_color(cell, COLOR_INVALID)
			elif is_spawn_zone(x, y):
				invalid_positions[Vector2i(x, y)] = true
				set_cell_color(cell, COLOR_SPAWN_ZONE)
			else:
				set_cell_color(cell, COLOR_VALID)

## Create a single grid cell
func create_cell(x: int, y: int) -> ColorRect:
	var cell = ColorRect.new()
	cell.size = Vector2(cell_size - 4, cell_size - 4)
	cell.position = Vector2(x * cell_size, y * cell_size)
	add_child(cell)
	return cell

## Check if cell is too close to edges (constraint: 1 cell from walls)
func is_invalid_cell(x: int, y: int) -> bool:
	return x == 0 or x == grid_size - 1 or y == 0 or y == grid_size - 1

## Check if cell is in the spawn zone (top-left 2x2)
func is_spawn_zone(x: int, y: int) -> bool:
	return x < 2 and y < 2

#endregion

#region CSP Solver

## Main CSP solving function - finds valid treasure placement
func solve_treasure_placement() -> void:
	print("=== Starting CSP Treasure Placement ===")
	backtrack_count = 0
	nodes_explored = 0
	treasure_positions.clear()

	var start_time = Time.get_ticks_msec()
	var success = backtrack_search(0)
	var end_time = Time.get_ticks_msec()

	if success:
		print("✓ Solution found! Placed %d treasures" % treasure_positions.size())
		print("Performance: %d nodes explored, %d backtracks" % [nodes_explored, backtrack_count])
		print("Time: %d ms" % (end_time - start_time))
		visualize_solution()
	else:
		print("✗ No solution found!")

## Backtracking search algorithm
## TODO: Students implement this function
##
## @param treasure_index: Current treasure being placed (0 to num_treasures-1)
## @return: True if solution found, false otherwise
func backtrack_search(treasure_index: int) -> bool:
	nodes_explored += 1

	# Base case: all treasures placed successfully
	if treasure_index >= num_treasures:
		return true

	# TODO: Implement variable selection with MRV heuristic
	# For now, we just try to place treasures in order

	# Get domain of valid positions for this treasure
	var domain = get_domain(treasure_index)

	# TODO: Implement Least Constraining Value ordering
	# For now, try positions in order

	for position in domain:
		# Try assigning this position
		treasure_positions.append(position)

		# Check if this assignment is consistent with constraints
		if is_consistent(position, treasure_index):
			# Recursively try to place remaining treasures
			if backtrack_search(treasure_index + 1):
				return true  # Solution found!

		# Assignment failed, backtrack
		treasure_positions.pop_back()
		backtrack_count += 1

	return false  # No valid assignment found

## Get the domain (valid positions) for a treasure
## TODO: Students can optimize this with forward checking
func get_domain(treasure_index: int) -> Array[Vector2i]:
	var domain: Array[Vector2i] = []

	for x in range(grid_size):
		for y in range(grid_size):
			var pos = Vector2i(x, y)

			# Skip invalid positions
			if pos in invalid_positions:
				continue

			# Skip already occupied positions
			if pos in treasure_positions:
				continue

			domain.append(pos)

	return domain

## Check if a position satisfies all constraints
func is_consistent(position: Vector2i, current_treasure_index: int) -> bool:
	# Check constraints against all previously placed treasures
	for i in range(treasure_positions.size() - 1):
		var other_treasure = treasure_positions[i]

		# Constraint 1: No two treasures in same position (handled by domain)
		if position == other_treasure:
			return false

		# Constraint 2: Minimum Manhattan distance of 2
		var manhattan_distance = abs(position.x - other_treasure.x) + \
		                        abs(position.y - other_treasure.y)
		if manhattan_distance < 2:
			return false

		# Constraint 3: Not adjacent (no horizontal or vertical neighbors)
		var is_adjacent = (abs(position.x - other_treasure.x) <= 1 and \
		                  abs(position.y - other_treasure.y) <= 1 and \
		                  manhattan_distance > 0)
		if is_adjacent and manhattan_distance == 1:
			return false

	return true

#endregion

#region Heuristics (TODO for students)

## TODO: Implement Most Constrained Variable (MRV) heuristic
## Select the treasure position that has the fewest remaining valid positions
func select_most_constrained_variable() -> int:
	# STUDENT TODO: Implement MRV
	# Hint: For each unplaced treasure, count how many valid positions remain
	# Return the index of the treasure with fewest options

	return -1  # Placeholder

## TODO: Implement Least Constraining Value (LCV) heuristic
## Order domain values by how many options they eliminate for other variables
func order_by_least_constraining(domain: Array[Vector2i]) -> Array[Vector2i]:
	# STUDENT TODO: Implement LCV
	# Hint: For each position in domain, count how many positions it would
	# eliminate from other treasures' domains
	# Sort by ascending number of eliminations

	return domain  # Placeholder - return unsorted

## TODO: Implement forward checking
## After placing a treasure, reduce domains of remaining treasures
func forward_check(new_position: Vector2i) -> bool:
	# STUDENT TODO: Implement forward checking
	# Hint: For each unplaced treasure, remove values from its domain
	# that are now inconsistent with new_position
	# Return false if any domain becomes empty

	return true  # Placeholder

#endregion

#region Visualization

## Display the solution by placing treasure objects
func visualize_solution() -> void:
	# Clear previous treasures
	for treasure in treasure_objects:
		treasure.queue_free()
	treasure_objects.clear()

	# Place treasure objects
	for pos in treasure_positions:
		var treasure = create_treasure(pos)
		treasure_objects.append(treasure)

		# Highlight the cell
		set_cell_color(grid_cells[pos.x][pos.y], COLOR_TREASURE)

	# Display metrics
	if show_metrics:
		display_metrics()

## Create a treasure sprite at the given position
func create_treasure(pos: Vector2i) -> Sprite2D:
	var treasure = Sprite2D.new()
	treasure.position = Vector2(pos.x * cell_size + cell_size / 2,
	                            pos.y * cell_size + cell_size / 2)
	treasure.modulate = Color.GOLD

	# Create a simple treasure visual (circle)
	var texture = create_circle_texture(24, Color.GOLD)
	treasure.texture = texture

	add_child(treasure)
	return treasure

## Helper function to create a circle texture
func create_circle_texture(radius: int, color: Color) -> ImageTexture:
	var img = Image.create(radius * 2, radius * 2, false, Image.FORMAT_RGBA8)

	for x in range(radius * 2):
		for y in range(radius * 2):
			var dx = x - radius
			var dy = y - radius
			if dx * dx + dy * dy <= radius * radius:
				img.set_pixel(x, y, color)

	return ImageTexture.create_from_image(img)

func set_cell_color(cell: ColorRect, color: Color) -> void:
	if cell:
		cell.color = color

func display_metrics() -> void:
	print("=== CSP Performance Metrics ===")
	print("Grid Size: %dx%d" % [grid_size, grid_size])
	print("Treasures Placed: %d/%d" % [treasure_positions.size(), num_treasures])
	print("Total Nodes Explored: %d" % nodes_explored)
	print("Backtracks: %d" % backtrack_count)
	print("Solution: %s" % str(treasure_positions))

#endregion

#region UI

func _draw() -> void:
	if not show_metrics:
		return

	# Draw metrics overlay
	var metrics_text = [
		"CSP Treasure Placement",
		"Treasures: %d/%d" % [treasure_positions.size(), num_treasures],
		"Nodes: %d" % nodes_explored,
		"Backtracks: %d" % backtrack_count
	]

	var y_offset = 10
	for text in metrics_text:
		draw_string(ThemeDB.fallback_font, Vector2(10, y_offset), text, HORIZONTAL_ALIGNMENT_LEFT, -1, 16)
		y_offset += 20

func _input(event: InputEvent) -> void:
	if event is InputEventKey and event.pressed:
		if event.keycode == KEY_SPACE:
			# Solve again on spacebar
			solve_treasure_placement()
			queue_redraw()

#endregion

"""
STUDENT IMPLEMENTATION GUIDE
============================

1. BASIC IMPLEMENTATION (Required):
   - The backtrack_search function is already implemented
   - Test that it works and understand how it explores the search space
   - Observe the number of backtracks needed

2. MRV HEURISTIC (Medium Difficulty):
   - Implement select_most_constrained_variable()
   - Modify backtrack_search to use MRV instead of sequential treasure placement
   - Compare performance: backtracks before vs after MRV

3. LCV HEURISTIC (Medium-Hard Difficulty):
   - Implement order_by_least_constraining()
   - Sort domain values before trying them in backtrack_search
   - Measure impact on performance

4. FORWARD CHECKING (Advanced):
   - Implement forward_check() function
   - Maintain explicit domains for each treasure
   - Call forward_check after each assignment
   - Fail early if any domain becomes empty

5. TESTING:
   - Start with num_treasures = 2, then increase
   - Try different grid sizes
   - Add more constraints (e.g., treasures must form certain patterns)

DEBUGGING TIPS:
- Use print() to output state during search
- Visualize which cells are being tried
- Add a timer to see the search process animated
- Count how many times each constraint is checked

GODOT-SPECIFIC TIPS:
- Press SPACE to re-solve the problem
- Use the inspector to adjust grid_size and num_treasures
- Colors are defined as constants for easy customization
- Use queue_redraw() to update the visual display
"""
