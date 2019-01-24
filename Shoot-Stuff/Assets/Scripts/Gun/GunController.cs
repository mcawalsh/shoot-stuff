using UnityEngine;

public class GunController : MonoBehaviour
{
	public float damage = 10f;
	public float range = 100f;
	public float impactForce = 40f;

	public Camera fpsCam;
	public ParticleSystem muzzleFlash;
	public GameObject impactEffect;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
		{
			Shoot();
		}
    }

	private void Shoot()
	{
		if (muzzleFlash)
			muzzleFlash.Play();

		RaycastHit hit;
		if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
		{
			Debug.Log(hit.collider.name);

			IDamageable damageable = hit.transform.GetComponent<IDamageable>();

			if (damageable != null)
			{
				Debug.Log($"Dealing {damage} damage to {hit.collider.name}");
				damageable.TakeDamage(damage);
			}

			if (hit.rigidbody != null)
			{
				hit.rigidbody.AddForce(-hit.normal * impactForce);
			}

			GameObject explosion = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
			Destroy(explosion, 2f);
			
		}
	}
}
