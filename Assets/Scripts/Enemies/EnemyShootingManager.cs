using UnityEngine;

public class EnemyShootingManager : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    public float shootInterval = 1f;
    public float bulletLifetime = 5f;

    private float shootTimer;

    void Update()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= shootInterval)
        {
            shootTimer = 0f;
            ShootFromRandomEnemy();
        }
    }

    void ShootFromRandomEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return;

        GameObject bullet = Instantiate(
            enemyBulletPrefab,
            enemies[Random.Range(0, enemies.Length)].transform.position,
            Quaternion.identity
        );

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.down * 7f;
        }

        Destroy(bullet, bulletLifetime);
    }
}