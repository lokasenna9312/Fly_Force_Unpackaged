using UnityEngine;
using Enemy;

namespace Player
{
    public abstract class ProjectileController : MonoBehaviour
    {
        protected abstract int damagePoint { get; set; }
        protected GameObject player;
        protected PlayerController playerController;
        private int hitCount = 0;
        private bool isMissed = false;
        protected abstract int missedShotPenalty { get; }

        protected virtual void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                playerController = player.GetComponent<PlayerController>();
        }

        protected virtual void Update()
        {
            DestroyOverrunProjectile();
        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player"))
                hitCount++;
            if (collision.CompareTag("enemy") || collision.CompareTag("ItemDropper"))
            {
                UIManager.instance.AddScore(10);
                MinionController collidedEnemy = collision.GetComponent<MinionController>();
                if (collidedEnemy != null)
                {
                    int inflictedDamage = collidedEnemy.HandleDamage(damagePoint, gameObject.tag);
                    damagePoint -= inflictedDamage;
                    if (damagePoint <= 0)
                    {
                        DestroyProjectile();
                    }
                }
            }
            if (collision.CompareTag("Boss"))
            {
                UIManager.instance.AddScore(10);
                BossController collidedBoss = collision.GetComponent<BossController>();
                if (collidedBoss != null)
                {
                    int inflictedDamage = collidedBoss.HandleDamage(damagePoint, gameObject.tag);
                    damagePoint -= inflictedDamage;
                    if (damagePoint <= 0)
                    {
                        DestroyProjectile();
                    }
                }
            }
            void DestroyProjectile()
            {
                Destroy(gameObject);
            }
        }

        protected void DestroyOverrunProjectile()
        {
            if (transform.position.y > 20.0f)
            {
                if (hitCount == 0 && isMissed == false)
                {
                    UIManager.instance.AddScore(-missedShotPenalty);
                    isMissed = true;
                }
            }
            if (transform.position.y > 40.0f)
                Destroy(gameObject);
        }

        protected void OnDestroy()
        {
            if (gameObject.CompareTag("BulletBomb") && playerController != null)
            {
                Debug.Log("Bomb cleared!");
                playerController.BulletBombDowned();
            }
        }
    }
}