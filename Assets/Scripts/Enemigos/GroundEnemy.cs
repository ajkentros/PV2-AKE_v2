using UnityEngine;

public class GroundEnemy : Enemy
{
    private void Update()
    {
        Move();
    }

    protected override void Move()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
