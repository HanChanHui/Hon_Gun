using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField]
    protected float moveVelocity;
    [SerializeField]
    protected Vector2 startPos;
    [SerializeField]
    private LayerMask targetLayer;
    

    [SerializeField]
    private int damage;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    protected Rigidbody rigid;

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {
        var distance = Vector2.Distance(startPos, transform.position);
    }

    protected virtual void FixedUpdate()
    {
        rigid.velocity = transform.forward * moveVelocity;
    }

    /// <summary>
    /// 해당 gameObject를 오브젝트 풀에 반환.
    /// </summary>
    /// <param name="delay"></param>
    protected abstract void ReturnObject(float delay = 0.0f);

    public void SetupProjectile(LayerMask _layerMask, float _velocity, float _distance)
    {
        startPos = transform.position;

        moveVelocity = _velocity;
        targetLayer = _layerMask;
    }

    // <summary>
    /// 투사체의 Trigger에 다른 collider가 닿은 경우에 처리.
    /// </summary>
    protected abstract void OnEnterProcess(Collision other);

    //protected void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.layer == Util.MaskToLayer(targetLayer))
    //    {
    //        OnEnterProcess(other);
    //    }
    //}

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Wall") ||
            other.gameObject.layer == LayerMask.NameToLayer("Player") ||
            other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            OnEnterProcess(other);
        }
    }

}
