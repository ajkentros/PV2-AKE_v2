using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    public int health;
    public float speed;

    protected abstract void Move();

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
