using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static KeyManager Instance;

    public int jumlahKunci = 0;  

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Menambah kunci ketika pemain mengambil kunci
    public void TambahKunci()
    {
        jumlahKunci++;
        Debug.Log("🔑 Kunci didapat! Jumlah kunci: " + jumlahKunci);
    }

    // Menggunakan 1 kunci jika tersedia
    public bool GunakanKunci()
    {
        if (jumlahKunci > 0)
        {
            jumlahKunci--;
            Debug.Log("🔓 Kunci digunakan. Sisa kunci: " + jumlahKunci);
            return true;
        }
        else
        {
            Debug.Log("❌ Tidak ada kunci!");
            return false;
        }
    }
}
