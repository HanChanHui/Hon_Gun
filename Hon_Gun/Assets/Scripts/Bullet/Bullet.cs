using UnityEngine;

public class Bullet : Projectile
{
  protected override void ReturnObject(float delay = 0.0f)
    {
        Destroy(gameObject);
    }

    protected override void OnEnterProcess(Collider2D other)
    {
        if(other.TryGetComponent(out Enemy enemy))
        {
            ReturnObject();
        }

        if(other.TryGetComponent(out Player player))
        {
            ReturnObject();
        }
    }
}
