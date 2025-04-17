using UnityEngine;
using UnityEngine.UI;

public class Materiv3 : MonoBehaviour
{
    [System.Serializable]
    public struct Soal
    {
        public string pertanyaan;
        public string jawabanBenar;
        public string jawabanSalah;
    }

    [Header("Soal & UI")]
    public Soal[] daftarSoal;
    public Text textSoal;
    public Button buttonA;
    public Button buttonB;
    public GameObject panelPertanyaan;

    [Header("Objek Lain")]
    public GameObject hartaKarun;
    public PlayerMove playerMove;
    public MissionBar missionBar;
    public Health playerHealth;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip soundBukaPertanyaan;
    public AudioClip soundJawabanBenar;
    public AudioClip soundJawabanSalah;

    private bool pemainDiDekat = false;
    private bool sudahDibuka = false;
    private Soal soalTerpilih;

    private void Start()
    {
        // Menyembunyikan panel pertanyaan di awal
        panelPertanyaan.SetActive(false);  
    }

    private void Update()
    {
        // Jika pemain menekan E di dekat harta karun dan panel belum aktif
        if (pemainDiDekat && !panelPertanyaan.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            if (sudahDibuka) return;

            // Cek apakah pemain punya kunci, jika ya tampilkan soal
            if (KeyManager.Instance != null && KeyManager.Instance.GunakanKunci())
            {
                TampilkanPertanyaan();  
                sudahDibuka = true;
            }
            else
            {
                Debug.Log("❗ Tidak cukup kunci untuk membuka harta karun ini!");
            }
        }
    }

    private void TampilkanPertanyaan()
    {
        // Memutar suara saat membuka panel soal
        audioSource.PlayOneShot(soundBukaPertanyaan); 

        // Memilih soal secara acak dari daftar
        soalTerpilih = daftarSoal[Random.Range(0, daftarSoal.Length)];  
        textSoal.text = soalTerpilih.pertanyaan;

        // Acak posisi jawaban benar/salah antara button A dan B
        if (Random.value > 0.5f)
        {
            buttonA.GetComponentInChildren<Text>().text = soalTerpilih.jawabanBenar;
            buttonB.GetComponentInChildren<Text>().text = soalTerpilih.jawabanSalah;
        }
        else
        {
            buttonA.GetComponentInChildren<Text>().text = soalTerpilih.jawabanSalah;
            buttonB.GetComponentInChildren<Text>().text = soalTerpilih.jawabanBenar;
        }

        // Aktifkan panel pertanyaan dan nonaktifkan kontrol player
        panelPertanyaan.SetActive(true);  
        playerMove.enabled = false;  

        // Bersihkan dan atur ulang event listener tombol
        buttonA.onClick.RemoveAllListeners();
        buttonB.onClick.RemoveAllListeners();
        buttonA.onClick.AddListener(() => CekJawaban(buttonA.GetComponentInChildren<Text>().text));
        buttonB.onClick.AddListener(() => CekJawaban(buttonB.GetComponentInChildren<Text>().text));
    }

    private void CekJawaban(string jawabanUser)
    {
        // Jika jawaban benar, update misi dan sembunyikan harta karun
        if (jawabanUser == soalTerpilih.jawabanBenar)
        {
            audioSource.PlayOneShot(soundJawabanBenar);
            missionBar.UpdateMissionBar(0.25f);  
            HilangkanHartaKarun();  
        }
        else
        {
            // Jika jawaban salah, kurangi nyawa
            audioSource.PlayOneShot(soundJawabanSalah);
            playerHealth.TakeDamage(3);  

            if (playerHealth.currentHealth <= 0)
            {
                Debug.Log("Game Over!");
            }
        }

        // Tutup panel pertanyaan setelah menjawab
        TutupPertanyaan();
    }

    private void TutupPertanyaan()
    {
        // Menyembunyikan panel dan mengaktifkan kembali kontrol player
        panelPertanyaan.SetActive(false);  
        playerMove.enabled = true;  
    }

    private void HilangkanHartaKarun()
    {
        // Menyembunyikan objek harta karun setelah berhasil dijawab
        hartaKarun.SetActive(false);  
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Deteksi saat pemain masuk area harta karun
        if (other.CompareTag("Player"))
            pemainDiDekat = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Deteksi saat pemain keluar dari area harta karun
        if (other.CompareTag("Player"))
            pemainDiDekat = false;
    }
}
