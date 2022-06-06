using UnityEngine;

public abstract class IDamagable : MonoBehaviour
{
    [SerializeField] protected int hp;

    protected bool isDead;

    protected virtual void Start()
    {
        isDead = false;
    } 


    public virtual void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            isDead = true;
        }
    }
}
