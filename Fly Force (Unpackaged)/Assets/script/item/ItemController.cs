using UnityEngine;
using Player;

public class ItemController : MonoBehaviour
{
    protected GameObject player;
    protected PlayerController playerController;
    protected float speed = 10.0f;
    protected int score = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController = collision.GetComponent<PlayerController>();
            if (playerController != null && playerController.isDead == false)
            {
                ItemGain();
                Destroy(gameObject);
            }
        }
        if (collision.CompareTag("BlockCollider"))
        {
            Destroy(gameObject);
        }
    }
    protected virtual void ItemGain()
    {
        if (playerController == null)
        {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
    }
}