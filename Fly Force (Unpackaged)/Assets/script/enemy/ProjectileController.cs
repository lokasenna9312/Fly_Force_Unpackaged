using UnityEngine;
using Player;

namespace Enemy
{
    public abstract class ProjectileController : MonoBehaviour
    {
        protected GameObject player;
        protected PlayerController playerController;
        protected Vector3 distance;
        protected Vector3 dir;
        protected float time;

        protected virtual void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                playerController = player.GetComponent<PlayerController>();
            distance = player.transform.position - gameObject.transform.position;
            dir = distance.normalized;
            time = 0.0f;
        }

        protected virtual void Update()
        {
            DestroyOverrunProjectile();
        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") || collision.CompareTag("BulletBomb") || collision.CompareTag("BlockCollider"))
            {
                Destroy(gameObject);
            }
        }

        protected void DestroyOverrunProjectile()
        {
            time += Time.deltaTime;
            if (time > 5.0f)
            {
                Destroy(gameObject);
            }
        }
    }
}