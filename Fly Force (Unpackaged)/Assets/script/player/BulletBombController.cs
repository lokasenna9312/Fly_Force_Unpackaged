using UnityEngine;

namespace Player
{
    public abstract class BulletBombController : TardionProjectileController
    {
        protected override float acceleration { get; set; }
        protected override Rigidbody2D momentum { get; set; }
        protected override float burstTime { get; set; }
        protected override int damagePoint { get; set; }

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
    }
}