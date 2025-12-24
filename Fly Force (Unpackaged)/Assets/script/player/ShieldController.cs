using Enemy;
using UnityEngine;

namespace Player
{
    public class ShieldController : MonoBehaviour
    {
        public int damage { get; private set; }
        public float duration { get; private set; }
        private GameObject player;
        private PlayerController playerController;
        private MinionController collidedEnemy;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {   

        }

        // Update is called once per frame
        void Update()
        {
            if (player != null)
                transform.position = player.transform.position;
            else
            {
                Destroy(gameObject);
                return;
            }
            duration -= Time.deltaTime;
            if (duration <= 0.0f)   
            {
                DestroyShield();
            }
        }

        public void Initializer(PlayerController owner)
        {
            playerController = owner;
            player = owner.gameObject;
        }

        public void DamageForceSetter(int amount)
        {
            damage = amount;
        }

        public void DurationForceSetter(float amount)
        {
            duration = amount;
        }

        public void DestroyShield()
        {
            if (playerController != null)
            {
                playerController.SetShieldActiveState(false);
            }
            Destroy(gameObject);
            Debug.Log("Shield Off!");
        }

        public void RemoveShield()
        {
            if (playerController != null)
            {
                playerController.SetShieldActiveState(false);
            }
            Destroy(gameObject);
            SoundManager.instance.ShieldOffSound.Play();
            Debug.Log("Shield was nullified!");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("enemyBullet"))
            {
                Debug.Log("Shield blocked bullet: " + collision.gameObject.name);
                Destroy(collision.gameObject);
            }
            if (collision.CompareTag("enemy") || collision.CompareTag("ItemDropper"))
            {
                collidedEnemy = collision.gameObject.GetComponent<MinionController>();
                if (collidedEnemy != null)
                {
                    Debug.Log("Shield collided with " + collision.gameObject.name);
                    collidedEnemy.TakeDamage(damage, "Shield");
                }
            }
            if (collision.CompareTag("Boss"))
            {
                Debug.Log("Shield was broken by the Boss: " + collision.gameObject.name);
                RemoveShield();
            }
        }
    }
}