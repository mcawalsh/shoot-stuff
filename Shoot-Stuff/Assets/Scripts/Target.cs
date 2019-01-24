using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
	public float health = 50f;
	public GameObject destroyedEffect;

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
			var pos = transform.position;
			var direction = transform.up;

			Destroy(gameObject);

			GameObject displayedDestroyEffect = Instantiate(destroyedEffect, pos, Quaternion.LookRotation(direction));
			Destroy(displayedDestroyEffect, 2f);
		}
	}
}
