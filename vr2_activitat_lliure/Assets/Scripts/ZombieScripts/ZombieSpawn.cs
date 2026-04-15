using System.Collections;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    public GameObject prefabZombie;
    private GameManager gameManager;
    private Transform[] useSpawners;
    public Transform[] spawnersCafeteria;
    public Transform[] spawnersAula;
    public int maxZombies = 10;
    public float spawnTime = 3f;
    public Transform Target;
    private int zombiesAlive = 0;
    private bool spawnActive = true;

    public Score score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
    //    gameManager = GameManager.instancia;
    //    UpdateSpawners();
    //    StartCoroutine(SpawningLoop());
    //}

    public void StartScript(GameManager gm)
    {
        gameManager = gm;
        UpdateSpawners();
        StartCoroutine(SpawningLoop());
    }
    IEnumerator SpawningLoop()
    {
        while (spawnActive)
        {
            yield return new WaitForSeconds(spawnTime);

            if (zombiesAlive < maxZombies)
            {
                SpawnZombie();
            }
        }
    }
    void UpdateSpawners()
    {
        if (gameManager == null) return;

        if (gameManager.escenaCargada == true)
        {
            useSpawners = spawnersAula;
        }
        else if (gameManager.siguienteNivel == true)
        {
            useSpawners = spawnersCafeteria;
        }

    }
    void SpawnZombie()
    {
        //detectar mapa
        //tener aqui o crear arriba un Transform[] useSpawners
        //si el mapa es cafeteria, asignarle los spawners de cafeteria
        //si es de aula asignar a useSpawners spawnersAula
        //reeplazar todos los spawners de este codigo con useSpawners

        if (useSpawners.Length == 0 || prefabZombie == null) return;

        //int randomIndex = Random.Range(0, spawners.Length);
        Transform spawnPoint = null;

        for (int i = 0; i < useSpawners.Length; i++)
        {
            int randomIndex = Random.Range(0, useSpawners.Length);
            Transform posibleSpawn = useSpawners[randomIndex];

            float distance = Vector3.Distance(posibleSpawn.position, Target.position);

            if (distance < 3f)
            {
                continue;
            }

            if (!IsSpawnerFree(posibleSpawn))
            {
                continue;
            }

            spawnPoint = posibleSpawn;
            break;
        }

        if (spawnPoint == null)
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

    bool IsSpawnerFree(Transform spawnPoint)
    {
        float zombieRadius = 1f;

        Collider[] hitsSpawn = Physics.OverlapSphere(spawnPoint.position, zombieRadius);
         
        for (int i = 0; i < hitsSpawn.Length; i++){
            if (hitsSpawn[i].GetComponent<ZombieStateManager>() != null){
                return false;
            }
        }

        return true;
    }

    IEnumerator TrackZombieDeath(GameObject zombie)
    {
        ZombieStateManager zombieStateManager = zombie.GetComponent<ZombieStateManager>();

        while (zombie != null)
        {
            yield return null;
        }

        //yield return new WaitForSeconds(2f);

        zombiesAlive--;
        score.AddScore(1);
        Debug.Log(score);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpawners();
    }
}
