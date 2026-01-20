# CST-415 Topic 2: Constraint Satisfaction Problems
## Complete Teaching Materials

**Created:** January 19, 2026
**Course:** AI in Games and Simulations
**Topic Duration:** Jan 19 - Feb 1, 2026

---

## 📦 Package Contents

This directory contains all materials for teaching Topic 2: Constraint Satisfaction Problems (CSP).

### 1. Interactive Slide Deck
**File:** `topic2_csp_slides.html`

- **Format:** Self-contained HTML5 presentation
- **Slides:** 12 comprehensive slides
- **Features:**
  - Interactive 4-Queens problem demo
  - Mathematical formulations
  - Algorithm pseudocode
  - Game development examples
  - Keyboard navigation (Arrow keys, Space)

**To Use:**
- Open `topic2_csp_slides.html` in any modern web browser
- Navigate with arrow keys or on-screen buttons
- Click on the chessboard to interact with the N-Queens demo

### 2. In-Class Activity
**Files:**
- `InClass_Activity_CSP_Treasure_Placement.md` - Activity instructions
- `TreasurePlacementCSP.cs` - Unity C# starter code
- `treasure_placement_csp.gd` - Godot GDScript starter code

**Activity:** Treasure Placement CSP
- **Duration:** 60-75 minutes
- **Platform:** Unity 3D or Godot
- **Difficulty:** Beginner to Intermediate
- **Objectives:**
  - Formulate a game problem as a CSP
  - Implement backtracking search
  - Apply Most Constrained Variable (MRV) heuristic
  - Understand constraint checking

---

## 🎯 Learning Objectives

Students will be able to:

1. **Design solutions** to problems with multiple concurrent constraints
2. **Implement solutions** using a least constraining value method
3. **Use objective functions** as solution drivers

---

## 📚 Slide Deck Content Overview

### Slide 1: Title & Objectives
Introduction to CSP and learning goals

### Slide 2: What is a CSP?
- Variables, Domains, Constraints
- Complete vs Consistent assignments
- Mathematical formulation

### Slide 3: Map Coloring Example
Classic CSP problem with game context

### Slide 4: N-Queens Problem
- Interactive demo
- CSP formulation for chess problem
- Constraint visualization

### Slide 5: Knights Problem
Lab question from syllabus with detailed CSP formulation

### Slide 6: Backtracking Algorithm
Complete pseudocode with explanation

### Slide 7: Most Constrained Variable (MRV)
- Fail-first heuristic
- Why it works
- Game examples

### Slide 8: Least Constraining Value (LCV)
- Success-first heuristic
- Combining with MRV
- Strategic explanation

### Slide 9: Forward Checking
- Inference technique
- Pseudocode
- Example walkthrough

### Slide 10: Arc Consistency (AC-3)
- Stronger constraint propagation
- Complete AC-3 algorithm
- Revise function

### Slide 11: Game Applications
- Level generation
- Puzzle games
- Character scheduling
- Animation/IK
- Dungeon room placement example

### Slide 12: Summary
- Key takeaways
- Project guidelines
- Next steps

---

## 🎮 In-Class Activity Details

### Problem: Treasure Placement

Place K=5 treasures on an 8×8 grid with constraints:

**Constraints:**
1. No adjacent treasures (horizontally/vertically)
2. Safe zone: No treasures in top-left 2×2 (spawn area)
3. Distance from walls: At least 1 cell away from edges
4. Minimum separation: Manhattan distance ≥ 2 between any two treasures

### Implementation Tasks

**Part 1 (10 min):** Setup
- Create Unity/Godot project
- Set up 8×8 grid
- Attach provided script

**Part 2 (40 min):** Implementation
1. **Task 1 (10 min):** Define CSP structure
2. **Task 2 (15 min):** Implement backtracking
3. **Task 3 (10 min):** Add MRV heuristic
4. **Task 4 (5 min):** Visualization

**Part 3 (10 min):** Testing & Discussion
- Test cases
- Performance comparison
- Discussion questions

### Extension Challenges (Optional)

1. Implement LCV heuristic
2. Add forward checking
3. Create different constraints
4. Add performance metrics
5. Interactive constraint violation checking

---

## 🔧 Technical Requirements

### For Slide Deck
- Modern web browser (Chrome, Firefox, Safari, Edge)
- JavaScript enabled
- No internet connection required (self-contained)

### For Unity Version
- Unity 2020.3 or later
- C# 7.3+
- Prefabs needed:
  - Cell prefab (cube or plane)
  - Treasure prefab (any visual representation)
- Materials:
  - Valid cell material (light gray)
  - Invalid cell material (dark red)
  - Spawn zone material (blue)
  - Treasure cell material (gold)

