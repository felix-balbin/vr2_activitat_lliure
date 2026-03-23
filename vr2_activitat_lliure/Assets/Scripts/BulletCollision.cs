using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];

        Target target = collision.gameObject.GetComponent<Target>();
        if(target != null)
        {
            target.BulletHit(contactPoint.point, contactPoint.normal);
            Destroy(gameObject);
        }

        ZombieStateManager zombie = collision.gameObject.GetComponent<ZombieStateManager>();
        if(zombie != null)
        {
            zombie.TakeHit(1, contactPoint.point, contactPoint.normal);
            Destroy(gameObject);
        }

    }
}
