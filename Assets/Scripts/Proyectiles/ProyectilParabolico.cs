using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilParabolico : Proyectil
{
    // define la variable privada float launchAngle = ángulo de disparo del proyectil
    [SerializeField]
    [Range(0f, 90f)]
    private float launchAngle = 45f;

    // mueve el proyectil parabólico siguiendo una trayectoria parabólica
    protected override void Mover()
    {
        // convierte ángulos a radianes
        float launchAngleInRadians = launchAngle * Mathf.Deg2Rad;
       
        // define un vector2 launchVelocity para el lanzamiento del proyectil parabólico (usa el coseno para la componente x; usa el seno para la componente y)
        Vector2 launchVelocity = new(Mathf.Cos(launchAngleInRadians) * speed, Mathf.Sin(launchAngleInRadians) * speed);

        // aplica la velocidad inicial al objeto proyectil parabólico
        rb.velocity = launchVelocity;

    }
}
