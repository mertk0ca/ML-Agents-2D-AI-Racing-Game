Author: Mert Koca
Date: June 14, 2024

Summary
This project involves an artificial intelligence developed using Unity's ML-Agents library, Python, and PyTorch. The goal is to teach an AI model how to drive a car within a Unity environment. This report details the project's requirements, literature review, reinforcement learning algorithm, setup steps, ML-Agents components, different project sections, and results. It emphasizes the importance of using libraries like ML-Agents in the field of artificial intelligence.

1. Introduction
The objective of this project is to develop an AI model trained in a Unity environment to teach it how to drive a car. This report discusses the project's requirements, literature review, reinforcement learning algorithm, setup steps, ML-Agents components, various sections of the project, and results in detail. It also examines the training processes and achievements. This work underscores the significance of using libraries like ML-Agents in AI applications.

2. Requirements
Python 3.9.13
Unity
PyTorch
Visual Studio
ML-Agents Library
3. Literature Review
Python 3.9.13: A general-purpose, high-level, interactive programming language known for its simple and readable syntax. It is modular and has a wide standard library, making it useful in various fields.
PyTorch: An open-source machine learning library based on Python.
Unity: A game engine used for developing games and graphic applications.
ML-Agents Library: An open-source project providing environments for training agents in games and simulations using reinforcement learning methods with a Python API. It supports PyTorch-based applications and can be used for controlling NPC behaviors and automatic game testing.
4. Reinforcement Learning Algorithm
In reinforcement learning, an agent interacts with its environment, performs certain actions, and receives rewards or penalties based on the outcomes. The following are key components illustrated in the reinforcement learning model:

Agent: The AI that makes decisions, learns, selects actions, and aims to maximize rewards.
Environment: The simulation environment with which the agent interacts and receives feedback from.
State: Represents the environment's condition or the agent's observation at time t.
Action: The action chosen by the agent at time t.
Reward: The reward the agent receives at time t+1 based on its action.
Next State: The resulting state of the environment after the agent's action.
5. Setup Steps
Open the command prompt and navigate to the project directory.
Create a virtual environment to avoid conflicts with other Python projects.
Activate the virtual environment.
Ensure pip is up to date.
Install the ML-Agents package.
Install PyTorch and related libraries.
Install protobuf 3.20.3 for faster data transfer.
Install the packaging library to simplify creating, distributing, and versioning Python packages.
Open the Unity project and activate the ML-Agents package from the Package Manager.
Create an empty object in Unity and ensure ML-Agents is active.
6. ML-Agents Components
Behavior Parameters: Used to configure agents' behaviors and learning processes.
Buffer Sensor: Stores and processes various data types.
Camera Sensor: Uses camera images to capture visual data from the game world.
Decision Requester: Allows agents to request decisions as needed.
Demonstration Recorder: Records human gameplay for training purposes.
Grid Sensor: Used in grid-based games to perceive the environment.
Match 3 Actuator: Performs actions in Match 3 style games (e.g., Candy Crush).
Match 3 Sensor: Detects the game state in Match 3 style games.
Ray Perception Sensor 2D/3D: Detects objects in 2D/3D environments.
Render Texture Sensor: Provides data using render textures.
Vector Sensor: Provides environment data using specific vector information.
7. Movement Project
In this project, the agent targets purple rewards on a platform while avoiding invisible walls that penalize contact. The platform color changes based on interactions: green for rewards and red for collisions. This design encourages agents to reach targets while avoiding negative interactions.

8. Project Objects
The project includes four objects: platform, agent, reward, and wall. The agent aims to reach rewards but must restart if it contacts walls.

9. ML-Agents Behaviors
The agent must perform multiple trials to reach the target. To speed up training, the environment is duplicated nine times, allowing simultaneous training of nine agents.

10. Vehicle Control Learning Project
Similar to the Movement Project, the agent targets random rewards on a platform, restarting and receiving penalties if it hits walls, and rewards for reaching targets. The platform color changes based on interactions.

11. Vehicle and Agent Controls
Vehicle controls are accessed by assigning inputs from another code to the agent's movements. Behavior Parameters manage whether the agent is controlled by a user and other settings.

12. Brain Creation, Saving, and Loading
The agent starts learning upon running the Unity project. The trained brain is stored in the project directory.
Copy the brain from the results folder to the Assets folder to make it available in Unity.
Assign the brain to the agent in the Behavior Parameters component.
13. Success and Failure Rates
A panel shows the number of successful and unsuccessful trials, the difference, and the success rate. This panel helps observe the increase in success rate with more trials.

14. Teaching Cornering Project
A simple track with corners was designed to teach the agent to turn left and right. The agent receives penalties for hitting walls and rewards for reaching checkpoints. The reward increases as the agent progresses through checkpoints.

14.1 Checkpoints
When the agent reaches a checkpoint, the next one is activated. The reward for each checkpoint increases progressively. The agent restarts upon reaching the final checkpoint or hitting a wall.

15. Preparing a Pre-Designed Track for Learning
A pre-designed track with frequent checkpoints was created to facilitate the agent's learning process. Each checkpoint activates the next one upon contact.

16. Ray Perception Sensor 2D
Used on the agent to detect objects like checkpoints and walls.

17. Imitation Learning
Imitation learning allows the agent to replicate pre-recorded human behaviors. Necessary observations were added to help the agent understand the target's direction.

17.1 Created Files
To use imitation learning, two folders are created in the project directory: "config" and "Demos".

17.1.1 Config Folder
Contains a .yaml file named MoveToTarget, allowing modification of parameters necessary for training. The location of the demo file is specified here.

17.1.2 Demos Folder
Stores user-recorded behaviors with a .demo extension.

17.1.3 Agent's Direction Relative to Target
A new observation was added to the code to collect the agent's direction vector relative to the target.

18. Recording Behaviors
The Demonstration Recorder component of ML-Agents records user behaviors. Recording is started by checking the box next to the "Record" label.

19. Trainings
After inputting correct parameters and reward/penalty amounts, the agent starts moving. Initially, the agent moves in circles but learns to move straight and turn over time. A best lap time of 3:55:665 was achieved after 33 training rounds.

19.1 Training-1
In the initial training without modifying the reward and penalty system, the agent received penalties for steering and reversing.

19.2 Training-2
In a different setup where the agent did not receive penalties for steering and reversing, the results were improved.

19.3 Training-3
By adjusting the agent's speed and road grip when off-track without changing the reward and penalty system, better results were achieved.

Training	Method	Time
1	Old Reward/Penalty System	3:16:332
2	New Reward/Penalty System	2:53:333
3	Speed and Road Grip Adjustments	2:46:666
