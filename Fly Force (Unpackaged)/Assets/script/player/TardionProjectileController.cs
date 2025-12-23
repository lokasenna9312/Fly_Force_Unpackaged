using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class TardionProjectileController : ProjectileController
    {
        public abstract float acceleration { get; set; }
        public abstract Rigidbody2D momentum { get; set; }
        public abstract float burstTime { get; set; }
        public float time;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected override void Start()
        {
            base.Start();
            if (momentum == null) momentum = GetComponent<Rigidbody2D>();
            time = 0.0f;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }

        protected void FixedUpdate()
        {
            if (momentum != null) ApplyForce(burstTime);
            time += Time.fixedDeltaTime;
        }

        void ApplyForce(float burstTime)
        {
            if (momentum == null) return;
            if (time < burstTime)
            {
                momentum.AddForce(Vector3.up * acceleration);
            }
            else
            {
                time = burstTime;
            }
        }
    }
}