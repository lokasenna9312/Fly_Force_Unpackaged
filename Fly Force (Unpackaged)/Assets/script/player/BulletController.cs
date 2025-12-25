using UnityEngine;

namespace Player
{
    public abstract class BulletController : ImpulseProjectileController
    {
        private ParticleSystem blastParticles;
        [SerializeField] private GameObject blast;
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

        protected void OnDestroy()
        {
            if (blast != null)
            {
                blastParticles = Instantiate(blast, transform.position, transform.rotation).GetComponent<ParticleSystem>();
                if (blastParticles != null)
                {
                    var main = blastParticles.main;
                    main.startSpeed = deltaV;
                    blastParticles.Play();
                }
            }
        }
    }
}