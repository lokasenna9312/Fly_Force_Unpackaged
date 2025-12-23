using UnityEngine;

namespace Player
{
    public class BulletController : ImpulseProjectileController
    {
        private Rigidbody2D _momentum;
        public override Rigidbody2D momentum
        {
            get => _momentum;
            set => _momentum = value;
        }
        [SerializeField] private float _deltaV;
        public override float deltaV
        {
            get => _deltaV;
            set => _deltaV = value;
        }
        private int _damage;
        protected override int damagePoint
        {
            get => _damage;
            set => _damage = value;
        }
        protected override int missedShotPenalty => 10;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected override void Start()
        {
            base.Start();
            if (playerController != null)
            {
                damagePoint = playerController.Damage;
            }
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}