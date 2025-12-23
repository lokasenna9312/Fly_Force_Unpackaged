using UnityEngine;

namespace Player
{
    public class BulletBombController : TardionProjectileController
    {
        [SerializeField] private GameObject trail;
        [SerializeField] private Transform nozzle;
        private Rigidbody2D _momentum;
        protected override Rigidbody2D momentum
        {
            get => _momentum;
            set => _momentum = value;
        }
        [SerializeField] private float _acceleration;
        protected override float acceleration
        {
            get => _acceleration;
            set => _acceleration = value;
        }
        [SerializeField] private float _burstTime;
        protected override float burstTime
        {
            get => _burstTime;
            set => _burstTime = value;
        }
        private int _damage;
        protected override int damagePoint
        {
            get => _damage;
            set => _damage = value;
        }
        protected override int missedShotPenalty => 100;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected override void Start()
        {
            base.Start();
            if (playerController != null)
                damagePoint = playerController.BombDamage;
        }

        protected override void Update()
        {
            base.Update();
            if (trail != null && time < burstTime)
            {
                Instantiate(trail, nozzle.position, Quaternion.identity);
            }
        }

        protected new void FixedUpdate()
        {
            base.FixedUpdate();
        }
    }
}