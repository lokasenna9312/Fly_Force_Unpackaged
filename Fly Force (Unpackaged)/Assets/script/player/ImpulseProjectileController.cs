using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class ImpulseProjectileController : ProjectileController
    {
        protected abstract float deltaV { get; set; }
        protected abstract Rigidbody2D momentum { get; set; }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected override void Start()
        {
            base.Start();
            if (momentum == null) momentum = GetComponent<Rigidbody2D>();
            if (momentum != null) ApplyImpulse();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }

        protected virtual void ApplyImpulse()
        {
            if (momentum != null)
            {
                momentum.AddForce(Vector3.up * deltaV, ForceMode2D.Impulse);
            }
        }
    }
}