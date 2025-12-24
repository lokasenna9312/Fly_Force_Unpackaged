using UnityEngine;

namespace Player
{
    public abstract class BulletBombController : TardionProjectileController
    {
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
        private float _startMass;
        protected override float startMass
        {
            get => _startMass;
            set => _startMass = value;
        }
        [SerializeField] private float _fuelMass;
        protected override float fuelMass
        {
            get => _fuelMass;
            set => _fuelMass = value;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        protected virtual void OnDestroy()
        {
            if (playerController != null)
            {
                playerController.BulletBombDowned();
            }
        }
    }
}