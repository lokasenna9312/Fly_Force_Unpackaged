using UnityEngine;

namespace Player
{
    public abstract class BulletBombController : TardionProjectileController
    {
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

        protected virtual void OnDestroy()
        {
            if (playerController != null)
            {
                Debug.Log("Bomb cleared!");
                playerController.BulletBombDowned();
            }
        }
    }
}