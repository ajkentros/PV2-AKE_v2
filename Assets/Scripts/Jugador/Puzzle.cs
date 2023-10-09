using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    // variables serializadas privadas
    [SerializeField] private GameObject bolsa;
    [SerializeField] private Transform padreObjetivos;

    // variables privadas
    private Queue<GameObject> objetivos;        //cola
    private Stack<GameObject> items;            //pila
    private Dictionary<string, GameObject> inventario;      //diccionario
    
    private Progresion progresionJugador;       // del tipo Progresion

    //private bool presionado = false;

    // Inicia la cola objetivos, la pila items, el diccionario inventario, el método CargarObjetivos para cargar la cola , el método VerObjetivos para ver los gameObject de la cola 
    private void Awake()
    {
        objetivos = new Queue<GameObject>();
        items = new Stack<GameObject>();
        inventario = new Dictionary<string, GameObject>();
        
        CargarObjetivos();
        VerObjetivos();

        progresionJugador = GetComponent<Progresion>();
    }

    // Carga los gameObjetc coleccionables en la cola objetivos
    private void CargarObjetivos()
    {
        foreach (Transform objetivo in padreObjetivos)
        {
            objetivos.Enqueue(objetivo.gameObject);
        }
    }

    // Muestra coleccionables que colicionarone en la consola
    private void VerObjetivos()
    {
        foreach (GameObject objetivo in objetivos)
        {
            Debug.Log(objetivo.name);
        }
    }

    // Pregunta si el objeto recogido es el mismo objeto que está primero en la cola
    private bool EsObjetivoActual(GameObject objetivoActual, GameObject objetivoReal)
    {
        return objetivoActual == objetivoReal;
    }

    // Controla colisión con GameObjetec coleccionables
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // si no hay hay colisión con un gemaObject "Coleccionable" => interrumpir el método
        if (!collision.gameObject.CompareTag("Coleccionable"))
        {
            return;
        }

        // si la lista de objetivos si es nula => interrumpir el método
        if (objetivos.Count == 0)
        {
            return;
        }

        // Hace Peek (toma un elemento de la cola y lo asigna a una variable objeto = objetivo
        GameObject objetivo = objetivos.Peek();
       
        /*
         si hay colisión entre objetivo y algún gameObject => 
            desactiva el gameObject objetivo
            quita de la cola objetivos el primer gameObject
            pune en la pila item el gameObjetc objetivo
            adiciona al diccionario inventario el gameObject (nombre del objeto, objeto)
            llama al método VerObjetivos para ver en consola los gameObject de la cola
            cambia el padre del gameObject en la jerarquía a bolsa
            llama al método GanarExperiencia de Progresion
        */
        if(EsObjetivoActual(collision.gameObject, objetivo))
        {
            objetivo.SetActive(false);
            objetivos.Dequeue();
            items.Push(objetivo);
            inventario.Add(objetivo.name, objetivo);
            VerObjetivos();
            objetivo.transform.SetParent(bolsa.transform);

            progresionJugador.GanarExperiencia(10);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        /*
         si se presiona F1 =>
            si la cuenta de gameObject en la cola == 0 =>
                se interrumpe el proceso
                sino activo el método UsarItem
        */
        //if (Input.GetKeyDown(KeyCode.F1))
        //{
        //    if (objetivos.Count == 0) return;

        //    //presionado = !presionado;

        //    UsarItem(); ;
        //}

        /*
         si 
        */
        // si clic en F1 y el diccionario inventario tiene clave "Manzana" => activar método UsarInventario
        if (Input.GetKeyDown(KeyCode.F1) && inventario.ContainsKey("Manzana"))
        {
            UsarInventario(inventario["Manzana"]);
        }

        // si clic en F1 y el diccionario inventario tiene clave "Naranja" => activar método UsarInventario
        if (Input.GetKeyDown(KeyCode.F2) && inventario.ContainsKey("Naranja"))
        {
            UsarInventario(inventario["Naranja"]);
        }

        // si clic en F1 y el diccionario inventario tiene clave "Anana" => activar método UsarInventario
        if (Input.GetKeyDown(KeyCode.F3) && inventario.ContainsKey("Anana"))
        {
            UsarInventario(inventario["Anana"]);
        }

        // si clic en F1 y el diccionario inventario tiene clave "Melon" => activar método UsarInventario
        if (Input.GetKeyDown(KeyCode.F4) && inventario.ContainsKey("Melon"))
        {
            UsarInventario(inventario["Melon"]);
        }

    }

    // Posiciona el gameObject objeto en la pila
    private void UsarItem()
    {
        // Hace Pop del objeto a una variable local item
        // Asigna el padre como nulo 
        // Activa el item
        GameObject item = items.Pop();
        item.transform.SetParent(null);
        item.transform.position = transform.position;
        item.SetActive(true);
    }

    // Posiiona el gameObject inventario en el diccionario
    private void UsarInventario(GameObject item)
    {
        // Asigna el padre como nulo 
        // cambia la posición del item
        // Activa el item
        item.transform.SetParent(null);
        item.transform.position = transform.position;
        item.SetActive(true);
    }
}
