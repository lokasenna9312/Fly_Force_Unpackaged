using UnityEngine;
using UnityEngine.Android;

namespace Player.Ammunition
{
    public class BulletBomb1 : BulletBombController
    {
        // Bullet properties are set in the Inspector.
        private ParticleSystem trailParticles;
        private ParticleSystem.EmissionModule emission;
        private ParticleSystem blastParticles;
        [SerializeField] private GameObject trail;
        [SerializeField] private GameObject blast;
        [SerializeField] private Transform nozzle;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected override void Start()
        {
            base.Start();
            if (trail != null && nozzle != null)
            {
                trailParticles = Instantiate(trail, nozzle.position, nozzle.rotation).GetComponent<ParticleSystem>();
                if (trailParticles != null)
                {
                    emission = trailParticles.emission;
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
                emission.enabled = (time < burstTime);
            }
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (trailParticles != null)
            {
                trailParticles.Stop();
            }
            if (blast != null)
            {
                blastParticles = Instantiate(blast, transform.position, transform.rotation).GetComponent<ParticleSystem>();
                if (blastParticles != null)
                {
                    var main = blastParticles.main;
                    main.startSpeed = finalVelocity.magnitude;
                    if (blastParticles.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
                    {
                        rb.linearVelocity = finalVelocity;
                    }
                }
            }   
        }
    }
}