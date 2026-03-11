using UnityEngine; 

public class ShootBullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 40f;
    public float bulletLifetime = 5f;

    public AudioClip clip;
    public AudioSource source;
    public ParticleSystem particles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = GetComponent<AudioSource>();
        particles = GetComponent<ParticleSystem>();
        
    }

    public void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rbBullet = bullet.GetComponent<Rigidbody>();

        //source.PlayOneShot(clip);
        //particles.Play();

        if (rbBullet != null)
        {
            rbBullet.linearVelocity = firePoint.forward * bulletSpeed;
        }

        // Sonido
        if (clip != null && source != null)
        {
            source.PlayOneShot(clip);
        }

        // Partículas
        if (particles != null)
        {
            particles.Play();
        }

        Destroy(bullet, bulletLifetime);
    }
}
