using UnityEngine;
using TMPro;

public class LimpiaTexto : MonoBehaviour
{
    public TextMeshProUGUI texto;

    private float temporizador = 0f;
    private bool contando = false;

    void Update()
    {
        if (!string.IsNullOrEmpty(texto.text) && !contando)
        {
            temporizador = 2f;
            contando = true;
        }
        if (contando)
        {
            temporizador -= Time.deltaTime;
            if (temporizador <= 0f)
            {
                texto.text = "";
                contando = false;
            }
        }
    }
}
