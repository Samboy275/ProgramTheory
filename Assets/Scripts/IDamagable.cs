using UnityEngine;

public abstract class IDamagable : MonoBehaviour
{
    [SerializeField] protected int hp;
    [SerializeField] private int maxHp;
    [SerializeField] private HealthBar healthBar;
    protected bool isDead;

    protected virtual void Start()
    {
        hp = maxHp;
        healthBar.SetMaxHealth(maxHp);
        isDead = false;
    } 

    public bool Dead()
    {
        return isDead;
    }
    public virtual void TakeDamage(int amount)
    {
        hp -= amount;
        healthBar.SetHealth(hp);
        if (hp <= 0)
        {
            isDead = true;
        }
    }

    public void IncreaseHP(int amount)
    {
        hp = (hp + amount >= maxHp)? maxHp : hp + amount;
        healthBar.SetHealth(hp);
    }

    public int GetMaxHp()
    {
        return maxHp;
    }
}
