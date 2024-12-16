# Unity Maze Navigation with ML-Agents

This project involves training a red robot to navigate a 3D maze using Unity and the Unity ML-Agents Toolkit. The robot's goal is to reach a pink robot (reward) while avoiding walls.

## How It Works

### Maze and Setup:
- A simple 3D maze is created using Unity cubes for walls.
- The red robot acts as the AI agent.
- The pink robot serves as the goal/reward.

### Rules for the Robot:
- If the red robot collides with the pink robot, it gets a large positive reward.
- If the red robot hits a wall, it receives a negative reward (penalty).
- Raycasts are used to help the robot "see" its surroundings, such as detecting walls and the reward.

### Training the Robot:
- Reinforcement learning (via Unity ML-Agents) is used to train the robot.
- The robot starts by moving randomly, but over time, it learns to:
  - Avoid walls.
  - Find the pink robot faster.
- Fine-tuning the rewards ensures the robot learns the correct behavior.

## Results
After training, the red robot successfully learned to navigate the maze. It avoided walls and reached the pink robot efficiently.

## What I Learned
This project helped me understand how to:
- Use Raycasts to give the AI an idea of its environment.
- Design rewards and penalties to guide the AI’s behavior.
- Train an AI agent using the Unity ML-Agents Toolkit.

## How to Run the Project

1. Download or clone this repository to your local machine.
2. Open the project in Unity.
3. Ensure that the Unity ML-Agents Toolkit is installed and set up in your Unity project.
4. Import the provided script file into your Unity project.
5. Set up the 3D maze and robots as described in the script (ensure to assign the required objects like the target, checkpoints, and the maze walls in the Unity Editor).
6. Press `Play` in Unity to run the simulation and observe the AI agent training and navigating the maze.

## Requirements
- Unity (latest stable version)
- ML-Agents Toolkit (installed in the Unity project)
