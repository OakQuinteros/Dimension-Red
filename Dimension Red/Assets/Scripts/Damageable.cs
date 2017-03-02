using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour {

    public float health;
    float maxHealth = 100;

    float defense = 0;

    void Start()
    {
        health = 100;
    }

    void Update()
    {
        IsDead();
    }

    public float GetHealth()
    {
        return health;
    }

    public void Damage(float damage)
    {
        if ((damage -= defense) <= 0)
            damage = 1;
   
        if ((health - damage) < 0)
            health = 0;
        else
            health -= damage;
    }

    public void Heal(float heal)
    {
        if ((health + heal) > maxHealth)
            health = maxHealth;
        else
            health += heal;
    }

    public void SetHealth(float currHealth)
    {
        health = currHealth;
    }

    void IsDead()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
