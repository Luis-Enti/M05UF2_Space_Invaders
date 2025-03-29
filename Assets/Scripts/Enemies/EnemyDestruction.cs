using UnityEngine;

public class EnemyDestruction : MonoBehaviour
{
    public GameObject destructionEffect;
    public float effectDuration = 0.5f;
    public int enemyRow;

    public GameObject enemyPrefab;
    public static int enemiesRemaining;

    private static Vector3[] initialPositions;
    private static Transform enemyParent;
    private static bool initialized = false;

    void Start()
    {
        if (!initialized)
        {
            enemyParent = transform.parent;

            initialPositions = new Vector3[enemyParent.childCount];
            for (int i = 0; i < enemyParent.childCount; i++)
            {
                initialPositions[i] = enemyParent.GetChild(i).position;
            }

            enemiesRemaining = enemyParent.childCount;
            initialized = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);

            if (destructionEffect != null)
            {
                GameObject effect = Instantiate(destructionEffect, transform.position, Quaternion.identity);
                Destroy(effect, effectDuration);
            }

            ScoreManager.instance?.AddEnemyScore(enemyRow);

            enemiesRemaining--;
            Destroy(gameObject);

            if (enemiesRemaining <= 0)
            {
                RespawnAllEnemies();
            }
        }
    }

    void RespawnAllEnemies()
    {
        if (enemyPrefab == null) return;

        for (int i = 0; i < initialPositions.Length; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, initialPositions[i], Quaternion.identity, enemyParent);
            EnemyDestruction enemyScript = newEnemy.GetComponent<EnemyDestruction>();
            if (enemyScript != null)
            {
                enemyScript.enemyRow = i / 10;
                enemyScript.enemyPrefab = this.enemyPrefab;
            }


            newEnemy.SetActive(true);
            SpriteRenderer sr = newEnemy.GetComponent<SpriteRenderer>();
            if (sr != null) sr.enabled = true;
        }

        enemiesRemaining = initialPositions.Length;
    }
}