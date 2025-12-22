using UnityEngine;

public class BulletController : ProjectileController
{
    public override int missedShotPenalty => 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        speed = 30.0f;
        damagePoint = playerController.Damage;
    }

    public override void Update()
    {
        base.Update();
        Move();
    }

    public void Move()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }


}
