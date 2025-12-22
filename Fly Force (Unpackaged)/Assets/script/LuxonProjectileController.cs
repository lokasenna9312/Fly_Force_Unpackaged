using UnityEngine;

public abstract class LuxonProjectileController : ProjectileController
{
    public abstract float speed { get; set; }
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
