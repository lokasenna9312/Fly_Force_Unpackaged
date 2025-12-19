using Unity.VisualScripting;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    private PlayerController playerController;
    private EnemyController collidedEnemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

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
            collidedEnemy = collision.gameObject.GetComponent<EnemyController>();
            if (collidedEnemy != null && playerController != null)
            {
                Debug.Log("Shield collided with " + collision.gameObject.name);
                collidedEnemy.TakeDamage(playerController.ShieldDamage, "Shield");
            }
        }
        if (collision.CompareTag("Boss"))
        {
            Debug.Log("Shield was broken by the Boss: " + collision.gameObject.name);
            if (playerController != null)
            {
                playerController.RemoveShield();
            }
            Destroy(gameObject);
        }
    }
}
