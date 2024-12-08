using Assets.Entities;
using Assets.Game;
using Interlace;
using System;
using TeaSteep.Character.Status.Effect;
using TeaSteep.Character.Status;
using UnityEngine;
using UnityEngine.AI;
using TeaAI;
using Interlace.Test;
using TeaSteep.Character.Controller;
using TeaAI.Example;
using TeaAI.Example.Planning.ShallowFast;
using Assets.Game.Character;
using TeaAI.Example.Actions;

namespace Assets.Coordinators.AI
{
    public class AgentCoord : ActorEntityProxy, IAgentAI, ICoordinator<ActorHandler>
    {
        private StatusManager status;
        private EffectManager effects;
        private StatusEventModule statusEventModule;

        [SerializeField]
        private AgentMind mind;
        //[SerializeField]
        //private NavMeshAgent navAgent;
        //[SerializeField]
        //private NavMeshObstacle agentObstacle;

        //public NavMeshAgent NavAgent { get => navAgent; }
        //public NavMeshObstacle Obstacle { get => agentObstacle; }

        //public AgentMind Mind { get => mind; set => mind = value; }
        public StatusManager Status => throw new NotImplementedException();
        public EffectManager Effects => throw new NotImplementedException();

        public ActorController Controller { get; private set; }
        public Vector3 Position { get => transform.position; }
        public Vector3 Forward { get => Position + transform.forward; }
        public Weapon Primary { get; set; }

        public void InflictDamage(int damage)
        {
            var currentHealth = Health - damage;

            Health = currentHealth;

            statusEventModule.RaiseDamageTaken(damage);
            //mind.AddAlert(AlertHelper.DamageAlert(this, damage));
        }

        public void Initialize()
        {
            mind = GetComponent<AgentMind>();

            //Controller = GetComponent<ActorController>();
            if (mind.Agent == null)
            {
                mind.Agent = this;
                mind.SetStrategizer(new ShallowFastStrategizer());
            }
        }

        private void Awake()
        {
            Controller = GetComponent<ActorController>();
            statusEventModule = GetComponent<StatusEventModule>();
        }

        public void SetHandler(ActorHandler handler)
        {
            this.handler = handler;
        }

        private void Update()
        {
            mind.UpdateMind();
            Controller.ControllerUpdate();
        }

        public void FirePrimary(AttackAction attackAction)
        {
            Primary?.Fire(0);
        }
    }
}
