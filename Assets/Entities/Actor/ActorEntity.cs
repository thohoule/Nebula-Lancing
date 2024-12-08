using System.Collections;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;

namespace Assets.Entities
{
    public class ActorEntity : MonoBehaviour, IActorEntity
    {
        [SerializeField]
        private int health;
        [SerializeField]
        private int maxHealth;
        [SerializeField]
        private int shield;
        [SerializeField]
        private int maxShield;
        [SerializeField]
        private int shieldRefreshRate;
        [SerializeField]
        private float shieldRefreshDelay;
        [SerializeField]
        private bool isDead;

        public int Health { get => health; set => health = value; }
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        public int Shield { get => shield; set => shield = value; }
        public int MaxShield { get => maxShield; set => maxShield = value; }
        public int ShieldRefreshRate { get => shieldRefreshRate; set => shieldRefreshRate = value; }
        public float ShieldRefreshDelay { get => shieldRefreshDelay; set => shieldRefreshDelay = value; }
        public bool IsDead { get => isDead; set => isDead = value; }
    }
}
