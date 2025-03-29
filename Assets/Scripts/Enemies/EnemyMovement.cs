using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //Movimiento
    public float initialSpeed = 2f;
    public float dropDistance = 0.5f;
    public float speedIncreasePerKill = 0.2f;
    public float maxSpeed = 5f;

    //Matar naves
    public float deathDuration = 0.3f;
    public Sprite deathSprite;

    private bool movingRight = true;
    private Collider2D leftBoundary;
    private Collider2D rightBoundary;
    private float currentSpeed;
    private int initialEnemyCount;

    void Start()
    {
        currentSpeed = initialSpeed;
        initialEnemyCount = transform.childCount;
        leftBoundary = GameObject.FindGameObjectWithTag("LeftBoundary").GetComponent<Collider2D>();
        rightBoundary = GameObject.FindGameObjectWithTag("RightBoundary").GetComponent<Collider2D>();
    }

    void Update()
    {
        Vector3 direction = movingRight ? Vector3.right : Vector3.left;
        transform.Translate(direction * currentSpeed * Time.deltaTime);

        bool hitBoundary = false;
        foreach (Transform enemy in transform)
        {
            if (enemy == null) continue;

            if ((movingRight && rightBoundary.bounds.Contains(enemy.position)) ||
                (!movingRight && leftBoundary.bounds.Contains(enemy.position)))
            {
                hitBoundary = true;
                break;
            }
        }

        if (hitBoundary)
        {
            movingRight = !movingRight;
            transform.Translate(Vector3.down * dropDistance);
        }
    }

    public void DestroyEnemy(Transform enemy)
    {
        SpriteRenderer renderer = enemy.GetComponent<SpriteRenderer>();
        if (renderer != null && deathSprite != null)
        {
            renderer.sprite = deathSprite;
        }

        Collider2D collider = enemy.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        Destroy(enemy.gameObject, deathDuration);

        currentSpeed = Mathf.Min(currentSpeed + speedIncreasePerKill, maxSpeed);
    }
}