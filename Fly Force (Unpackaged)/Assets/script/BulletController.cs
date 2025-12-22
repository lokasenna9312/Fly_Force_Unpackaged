using UnityEngine;

public class BulletController : ImpulseProjectileController
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
    public override int missedShotPenalty => 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        momentum = GetComponent<Rigidbody2D>();
        acceleration = 30.0f;
        if (playerController != null)
            damagePoint = playerController.Damage;
        if (momentum != null)
            momentum.AddForce(Vector2.up * acceleration, ForceMode2D.Impulse);
    }

    protected override void Update()
    {
        base.Update();
    }


}
