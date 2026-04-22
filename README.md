# Dodge Agent

## 📌 Overview
This project implements a reinforcement learning environment using **Unity ML-Agents**.  
An agent is trained to **avoid dynamically moving obstacles** and maximize its survival time.



https://github.com/user-attachments/assets/af00ca18-d131-47ff-9a8f-8114790dd914


---

## 🧠 Environment Description
- 3D Unity environment
- Agent: cube
- Obstacles: moving red spheres
- Objective: avoid collisions for as long as possible

The environment includes **dynamic obstacle behavior**, making the task non-trivial and suitable for reinforcement learning.

---

## ⚙️ Key Features
- Continuous action space (agent controls horizontal movement)
- Ray Perception Sensor for environment awareness
- Dynamic obstacles with randomized speed and movement patterns
- Custom PPO training configuration (YAML)
- TensorBoard integration for training analysis
- Episode-based reward system

---

## 🎮 Agent Behavior
The agent:
- Moves left and right across the platform
- Receives a small positive reward for each timestep survived
- Receives a negative reward upon collision with an obstacle
- Ends the episode when a collision occurs

---

## 🏋️ Training

Training is performed using the **Proximal Policy Optimization (PPO)** algorithm.

### Run training:
```bash
mlagents-learn config/turtle.yaml --run-id=runX
```

---

> <small><i>Made for Advanced Reinforcement Learning course</i></small>
