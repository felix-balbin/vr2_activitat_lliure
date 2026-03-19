using System.Collections;
using UnityEngine;

public class Arms : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] float bulletForce = 50f;

    [Header("Fire Modes")]
    [SerializeField] float autoFireRate = 0.1f;
    [SerializeField] int burstCount = 3;
    [SerializeField] float burstFireRate = 0.08f;

    [Header("Effects")]
    [SerializeField] ParticleSystem shootParticles;
    public AudioClip shootSound;
    private AudioSource audioSource;

    private Coroutine fireCoroutine;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
            rb.linearVelocity = bulletSpawn.forward * bulletForce;

        if (shootParticles != null)
            shootParticles.Play();

        if (shootSound != null)
            audioSource.PlayOneShot(shootSound);
        Destroy(bullet, 5f);
    }

    public void StartAutoFire()
    {
        if (fireCoroutine == null)
        {
            fireCoroutine = StartCoroutine(AutoFire());
        }
    }

    public void StopAutoFire()
    {
        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }

    IEnumerator AutoFire()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(autoFireRate);
        }
    }

    public void BurstFire()
    {
        if (fireCoroutine == null){
            fireCoroutine = StartCoroutine(BurstRoutine());
        }
    }

    IEnumerator BurstRoutine()
    {
        for (int i = 0; i < burstCount; i++)
        {
            Shoot();
            yield return new WaitForSeconds(burstFireRate);
        }

        fireCoroutine = null;
    }
}
