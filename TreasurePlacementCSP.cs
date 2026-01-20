using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CSP-based Treasure Placement System
/// In-Class Activity - CST-415 Topic 2
///
/// This script demonstrates Constraint Satisfaction Problem solving
/// for placing treasures in a dungeon grid with multiple constraints.
/// </summary>
public class TreasurePlacementCSP : MonoBehaviour
{
    [Header("Grid Configuration")]
    [SerializeField] private int gridSize = 8;
    [SerializeField] private int numTreasures = 5;
    [SerializeField] private float cellSize = 1.0f;

    [Header("Visualization")]
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private GameObject treasurePrefab;
    [SerializeField] private Material validCellMaterial;
    [SerializeField] private Material invalidCellMaterial;
    [SerializeField] private Material spawnZoneMaterial;
    [SerializeField] private Material treasureCellMaterial;

    [Header("Performance Metrics")]
    [SerializeField] private bool showMetrics = true;
    private int backtrackCount = 0;
    private int nodesExplored = 0;

    // Data structures for CSP
    private List<Vector2Int> treasurePositions;
    private HashSet<Vector2Int> invalidPositions;
    private GameObject[,] gridCells;
    private List<GameObject> treasureObjects;

    private void Start()
    {
        InitializeGrid();
        SolveTreasurePlacement();
    }

    #region Grid Initialization

    /// <summary>
    /// Creates the visual grid and marks invalid positions
    /// </summary>
    private void InitializeGrid()
    {
        gridCells = new GameObject[gridSize, gridSize];
        invalidPositions = new HashSet<Vector2Int>();
        treasureObjects = new List<GameObject>();

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 position = new Vector3(x * cellSize, 0, y * cellSize);
                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity, transform);
                cell.name = $"Cell_{x}_{y}";
                gridCells[x, y] = cell;

