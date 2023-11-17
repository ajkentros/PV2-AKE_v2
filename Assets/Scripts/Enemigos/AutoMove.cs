using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    [SerializeField] float velocidad = 5f;

    // define una variable float para el tiempo de vida del proyectil
    [SerializeField][Range(0f, 60f)] private float timeOfLive;

    public int damage;

    private readonly float aliveTime = 0f;

    // Actualiza posición del proyectil que sale del Jefe
    void Update()
    {
        transform.position += Time.deltaTime * velocidad * Vector3.left;

        // Incrementa el tiempo de vida del proyectil
        timeOfLive -= Time.deltaTime;

        // Si el tiempo de vida supera el valor deseado, destruye el proyectil
        if (aliveTime >= timeOfLive)
        {
            Destroy(gameObject);
        }
    }

    // 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //IDamageable damagableObjetc = collision.GetComponent<IDamageable>();
        if (collision.TryGetComponent<IDamageable>(out var damagableObjetc))
        {
            damagableObjetc.TakeDamage(damage);
            Debug.Log("toco al player");
            Destroy(gameObject);
        }
    }

   
}
