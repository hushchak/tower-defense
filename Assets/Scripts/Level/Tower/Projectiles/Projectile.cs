using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected ProjectileData Data;
    protected Enemy Target;

    public virtual void Setup(ProjectileData data, Enemy target)
    {
        Data = data;
        Target = target;
    }
}
