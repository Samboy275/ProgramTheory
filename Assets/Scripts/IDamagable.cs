using UnityEngine;

public abstract class IDamagable : MonoBehaviour
{
    [SerializeField] protected int hp;




    public virtual void TakeDamage(int amount)
    {
        hp -= amount;
    }
}
