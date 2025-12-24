using UnityEngine;

namespace Player
{
    public abstract class BulletController : ImpulseProjectileController
    {
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
    }
}