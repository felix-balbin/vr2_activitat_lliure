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


    }
}