                // Mark invalid positions
                if (IsInvalidCell(x, y))
                {
                    invalidPositions.Add(new Vector2Int(x, y));
                    SetCellMaterial(cell, invalidCellMaterial);
                }
                else if (IsSpawnZone(x, y))
                {
                    invalidPositions.Add(new Vector2Int(x, y));
                    SetCellMaterial(cell, spawnZoneMaterial);
                }
                else
                {
                    SetCellMaterial(cell, validCellMaterial);
                }
            }
        }
    }

    /// <summary>
    /// Check if cell is too close to edges (constraint: 1 cell from walls)
    /// </summary>
    private bool IsInvalidCell(int x, int y)
    {
        return x == 0 || x == gridSize - 1 || y == 0 || y == gridSize - 1;
    }

    /// <summary>
    /// Check if cell is in the spawn zone (top-left 2x2)
    /// </summary>
    private bool IsSpawnZone(int x, int y)
    {
        return x < 2 && y < 2;
    }

    #endregion

    #region CSP Solver

    /// <summary>
    /// Main CSP solving function - finds valid treasure placement
    /// </summary>
    private void SolveTreasurePlacement()
    {
        Debug.Log("=== Starting CSP Treasure Placement ===");
        backtrackCount = 0;
        nodesExplored = 0;
        treasurePositions = new List<Vector2Int>();

        float startTime = Time.realtimeSinceStartup;
        bool success = BacktrackSearch(0);
        float endTime = Time.realtimeSinceStartup;

        if (success)
        {
            Debug.Log($"✓ Solution found! Placed {treasurePositions.Count} treasures");
            Debug.Log($"Performance: {nodesExplored} nodes explored, {backtrackCount} backtracks");
            Debug.Log($"Time: {(endTime - startTime) * 1000:F2}ms");
            VisualizeSolution();
        }
        else
        {
            Debug.LogError("✗ No solution found!");
        }
    }

    /// <summary>
    /// Backtracking search algorithm
    /// TODO: Students implement this function
    /// </summary>
    /// <param name="treasureIndex">Current treasure being placed (0 to numTreasures-1)</param>
    /// <returns>True if solution found, false otherwise</returns>
    private bool BacktrackSearch(int treasureIndex)
    {
        nodesExplored++;

        // Base case: all treasures placed successfully
        if (treasureIndex >= numTreasures)
        {
            return true;
        }

        // TODO: Implement variable selection with MRV heuristic
        // For now, we just try to place treasures in order

        // Get domain of valid positions for this treasure
        List<Vector2Int> domain = GetDomain(treasureIndex);

        // TODO: Implement Least Constraining Value ordering
        // For now, try positions in order

        foreach (Vector2Int position in domain)
        {
            // Try assigning this position
            treasurePositions.Add(position);

            // Check if this assignment is consistent with constraints
            if (IsConsistent(position, treasureIndex))
            {
                // Recursively try to place remaining treasures
                if (BacktrackSearch(treasureIndex + 1))
                {
                    return true; // Solution found!
                }
            }

            // Assignment failed, backtrack
            treasurePositions.RemoveAt(treasurePositions.Count - 1);
            backtrackCount++;
        }

        return false; // No valid assignment found
    }

    /// <summary>
    /// Get the domain (valid positions) for a treasure
    /// TODO: Students can optimize this with forward checking
    /// </summary>
    private List<Vector2Int> GetDomain(int treasureIndex)
    {
        List<Vector2Int> domain = new List<Vector2Int>();

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);

                // Skip invalid positions
                if (invalidPositions.Contains(pos))
                    continue;

                // Skip already occupied positions
                if (treasurePositions.Contains(pos))
                    continue;

                domain.Add(pos);
            }
        }

        return domain;
    }

    /// <summary>
    /// Check if a position satisfies all constraints
    /// </summary>
    private bool IsConsistent(Vector2Int position, int currentTreasureIndex)
    {
        // Check constraints against all previously placed treasures
        for (int i = 0; i < treasurePositions.Count - 1; i++)
        {
            Vector2Int otherTreasure = treasurePositions[i];

            // Constraint 1: No two treasures in same position (handled by domain)
            if (position == otherTreasure)
                return false;

            // Constraint 2: Minimum Manhattan distance of 2
            int manhattanDistance = Mathf.Abs(position.x - otherTreasure.x) +
                                   Mathf.Abs(position.y - otherTreasure.y);
            if (manhattanDistance < 2)
                return false;

            // Constraint 3: Not adjacent (no horizontal or vertical neighbors)
            bool isAdjacent = (Mathf.Abs(position.x - otherTreasure.x) <= 1 &&
                              Mathf.Abs(position.y - otherTreasure.y) <= 1 &&
                              manhattanDistance > 0);
            if (isAdjacent && manhattanDistance == 1)
                return false;
        }

        return true;
    }

    #endregion

    #region Heuristics (TODO for students)

    /// <summary>
    /// TODO: Implement Most Constrained Variable (MRV) heuristic
    /// Select the treasure position that has the fewest remaining valid positions
    /// </summary>
    private int SelectMostConstrainedVariable()
    {
        // STUDENT TODO: Implement MRV
        // Hint: For each unplaced treasure, count how many valid positions remain
        // Return the index of the treasure with fewest options

        return -1; // Placeholder
    }

    /// <summary>
    /// TODO: Implement Least Constraining Value (LCV) heuristic
    /// Order domain values by how many options they eliminate for other variables
    /// </summary>
    private List<Vector2Int> OrderByLeastConstraining(List<Vector2Int> domain)
    {
        // STUDENT TODO: Implement LCV
        // Hint: For each position in domain, count how many positions it would
        // eliminate from other treasures' domains
        // Sort by ascending number of eliminations

        return domain; // Placeholder - return unsorted
    }

    /// <summary>
    /// TODO: Implement forward checking
    /// After placing a treasure, reduce domains of remaining treasures
    /// </summary>
    private bool ForwardCheck(Vector2Int newPosition)
    {
        // STUDENT TODO: Implement forward checking
        // Hint: For each unplaced treasure, remove values from its domain
        // that are now inconsistent with newPosition
        // Return false if any domain becomes empty

        return true; // Placeholder
    }

    #endregion

    #region Visualization

    /// <summary>
    /// Display the solution by placing treasure objects
    /// </summary>
    private void VisualizeSolution()
    {
        // Clear previous treasures
        foreach (GameObject treasure in treasureObjects)
        {
            Destroy(treasure);
        }
        treasureObjects.Clear();

        // Place treasure objects
        foreach (Vector2Int pos in treasurePositions)
        {
            Vector3 worldPos = new Vector3(pos.x * cellSize, 0.5f, pos.y * cellSize);
            GameObject treasure = Instantiate(treasurePrefab, worldPos, Quaternion.identity, transform);
            treasure.name = $"Treasure_{pos.x}_{pos.y}";
            treasureObjects.Add(treasure);

            // Highlight the cell
            SetCellMaterial(gridCells[pos.x, pos.y], treasureCellMaterial);
        }

        // Display metrics
        if (showMetrics)
        {
            DisplayMetrics();
        }
    }

    private void SetCellMaterial(GameObject cell, Material mat)
    {
        if (cell != null && mat != null)
        {
            Renderer renderer = cell.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = mat;
            }
        }
    }

    private void DisplayMetrics()
    {
        Debug.Log("=== CSP Performance Metrics ===");
        Debug.Log($"Grid Size: {gridSize}x{gridSize}");
        Debug.Log($"Treasures Placed: {treasurePositions.Count}/{numTreasures}");
        Debug.Log($"Total Nodes Explored: {nodesExplored}");
        Debug.Log($"Backtracks: {backtrackCount}");
        Debug.Log($"Solution: {string.Join(", ", treasurePositions)}");
    }

    #endregion

    #region GUI (Optional)

    private void OnGUI()
    {
        if (!showMetrics) return;

        GUILayout.BeginArea(new Rect(10, 10, 300, 200));
        GUILayout.Label($"CSP Treasure Placement", GUI.skin.box);
        GUILayout.Label($"Treasures: {treasurePositions.Count}/{numTreasures}");
        GUILayout.Label($"Nodes: {nodesExplored}");
        GUILayout.Label($"Backtracks: {backtrackCount}");

        if (GUILayout.Button("Solve Again"))
        {
            SolveTreasurePlacement();
        }

        GUILayout.EndArea();
    }

    #endregion
}

