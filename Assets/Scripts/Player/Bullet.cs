using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Movimiento
    public GameObject prefabBala;
    public Transform puntoDisparo;
    public float velocidadBala = 10f;
    public float tiempoVidaBala = 5f;
    public float intervaloDisparo = 0.5f;


    public GameObject efectoDestruccionPrefab;
    public float duracionDestruccion = 0.3f;

    private float ultimoDisparo;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (Time.time > ultimoDisparo + intervaloDisparo)
            {
                Shoot();
                ultimoDisparo = Time.time;
            }
        }
    }

    void Shoot()
    {
        if (prefabBala == null || puntoDisparo == null) return;

        GameObject nuevaBala = Instantiate(prefabBala, puntoDisparo.position, Quaternion.identity);
        Rigidbody2D rb = nuevaBala.GetComponent<Rigidbody2D>();
        if (rb != null) rb.velocity = Vector2.up * velocidadBala;

        Destroy(nuevaBala, tiempoVidaBala);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (efectoDestruccionPrefab != null)
            {
                GameObject efecto = Instantiate(
                    efectoDestruccionPrefab,
                    other.transform.position,
                    Quaternion.identity
                );
                Destroy(efecto, duracionDestruccion);
            }

            EnemyMovement enemySwarm = other.transform.parent?.GetComponent<EnemyMovement>();
            if (enemySwarm != null) enemySwarm.DestroyEnemy(other.transform);

            Destroy(gameObject);
        }
    }
}