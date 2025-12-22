using UnityEngine;

public class BulletController : LuxonProjectileController
{
    private int _damage;
    public override int damagePoint
    {
        get => _damage;
        set => _damage = value;
    }
    private float _speed;
    public override float speed
    {
        get => _speed;
        set => _speed = value;
    }
    public override int missedShotPenalty => 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        speed = 30.0f;
        if (playerController != null)
        {
            damagePoint = playerController.Damage;
        }
    }

    protected override void Update()
    {
        base.Update();
        Move();
    }

    void Move()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }


}
