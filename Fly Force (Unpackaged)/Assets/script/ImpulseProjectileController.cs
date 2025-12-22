using UnityEngine;

public abstract class ImpulseProjectileController : ProjectileController
{
    public abstract float acceleration { get; set; }
    public abstract Rigidbody2D momentum { get; set; }
    public abstract float burstTime { get; set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
