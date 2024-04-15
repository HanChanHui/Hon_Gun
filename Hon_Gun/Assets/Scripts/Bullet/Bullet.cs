using UnityEngine;

public class Bullet : Projectile
{
  protected override void ReturnObject(float delay = 0.0f)
    {
        Destroy(gameObject);
    }

    protected override void OnEnterProcess(Collision other)
    {
        ReturnObject();
    }
}
