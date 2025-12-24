using UnityEngine;
using Enemy;

namespace Player
{
    public abstract class ProjectileController : MonoBehaviour
    {
        protected GameObject player;
        protected PlayerController playerController;
        [SerializeField] private int _damage;
        protected int damagePoint
        {
            get => _damage;
            set => _damage = value;
        }
        [SerializeField] private int _missedShotPenalty;
        protected int missedShotPenalty
        {
            get => _missedShotPenalty;
            set => _missedShotPenalty = value;
        }
        private int hitCount = 0;
        private bool overrun = false;

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            DestroyOverrunProjectile();
        }

        public void Initializer(PlayerController owner)
        {
            playerController = owner;
            player = owner.gameObject;
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