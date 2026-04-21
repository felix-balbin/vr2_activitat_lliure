using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;
    private int armasRecogidas = 0;
    public bool escenaCargada = false; //si es true, ha entrado a aula
    public bool siguienteNivel = false; //si es true, ha entrado a cafeteria
    public GameObject pantallaFinal;
    private bool juegoTerminado = false;
    private bool spawnActivated = false;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (pantallaFinal != null)
        {
            pantallaFinal.SetActive(false);
        }
    }

    public void SumarArma()
    {
        armasRecogidas++;
        if (armasRecogidas == 2)
        {
            TeletransportarJugador();
        }
        if (armasRecogidas == 4)
        {
            SceneManager.LoadScene("DefScene");
        }
    }

    void TeletransportarJugador()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        Vector3 Ubi = camera.transform.position;
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        GameObject jugador2 = GameObject.FindGameObjectWithTag("Player2");
        if (jugador != null)
        {
            jugador.transform.position = (Ubi - new Vector3(0f, 1f, 0f));
        }
        if (jugador2 != null)
        {
            jugador2.transform.position = new Vector3(1000f, 1000f, 1000f);
        }
    }

    public void CheckScore(int score)
    {
        if (score >= 30 && !escenaCargada)
        {
            escenaCargada = true;
            SceneManager.LoadScene("FinalBattle");
            StartCoroutine(LoadSceneStartSpawner());
        }
        else if (score >= 30 && !siguienteNivel)
        {
            siguienteNivel = true;
            spawnActivated = false;
            ClearZombies();

            TeletransportarJugador();

            ZombieSpawn spawner = FindAnyObjectByType<ZombieSpawn>();

            if (spawner != null)
            {
                spawner.ResetZombieSpawn();
                StartCoroutine(LoadSceneStartSpawner());

            }

        }
        else if (score >= 30 && siguienteNivel && escenaCargada && !juegoTerminado)
        {
            juegoTerminado = true;
            StartCoroutine(FinalDelJuego());
        }
    }
    IEnumerator LoadSceneStartSpawner()
    {
        yield return new WaitForSeconds(10f);
        ActivateSpawner();
    }
    void ActivateSpawner()
    {
        if (spawnActivated) return;
        spawnActivated = true;
        ZombieSpawn spawner = FindAnyObjectByType<ZombieSpawn>();

        if (spawner != null)
        {
            spawner.StartScript(this);
        }
    }

    public void ClearZombies()
    {
        ZombieStateManager[] zombies = FindObjectsByType<ZombieStateManager>(FindObjectsSortMode.None);

        foreach (var zombie in zombies)
        {
            zombie.DespawnZombie();
        }
    }

    IEnumerator FinalDelJuego()
    {
        if (pantallaFinal != null)
        {
            pantallaFinal.SetActive(true);
        }
        yield return new WaitForSeconds(3f);
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}