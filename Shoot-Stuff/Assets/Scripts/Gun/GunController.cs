using TMPro;
using UnityEngine;

public class GunController : MonoBehaviour
{
	private const string FIRE_BUTTON = "Fire1";

	public float damage = 10f;
	public float range = 100f;
	public float impactForce = 40f;
	public bool fullAuto = true;
	public float fireRate = 15;
	public TextMeshProUGUI textMeshPro;

	private float nextTimeToFire = 0f;

	public Camera fpsCam;
	public ParticleSystem muzzleFlash;
	public GameObject impactEffect;
	private bool canFire = true;

	void Start()
	{
		SetFireRateText();

		SubscribeToEvents();
	}

	private void SubscribeToEvents()
	{
		FindObjectOfType<DialogueManager>().Chatting += PlayerCameraController_Chatting;
		FindObjectOfType<BuildManager>().OnBuilding += OnBuild;
	}

	private void OnBuild(bool isBuilding)
	{
		canFire = !isBuilding;
	}

	private void PlayerCameraController_Chatting(bool isChatting)
	{
		canFire = !isChatting;
	}

    void Update()
    {
		if (canFire)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				fullAuto = !fullAuto;
				SetFireRateText();
			}

			bool shouldFire = Input.GetButtonDown(FIRE_BUTTON) || (fullAuto && Input.GetButton(FIRE_BUTTON));

			if (shouldFire && Time.time >= nextTimeToFire)
			{
				nextTimeToFire = Time.time + 1f / fireRate;

				Shoot();
			}
		}
    }

	private void SetFireRateText()
	{
		textMeshPro.text = fullAuto ? "Auto" : "Single";
	}

	private void Shoot()
	{
		if (muzzleFlash)
			muzzleFlash.Play();

		RaycastHit hit;
		if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
		{
			DialogueTrigger trigger = hit.transform.GetComponent<DialogueTrigger>();

			if (trigger != null)
			{
				trigger.TriggerDialogue();
			}

			GameObject explosion = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
			Destroy(explosion, 2f);
			
		}
	}
}
