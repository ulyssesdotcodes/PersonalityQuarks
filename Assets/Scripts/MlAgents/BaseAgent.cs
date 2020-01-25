using System.Collections;
using System.Collections.Generic;
using MLAgents;
using UnityEngine;
using UnityEngine.UI;
using OptionalUnity;

public class BaseAgent : Agent {
    public QuarkGroup Quarks;

    public Option<Collider> TriggerCollider;

    public List<string> ColliderTags = new List<string>();

    private Vector3 StartPosition;
    private Quaternion StartRotation;
    public PersonalityQuarksArea area;

    public void Start() {
      if(area == null) {
        area = GetComponentInParent<PersonalityQuarksArea>();
      }
      StartPosition = transform.position;
      StartRotation = transform.rotation;
    }

    public override void InitializeAgent()
    {
        base.InitializeAgent();

        if(area == null) {
          area = GetComponentInParent<PersonalityQuarksArea>();
        }

        Quarks = QuarkGroup.Instantiate(Quarks);
        Quarks.Initialize(this);

        // TAG: MakeEvent area.Logger.Log(Logger.CreateMessage(LogMessageType.World, $"Initialized {Quarks.name} {gameObject.name}"), this); 
    }

    public override void CollectObservations() {
        AddVectorObs(Quarks.CollectObservations(this));
    }

    public override void AgentAction(float[] vectorAction) {
        Quarks.AgentAction(this, vectorAction);
    }

    public override void AgentReset() {
        base.AgentReset();
        Reset();
    }

    public void Reset()
    {
      //TAG: MakeEvent area.Logger.Log(System.String.Concat("Reset ", gameObject.transform.position.y));
      Quarks.Reset(this);
    }

    public void OnTriggerEnter(Collider col) {
        if(ColliderTags.Contains(col.gameObject.tag)) {
          //TAG: MakeEvent area.Logger.Log(Logger.CreateMessage(LogMessageType.Debug, $"Ran into {col.gameObject.tag}"), this); 
          TriggerCollider = col.Some();
        }
    }

    public void OnTriggerExit(Collider col) {
        if(ColliderTags.Contains(col.gameObject.tag)) {
          //TAG: MakeEvent area.Logger.Log(Logger.CreateMessage(LogMessageType.Debug, $"Ran away from {col.gameObject.tag}"), this); 
          TriggerCollider = Option.None<Collider>();
        }
    }

    public void OnCollisionEnter(Collision col) {
        if(ColliderTags.Contains(col.gameObject.tag)) {
          //TAG: MakeEvent area.Logger.Log(Logger.CreateMessage(LogMessageType.Debug, $"Ran into {col.gameObject.name}"), this); 
          TriggerCollider = col.collider.Some();
          if(area.EventSystem != null) {
            area.EventSystem.RaiseEvent(CollisionEnterEvent.Create(gameObject, col));
          }
        }
    }

    public void OnCollisionExit(Collision col) {
        if(ColliderTags.Contains(col.gameObject.tag)) {
          //TAG: MakeEvent area.Logger.Log(Logger.CreateMessage(LogMessageType.Debug, $"Ran away from {col.gameObject.name}"), this); 
          TriggerCollider = Option.None<Collider>();
          if(area.EventSystem != null) {
            area.EventSystem.RaiseEvent(CollisionExitEvent.Create(gameObject, col));
          }
        }
    }
}
