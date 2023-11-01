using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    [SerializeField] float velocidad = 5f;

    // define una variable float para el tiempo de vida del proyectil
    [SerializeField][Range(0f, 60f)] private float timeOfLive;

    public int damage;

    private float aliveTime = 0f;

    // Actualiza posición del proyectil que sale del Jefe
    void Update()
    {
        transform.position += Vector3.left * velocidad * Time.deltaTime;

        // Incrementa el tiempo de vida del proyectil
        aliveTime += Time.deltaTime;

        // Si el tiempo de vida supera el valor deseado, destruye el proyectil
        if (aliveTime >= timeOfLive)
        {
            Destroy(gameObject);
        }
    }

    // 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damagableObjetc = collision.GetComponent<IDamageable>();
        if(damagableObjetc != null )
        {
            damagableObjetc.TakeDamage(damage);
            Debug.Log("toco al player");
            Destroy(gameObject);
        }
    }

   
}
