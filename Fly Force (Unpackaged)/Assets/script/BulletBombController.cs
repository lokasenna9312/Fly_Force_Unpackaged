using UnityEngine;

public class BulletBombController : TardionProjectileController
{
    private Rigidbody2D _momentum;
    public override Rigidbody2D momentum
    {
        get => _momentum;
        set => _momentum = value;
    }
    public float _acceleration;
    public override float acceleration
    {
        get => _acceleration;
        set => _acceleration = value;
    }
    private float _burstTime;
    public override float burstTime
    {
        get => _burstTime;
        set => _burstTime = value;
    }
    private int _damage;
    public override int damagePoint
    {
        get => _damage;
        set => _damage = value;
    }
    public override int missedShotPenalty => 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        momentum = GetComponent<Rigidbody2D>();
        acceleration = 20000.0f;
        burstTime = 2.0f;
        damagePoint = playerController.BombDamage;
    }

    protected override void Update()
    {
        base.Update();
    }
}