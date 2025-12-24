using UnityEngine;

namespace Player.Ammunition
{
    public class Bullet2 : BulletController
    {
        // Bullet properties are set in the Inspector.
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
        [SerializeField] private int _damage;
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
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    }
}