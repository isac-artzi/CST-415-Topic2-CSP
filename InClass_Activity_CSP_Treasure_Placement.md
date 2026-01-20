# In-Class Activity: CSP Treasure Placement
## CST-415 Topic 2: Constraint Satisfaction Problems

### Activity Duration: 60-75 minutes

---

## Overview

In this hands-on activity, you will implement a Constraint Satisfaction Problem (CSP) to place treasure chests in a dungeon grid. This activity introduces core CSP concepts that you'll use in Project 2 (Least Constraining Value Heuristic).

### Learning Objectives
- Formulate a game problem as a CSP (variables, domains, constraints)
- Implement basic backtracking search
- Apply the Most Constrained Variable (MRV) heuristic
- Understand constraint checking in game contexts

---

## Problem Description

### The Treasure Placement Problem

You are designing a dungeon level for your game. You need to place **K treasure chests** on an **N×N grid** following these rules:

#### Constraints:
1. **No Adjacent Treasures**: Treasures cannot be placed in cells that are horizontally or vertically adjacent
2. **Safe Zone**: Treasures cannot be placed in the top-left 2×2 area (player spawn zone)
3. **Distance from Walls**: Treasures must be at least 1 cell away from the edges
4. **Minimum Separation**: Any two treasures must be at least 2 cells apart (Manhattan distance)

#### Configuration:
- Grid Size: 8×8
- Number of Treasures: 5

---

## CSP Formulation

### Variables
```
T₁, T₂, T₃, T₄, T₅ (5 treasure positions)
```

### Domains
```
For each Tᵢ:
  Domain = {(x, y) | 1 < x < 8, 1 < y < 8, (x, y) not in spawn zone}
```

### Constraints
```
For all i ≠ j:
1. Tᵢ ≠ Tⱼ (different positions)
2. |Tᵢ.x - Tⱼ.x| + |Tᵢ.y - Tⱼ.y| ≥ 2 (Manhattan distance ≥ 2)
3. Not adjacent: |Tᵢ.x - Tⱼ.x| ≤ 1 AND |Tᵢ.y - Tⱼ.y| ≤ 1 → FALSE
4. Spawn zone: (Tᵢ.x < 2) AND (Tᵢ.y < 2) → FALSE
```

---

## Part 1: Setup (10 minutes)

### Unity Setup
1. Create a new Unity 3D project
2. Create a new C# script called `TreasurePlacementCSP.cs`
3. Create a simple 8×8 grid using Unity Cubes or Plane with GridLayout
4. Attach the script to a GameObject

### Godot Setup
1. Create a new Godot project
2. Create a 2D scene with a TileMap (8×8 grid)
3. Create a new GDScript called `treasure_placement_csp.gd`
4. Attach the script to the main node

---

## Part 2: Implementation Tasks (40 minutes)

### Task 1: Define the CSP Structure (10 min)
Implement the data structures for:
- Grid representation
- Treasure positions
- Domain of valid positions
- Constraint checking functions

### Task 2: Implement Backtracking (15 min)
Create a backtracking search function that:
- Selects unassigned treasure positions
- Tries values from the domain
- Checks constraints
- Backtracks on failure

### Task 3: Add MRV Heuristic (10 min)
Enhance your variable selection to use the Most Constrained Variable heuristic:
- Count remaining valid positions for each unassigned treasure
- Select the treasure with fewest options first

### Task 4: Visualization (5 min)
Display the solution:
- Show the grid
- Highlight treasure positions
- Mark invalid cells (spawn zone, too close to walls)

---

## Part 3: Testing & Discussion (10 minutes)

### Test Cases

1. **Basic Solution**: Can you find any valid placement?
2. **Count Solutions**: How many valid solutions exist?
3. **Performance**: How many nodes does backtracking explore?
   - Without MRV
   - With MRV

### Discussion Questions
1. How does MRV improve search efficiency?
2. What happens if you increase K to 8 treasures?
3. How would you implement Least Constraining Value here?
4. Can you think of other game scenarios that fit CSP?

---

## Starter Code Provided

### Files Included:
- `TreasurePlacementCSP.cs` (Unity C#)
- `treasure_placement_csp.gd` (Godot GDScript)

### Key Functions to Implement:
```
1. IsValidPosition(x, y, currentTreasures)
   - Check all constraints for a position

2. BacktrackSearch(assignment, remainingTreasures)
   - Recursive backtracking algorithm

3. SelectUnassignedVariable(remainingTreasures)
   - Return next treasure to place (use MRV)

4. GetDomain(treasure, currentAssignment)
   - Return valid positions for a treasure

5. VisualizeSolution(treasures)
   - Display the solution on the grid
```

---

## Extension Challenges (Optional)

If you finish early, try these:

1. **Least Constraining Value**: Implement LCV heuristic for value selection
2. **Forward Checking**: Track domain reductions after each assignment
3. **Different Constraints**: Add "treasure must be visible from entrance" constraint
4. **Performance Metrics**: Count and display number of backtracks
5. **Interactive Mode**: Let user place treasures and check constraint violations

---

## Submission (End of Class)

### Deliverables:
1. Working code (Unity or Godot project)
2. Screenshot of one valid solution
3. Brief write-up (2-3 sentences) answering:
   - How many backtracks did your solution require?
   - How did MRV help (if implemented)?

### Turn in via:
- Upload project folder (compressed) to course platform
- OR demonstrate working solution to instructor

---

## Connection to Project 2

This activity prepares you for **Project 2: Least Constraining Value Heuristic** by:

1. **CSP Formulation Practice**: You've now formulated a game problem as a CSP
2. **Backtracking Implementation**: Core algorithm you'll use in Project 2
3. **Heuristics**: You've implemented MRV; Project 2 adds LCV
4. **Game Context**: Experience translating game constraints into CSP constraints

### For Project 2, you will:
- Choose your own game/simulation scenario
- Implement both MRV and LCV heuristics
- Add forward checking or arc consistency
- Create more complex visualizations
- Integrate into a larger project

---

## Resources

### Russell & Norvig Chapter 6 Key Sections:
- 6.1: Defining CSPs
- 6.3: Backtracking Search
- 6.3.1: Variable and Value Ordering

### Online Resources:
- [CSP Visualization](https://www.cs.ubc.ca/~hoos/Courses/AI/CSP/)
- [Backtracking Algorithm Walkthrough](https://www.youtube.com/watch?v=eqUwSA0xI-s)

---

## Tips for Success

1. **Start Simple**: Get basic backtracking working first, then add heuristics
2. **Test Incrementally**: Test with K=2 treasures before trying K=5
3. **Visualize**: Print/log the grid state to debug constraint issues
4. **Think CSP**: Frame everything in terms of variables, domains, and constraints
5. **Ask Questions**: This is a learning activity - discuss with classmates!

---

Good luck! Remember: CSPs are all about systematically exploring possibilities while respecting constraints. This is a powerful pattern you'll use throughout game AI development.
