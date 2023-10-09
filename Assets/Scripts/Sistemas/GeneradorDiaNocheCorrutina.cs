using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorDiaNocheCorrutina : MonoBehaviour
{
    [SerializeField] private Camera camara;
    [SerializeField] private Color nocheColor;
    [SerializeField] private float segundos;

    private Color diaColor;

    void Start()
    {
        diaColor = camara.backgroundColor;
        StartCoroutine(CambiarColor(segundos)); ;
    }

    IEnumerator CambiarColor(float tiempo)
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempo);
            camara.backgroundColor = camara.backgroundColor == diaColor ?
                                        camara.backgroundColor = nocheColor :
                                        camara.backgroundColor = diaColor;
        }
    }
}