using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    private int armasRecogidas = 0;
    private bool escenaCargada = false;
    private bool siguienteNivel = false;

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
            jugador.transform.position = new Vector3(5.6f, -0.248f, -7.718f);
        }
    }

    public void CheckScore(int score)
    {
        if (score >= 30 && !escenaCargada)
        {
            escenaCargada = true;
            SceneManager.LoadScene("FinalBattle");
        }
        else if (score >= 30 && !siguienteNivel)
        {
            siguienteNivel = true;
            TeletransportarJugador();
        }
    }
}