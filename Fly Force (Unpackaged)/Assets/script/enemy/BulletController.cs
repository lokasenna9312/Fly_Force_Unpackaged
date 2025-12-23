using UnityEngine;

namespace Enemy
{
    public class BulletController : ImpulseProjectileController
    {
        private Rigidbody2D _momentum;
        public override Rigidbody2D momentum
        {
            get => _momentum;
            set => _momentum = value;
        }
        public float _deltaV;
        public override float deltaV
        {
            get => _deltaV;
            set => _deltaV = value;
        }
        private float rotateSpeed;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected override void Start()
        {
            deltaV = 10.0f;
            rotateSpeed = 300.0f;
            base.Start();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            RotateBullet();
        }

        private void RotateBullet()
        {
            transform.rotation = Quaternion.Euler(0, 0, time * rotateSpeed);
        }
    }
}