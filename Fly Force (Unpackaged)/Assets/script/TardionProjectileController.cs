using UnityEngine;

public abstract class TardionProjectileController : ProjectileController
{
    public abstract float acceleration { get; set; }
    public abstract Rigidbody2D momentum { get; set; }
    public abstract float burstTime { get; set; }
    public float time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        time = 0.0f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        time += Time.deltaTime;
    }

    protected void FixedUpdate()
    {
        Move(burstTime);
    }

    void Move(float burstTime)
    {
        if (time < burstTime)
        {
            momentum.AddForce(Vector3.up * acceleration);
        }
        else
        {
            time = burstTime;
            momentum.AddForce(new Vector3(0, 0, 0));
        }
    }
}
