using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 7f;
    public string[] tagsToIgnore = {"Enemy"};

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
        }

        rb.velocity = Vector2.down * speed;

        IgnoreTaggedObjects();
    }

    void IgnoreTaggedObjects()
    {
        Collider2D bulletCollider = GetComponent<Collider2D>();
        if (bulletCollider == null) return;

        foreach (string tag in tagsToIgnore)
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag(tag))
            {
                Collider2D objCollider = obj.GetComponent<Collider2D>();
                if (objCollider != null)
                {
                    Physics2D.IgnoreCollision(bulletCollider, objCollider);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destruir la bala al chocar con CUALQUIER objeto
        Destroy(gameObject);

        if (collision.gameObject.CompareTag("Edificio"))
        {
           
        }
    }
}