using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.UI;
using OptionalUnity;

public class BaseAgent : Agent
{
    public QuarkGroup Quarks;
    private Vector3 StartPosition;
    private Quaternion StartRotation;
    [HideInInspector]
    public StartEpisodeAction[] StartEpisodeActions;
    [HideInInspector]
    public PersonalityQuarksArea area;
    public bool ResetArea = true;
    private int lastRewardedStepCount = 1;

    public void Start()
    {
        area = GetComponentInParent<PersonalityQuarksArea>();

        if (area.Playback)
        {
            enabled = false;
            if (GetComponent<Rigidbody>() != null)
            {
                GetComponent<Rigidbody>().isKinematic = true;
            }
            return;
        }

        StartEpisodeActions = GetComponents<StartEpisodeAction>();
        StartPosition = transform.position;
        StartRotation = transform.rotation;
    }

    public override void Initialize()
    {
        base.Initialize();

        if (area == null)
        {
            area = GetComponentInParent<PersonalityQuarksArea>();
        }

        Quarks = QuarkGroup.Instantiate(Quarks);
        Quarks.Initialize(this);

        // TAG: MakeEvent area.Logger.Log(Logger.CreateMessage(LogMessageType.World, $"Initialized {Quarks.name} {gameObject.name}"), this); 
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Quarks.CollectObservations(this, sensor);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        Quarks.AgentAction(this, vectorAction);
    }

    void FixedUpdate()
    {
        // Reward only after an action is taken.
        Quarks.RewardAgent(this, StepCount - lastRewardedStepCount);
        lastRewardedStepCount = 1;

        Quarks.FixedUpdate();
    }

    public override void OnEpisodeBegin()
    {
        if (ResetArea)
        {
            area.ResetArea();
        }

        foreach (StartEpisodeAction action in StartEpisodeActions)
        {
            action.OnStartEpisodePreReset();
        }

        Quarks.Reset(this);

        foreach (StartEpisodeAction action in StartEpisodeActions)
        {
            action.OnStartEpisodePostReset();
        }
    }
}
