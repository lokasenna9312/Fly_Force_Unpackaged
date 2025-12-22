using UnityEngine;

public class BulletBombController : ProjectileController
{
    private Rigidbody2D momentum;
    public override int missedShotPenalty => 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        acceleration = 20000.0f;
        damagePoint = playerController.BombDamage;
        momentum = GetComponent<Rigidbody2D>();
    }

    public override void Update()
    {
        base.Update();
    }

    public void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        if (time < 2.0f)
        {
            momentum.AddForce(Vector3.up * acceleration);
        }
        else
        {
            time = 2.0f;
            momentum.AddForce(new Vector3(0,0,0));
        }
    }
}