using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    [SerializeField] float velocidad = 5f;

    public int damage;

    // Actualiza posición del proyectil que sale del Jefe
    void Update()
    {
        transform.position += Vector3.left * velocidad * Time.deltaTime;
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
