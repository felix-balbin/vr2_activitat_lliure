using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    public GameObject impact;

    public void ShowImpact(Vector3 position, Vector3 normal)
    {
        GameObject impacto = Instantiate(impact, position, Quaternion.LookRotation(normal));
    }

}
