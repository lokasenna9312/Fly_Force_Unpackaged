using UnityEngine;
using UnityEngine.Android;

namespace Player.Ammunition
{
    public class BulletBomb1 : BulletBombController
    {
        private ParticleSystem trailParticles;
        private ParticleSystem.EmissionModule _emission;
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
            if (trail != null && nozzle != null)
            {
                trailParticles = Instantiate(trail, nozzle.position, nozzle.rotation).GetComponent<ParticleSystem>();

                if (trailParticles != null)
                {
                    var main = trailParticles.main;
                    _emission = trailParticles.emission;
                    trailParticles.Play();
                }
            }
        }

        // Update is called once per frame
        protected override void Update()    
        {
            base.Update();
            if (trailParticles != null)
            {
                trailParticles.transform.position = nozzle.position;
                trailParticles.transform.rotation = nozzle.rotation;
                _emission.enabled = (time < burstTime);
            }
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public void DetachTrail()
        {
            if (trailParticles != null)
            {
                trailParticles.transform.SetParent(null);
                var emission = trailParticles.emission;
                emission.enabled = false;
                trailParticles = null;
            }
        }

        protected override void OnDestroy()
        {
            if (trailParticles != null)
            {
                trailParticles.Stop();
            }
                base.OnDestroy();
        }
    }
}