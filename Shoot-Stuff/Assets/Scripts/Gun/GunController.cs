using UnityEngine;

public class GunController : MonoBehaviour
{
	public float damage = 10f;
	public float range = 100f;

	public Camera fpsCam;
	public ParticleSystem muzzleFlash;

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
		}
	}
}
