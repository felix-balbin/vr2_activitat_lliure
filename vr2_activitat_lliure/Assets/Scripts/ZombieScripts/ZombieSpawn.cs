using System.Collections;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    public GameObject prefabZombie;
    public Transform[] spawners;
    public int maxZombies = 10;
    public float spawnTime = 3f;
    public Transform Target;
    private int zombiesAlive = 0;
    private bool spawnActive = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawningLoop());
        
    }
    IEnumerator SpawningLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);

            if (zombiesAlive < maxZombies)
            {
                SpawnZombie();
            }
        }
    }

    void SpawnZombie()
    {
        if (spawners.Length == 0 || prefabZombie == null) return;

        int randomIndex = Random.Range(0, spawners.Length);
        Transform spawnPoint = spawners[randomIndex];

        float distance = Vector3.Distance(spawnPoint.position, Target.position);

        if (distance < 3f)
        {
            return;
        }

        GameObject zombie = Instantiate(prefabZombie, spawnPoint.position, spawnPoint.rotation);

        ZombieAIContext ctx = zombie.GetComponent<ZombieAIContext>();
        if (ctx != null)
        {
            ctx.Target = Target;
        }

        zombiesAlive++;

        ZombieStateManager zombieStateManager = zombie.GetComponent<ZombieStateManager>();
        if (zombieStateManager != null)
        {
            StartCoroutine(TrackZombieDeath(zombie));
        }
    }

    IEnumerator TrackZombieDeath(GameObject zombie)
    {
        ZombieStateManager zombieStateManager = zombie.GetComponent<ZombieStateManager>();

        while (zombie != null)
        {
            yield return null;
        }

        //yield return new WaitForSeconds(3f);

        zombiesAlive--;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
