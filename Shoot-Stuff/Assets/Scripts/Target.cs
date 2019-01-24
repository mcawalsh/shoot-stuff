using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
	public float health = 50f;

	void Start()
    {
        // health = asset.maxHealth;
    }

    void Update()
    {
        
    }

	public void TakeDamage(float damage)
	{
		health -= damage;
		CheckIfDead();
	}

	private void CheckIfDead()
	{
		if (health <= 0)
		{
			Destroy(gameObject);
		}
	}
}