### For Godot Version
- Godot 4.0 or later
- GDScript 2.0
- Built-in scene creation (no external assets needed)

---

## 📖 Connection to Course Content

### Textbook Reference
**Artificial Intelligence: A Modern Approach** (Russell & Norvig, 4th ed.)
- Chapter 6, Sections 1-5: Constraint Satisfaction Problems

### Lab Questions Covered

**Lab Question 1 (Jan 25):**
Knights on chessboard problem - formulation covered in Slide 5

**Lab Question 2 (Feb 1):**
Most constrained variable + Least constraining value - covered in Slides 7-8

### Project 2 Preparation

This activity prepares students for **Project 2: Least Constraining Value Heuristic** by:

1. **CSP Formulation:** Practice converting game problems to CSP
2. **Backtracking:** Implement core search algorithm
3. **Heuristics:** Experience with MRV, groundwork for LCV
4. **Game Context:** Apply CSP to actual game scenarios

**Students will extend this to Project 2 by:**
- Choosing their own game/simulation scenario
- Implementing both MRV and LCV
- Adding forward checking or AC-3
- Creating complex visualizations
- Integrating into larger project

---

## 🎓 Teaching Tips

### For the Slide Deck

1. **Interactive Demo:** Encourage students to solve 4-Queens on Slide 4
2. **Pause Points:** Stop at Slides 6, 8, and 10 for questions
3. **Code Walkthrough:** Spend extra time on algorithm pseudocode
4. **Real Examples:** Ask students for game ideas that could use CSP

### For the In-Class Activity

1. **Start Simple:** Have students test with K=2 treasures first
2. **Pair Programming:** Students can work in pairs
3. **Live Coding:** Demonstrate MRV implementation if needed
4. **Performance Focus:** Emphasize counting backtracks before/after optimization
5. **Discussion:** Reserve 10 minutes for whole-class discussion

### Common Student Struggles

1. **Constraint Formulation:** Students may struggle translating English to math
   - *Solution:* Work through the knights problem together

2. **Recursion:** Backtracking can be conceptually challenging
   - *Solution:* Draw the search tree on the board

3. **Heuristics:** Why MRV + LCV combination works
   - *Solution:* Use the "hardest first, easiest value" metaphor

4. **Off-by-One Errors:** Grid indexing issues
   - *Solution:* Emphasize testing with small grids first

---

## 📊 Assessment Opportunities

### Formative Assessment (In-Class)
- Working code demonstration
- Screenshot of valid solution
- Brief writeup (2-3 sentences)
- Participation in discussion

### Summative Assessment (Project 2)
- Full CSP implementation
- MRV and LCV heuristics
- Forward checking or AC-3
- Integration with game project
- Documentation and visualization

---

## 🔄 Iteration & Updates

### Version History
- **v1.0 (Jan 19, 2026):** Initial creation
  - 12-slide interactive deck
  - In-class activity with Unity & Godot versions
  - Complete documentation

### Future Improvements
- Add more interactive demos to slides
- Create video walkthrough of activity
- Add profiler integration for Unity version
- Create pre-built Unity package with materials

---

## 📞 Support & Questions

For questions about these materials:
- **Instructor:** Isac Artzi (Isac.Artzi@gcu.edu)
- **Course:** CST-415 AI in Games and Simulations
- **Institution:** Grand Canyon University

---

## 📜 License & Attribution

These materials are created for CST-415 at Grand Canyon University.

**Textbook:**
Russell, S., and Norvig, P. (2020). *Artificial Intelligence: A Modern Approach* (4th ed.). Pearson. ISBN-13: 9780134610993.

**Original Materials by:** AI Teaching Assistant
**Date:** January 19, 2026
**Course Designer:** Isac Artzi

---

## ✅ Pre-Class Checklist

- [ ] Review slide deck (test interactive demo)
- [ ] Prepare Unity/Godot project template
- [ ] Test starter code on target platform
- [ ] Print activity handout (optional)
- [ ] Set up classroom computers with Unity/Godot
- [ ] Prepare whiteboard for algorithm diagrams
- [ ] Queue up discussion questions
- [ ] Have example solutions ready for debugging help

---

## 🎯 Post-Class Follow-Up

1. **Collect Submissions:** In-class activity deliverables
2. **Grade:** Quick check for completion (participation points)
3. **Feedback:** Note common issues for next class
4. **Project 2 Prep:** Remind students of proposal deadline
5. **Office Hours:** Announce availability for CSP questions

---

Good luck with your lesson! These materials provide a comprehensive introduction to CSP with practical game development applications.
