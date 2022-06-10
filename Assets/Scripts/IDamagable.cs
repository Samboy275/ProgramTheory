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


    public virtual void TakeDamage(int amount)
    {
        Debug.Log(amount + " " + hp);
        hp -= amount;
        healthBar.SetHealth(hp);
        if (hp <= 0)
        {
            Debug.Log(gameObject.name + " is dead");
            isDead = true;
        }
    }
}
