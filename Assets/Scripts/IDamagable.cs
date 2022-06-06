using UnityEngine;

public abstract class IDamagable : MonoBehaviour
{
    [SerializeField] protected int hp;
    [SerializeField] private HealthBar healthBar;
    protected bool isDead;

    protected virtual void Start()
    {
        healthBar.SetMaxHealth(hp);
        isDead = false;
    } 


    public virtual void TakeDamage(int amount = 1)
    {
        hp -= amount;
        healthBar.SetHealth(hp);
        if (hp <= 0)
        {
            isDead = true;
        }
    }
}
