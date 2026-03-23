using UnityEngine;
using TMPro;
using System.Collections;

public class GunRecolector : MonoBehaviour
{
    public GameObject armaObjeto;
    public TextMeshProUGUI textoUI;

    public void Start()
    {
        armaObjeto.SetActive(true);
        textoUI.text = "";
    }
    public void Recoger()
    {
        armaObjeto.SetActive(false);
        GameManager.instancia.SumarArma();
        StartCoroutine(MostrarMensaje());
    }

    IEnumerator MostrarMensaje()
    {
        textoUI.text = "Has obtenido " + gameObject.name;
        yield return new WaitForSeconds(3f);
        textoUI.text = "";
    }
}