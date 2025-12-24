using UnityEngine;

namespace Player.Ammunition
{
    public class BulletBomb1 : BulletBombController
    {
        // Bullet properties are set in the Inspector.
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
        [SerializeField] private int _damage;
        protected override int damagePoint
        {
            get => _damage;
            set => _damage = value;
        }
        [SerializeField] private GameObject trail;
        [SerializeField] private Transform nozzle;
        protected override int missedShotPenalty => 100;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            if (trail != null && time < burstTime)
            {
                Instantiate(trail, nozzle.position, Quaternion.identity);
            }
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }
    }
}