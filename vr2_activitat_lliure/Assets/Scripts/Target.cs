using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class Target : MonoBehaviour
{
    public int points = 1;
    //public Score score;
    //public BulletImpact bulletImpact;
    public GameObject bulletHole;
    public void BulletHit(Vector3 hitPoint, Vector3 hitNormal)
    {
        //score.AddScore(points);
        //bulletImpact.ShowImpact(hitPoint, hitNormal);
        GameObject impacto = Instantiate(bulletHole, hitPoint, Quaternion.LookRotation(hitNormal));

    }

}
