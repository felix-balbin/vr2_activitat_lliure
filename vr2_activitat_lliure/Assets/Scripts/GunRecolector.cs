using UnityEngine;
using TMPro;

public class GunRecolector : MonoBehaviour
{
    public GameObject armaObjeto;
    public TextMeshProUGUI textoUI;

    private float tiempoMensaje = 0f;
    private bool mostrandoMensaje = false;

    public void Start()
    {
        armaObjeto.SetActive(true);
        textoUI.text = "";
    }

    public void Recoger()
    {
        armaObjeto.SetActive(false);
        GameManager.instancia.SumarArma();
        textoUI.text = "Has obtenido " + gameObject.name;
    }
}