using UnityEngine;

public class JebakanPaku : MonoBehaviour
{
    [Header("Pengaturan Waktu")]
    public float interval = 2f; // Waktu muncul-tenggelam dalam detik

    private Collider2D col;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Mulai sistem muncul-tenggelam berulang
        InvokeRepeating("TogglePaku", 0, interval);
    }

    void TogglePaku()
    {
        bool isActive = !gameObject.activeSelf;
        gameObject.SetActive(isActive);

        // Jika Paku di-disable, pastikan collider dan sprite juga nonaktif
        if (col != null) col.enabled = isActive;
        if (spriteRenderer != null) spriteRenderer.enabled = isActive;
    }
}
