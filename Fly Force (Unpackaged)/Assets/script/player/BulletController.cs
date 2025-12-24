using UnityEngine;

namespace Player
{
    public abstract class BulletController : ImpulseProjectileController
    {
        private Rigidbody2D _momentum;
        protected override Rigidbody2D momentum
        {
            get => _momentum;
            set => _momentum = value;
        }
        [SerializeField] private float _deltaV;
        protected override float deltaV
        {
            get => _deltaV;
            set => _deltaV = value;
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
    }
}