/*
 * STUDENT IMPLEMENTATION GUIDE
 * ============================
 *
 * 1. BASIC IMPLEMENTATION (Required):
 *    - The BacktrackSearch function is already implemented
 *    - Test that it works and understand how it explores the search space
 *    - Observe the number of backtracks needed
 *
 * 2. MRV HEURISTIC (Medium Difficulty):
 *    - Implement SelectMostConstrainedVariable()
 *    - Modify BacktrackSearch to use MRV instead of sequential treasure placement
 *    - Compare performance: backtracks before vs after MRV
 *
 * 3. LCV HEURISTIC (Medium-Hard Difficulty):
 *    - Implement OrderByLeastConstraining()
 *    - Sort domain values before trying them in BacktrackSearch
 *    - Measure impact on performance
 *
 * 4. FORWARD CHECKING (Advanced):
 *    - Implement ForwardCheck() function
 *    - Maintain explicit domains for each treasure
 *    - Call ForwardCheck after each assignment
 *    - Fail early if any domain becomes empty
 *
 * 5. TESTING:
 *    - Start with numTreasures = 2, then increase
 *    - Try different grid sizes
 *    - Add more constraints (e.g., treasures must form certain patterns)
 *
 * DEBUGGING TIPS:
 * - Use Debug.Log to print state during search
 * - Visualize which cells are being tried
 * - Add a delay to see the search process animated
 * - Count how many times each constraint is checked
 */
