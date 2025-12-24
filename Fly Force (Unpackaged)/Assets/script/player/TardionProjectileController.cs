using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class TardionProjectileController : ProjectileController
    {
        protected abstract float acceleration { get; set; }
        protected abstract Rigidbody2D momentum { get; set; }
        protected abstract float burstTime { get; set; }
        protected abstract float startMass { get; set; }
        protected abstract float fuelMass { get; set; }
        protected float time { get; set; }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected override void Start()
        {
            base.Start();
            if (momentum == null) momentum = GetComponent<Rigidbody2D>();
            startMass = momentum.mass;
            time = 0.0f;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }

        protected virtual void FixedUpdate()
        {
            time += Time.fixedDeltaTime;
            if (momentum != null)
            {
                ApplyForce(burstTime);
                momentum.mass = Discharged(startMass, fuelMass);
            }
            Debug.Log("Time: " + time + " Mass: " + momentum.mass);
        }

        void ApplyForce(float burstTime)
        {
            if (time < burstTime) momentum.AddForce(Vector3.up * acceleration);
        }

        float Discharged(float startMass, float fuelMass)
        {
            float burnRatio = Mathf.Clamp01(time / burstTime);
            float massOnTime = startMass - (fuelMass * burnRatio);
            return Mathf.Max(massOnTime, startMass - fuelMass);
        }
    }
}