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
        protected abstract int missedShotPenalty { get; }
        private bool overrun;

        protected virtual void Awake()
        {

        }

        protected virtual void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                playerController = player.GetComponent<PlayerController>();
            overrun = false;
        }

        protected virtual void Update()
        {
            DestroyOverrunProjectile();
        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("enemy") || collision.CompareTag("ItemDropper"))
            {
                hitCount++;
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
                hitCount++;
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
                if (hitCount == 0 && overrun == false)
                {
                    UIManager.instance.AddScore(-missedShotPenalty);
                    overrun = true;
                }
                if (transform.position.y > 40.0f) Destroy(gameObject);
            }
        }
    }
}