using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");

        if (jugador != null)
        {
            jugador.transform.position = new Vector3(-13.82f, -0.37f, 15f);
        }
    }

    public void CheckScore(int score)
    {
        if (score >= 30 && !escenaCargada)
        {
            escenaCargada = true;
            StartCoroutine(LoadSceneStartSpawner());
        }
        else if (score >= 30 && !siguienteNivel)
        {
            siguienteNivel = true;
            TeletransportarJugador();
        }
        else if (score >= 30 && siguienteNivel && escenaCargada && !juegoTerminado)
        {
            juegoTerminado = true;
            StartCoroutine(FinalDelJuego());
        }
    }
    IEnumerator LoadSceneStartSpawner()
    {
        SceneManager.LoadScene("FinalBattle");
        yield return new WaitForSeconds(0.5f);
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