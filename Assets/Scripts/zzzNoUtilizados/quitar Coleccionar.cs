using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coleccionar : MonoBehaviour
{
    // Define variables del tipo lista (colleccionables), GameObjetc (bolsa), bool
    [SerializeField] private List<GameObject> colleccionables;
    [SerializeField] private GameObject bolsa;

    private bool presionado = false;

    // Instancia la lista colleccionables
    private void Awake()
    {
        colleccionables = new List<GameObject>();
    }

    // Controla la colisión del objeto coleccionable
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Coleccionable")) 
        { 
            return; 
        }
                
        GameObject nuevoColeccionable = collision.gameObject;
        nuevoColeccionable.SetActive(false);

        colleccionables.Add(nuevoColeccionable);
        nuevoColeccionable.transform.SetParent(bolsa.transform);
    }

    // Recarga coleccionables cuando se hace clic en F1
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (colleccionables.Count == 0) return;

            presionado = !presionado;
            colleccionables[0].SetActive(presionado);
        }
    }
}
