using UnityEngine;

public class BuildingHealth : MonoBehaviour
{
    //Vidas del edificio
    public int maxHealth = 15;
    public GameObject destructionEffect;
    public float effectDuration = 0.5f;

    //Efectos
    public Sprite[] damageStates;
    public SpriteRenderer buildingRenderer;

    //Daño a estructuras
    public int playerBulletDamage = 1;
    public int enemyBulletDamage = 1;

    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateDamageVisual();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(playerBulletDamage);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("EnemyBullet"))
        {
            TakeDamage(enemyBulletDamage);
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateDamageVisual();

        if (currentHealth <= 0)
        {
            DestroyBuilding();
        }
    }

    void UpdateDamageVisual()
    {
        if (damageStates.Length > 0 && buildingRenderer != null)
        {
            int stateIndex = Mathf.FloorToInt((1f - (float)currentHealth / maxHealth) * (damageStates.Length - 1));
            buildingRenderer.sprite = damageStates[Mathf.Clamp(stateIndex, 0, damageStates.Length - 1)];
        }
    }

    void DestroyBuilding()
    {
        if (destructionEffect != null)
        {
            GameObject effect = Instantiate(destructionEffect, transform.position, Quaternion.identity);
            Destroy(effect, effectDuration);
        }

        Collider2D buildingCollider = GetComponent<Collider2D>();
        if (buildingCollider != null) buildingCollider.enabled = false;

        if (buildingRenderer != null) buildingRenderer.enabled = false;

        Destroy(gameObject, effectDuration + 0.1f);
    }
}