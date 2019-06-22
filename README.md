# Personality Quarks

An experiment in modular behaviours for Unity ML-Agents. It's a quarky project.

- `ScriptableObject`s define the smallest units of ML-Agent behaviour.
- `MLObs` is the base for observations, `MLReward` the base for rewards, and `MLAction` the base for actions
- `BaseAgent` combines all these quarks to create a personality that can be trained.