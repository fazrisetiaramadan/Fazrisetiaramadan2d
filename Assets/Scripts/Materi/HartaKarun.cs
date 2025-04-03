using UnityEngine;
using UnityEngine.UI;

public class HartaKarun : MonoBehaviour
{
    [System.Serializable]
    public struct Soal
    {
        public string pertanyaan;
        public string jawabanBenar;
        public string jawabanSalah;
    }

    [Header("Daftar Soal & Jawaban")]
    public Soal[] daftarSoal;

    [Header("UI Elements")]
    public Text textSoal;
    public Button buttonA;
    public Button buttonB;
    public GameObject panelPertanyaan;
    public GameObject hartaKarun;
    public PlayerMove playerMove;
    public MissionBar missionBar;
    public Health playerHealth; 
    
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip soundBukaPertanyaan;
    public AudioClip soundJawabanBenar;
    public AudioClip soundJawabanSalah;

    private bool pemainDiDekat = false;
    private Soal soalTerpilih;

    private void Start()
    {
        panelPertanyaan.SetActive(false);
    }

    private void Update()
    {
        if (pemainDiDekat && !panelPertanyaan.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            TampilkanPertanyaan();
        }
    }

    private void TampilkanPertanyaan()
    {
        audioSource.PlayOneShot(soundBukaPertanyaan);

        soalTerpilih = daftarSoal[Random.Range(0, daftarSoal.Length)];
        textSoal.text = soalTerpilih.pertanyaan;

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

        panelPertanyaan.SetActive(true);
        playerMove.enabled = false;

        buttonA.onClick.RemoveAllListeners();
        buttonB.onClick.RemoveAllListeners();
        buttonA.onClick.AddListener(() => CekJawaban(buttonA.GetComponentInChildren<Text>().text));
        buttonB.onClick.AddListener(() => CekJawaban(buttonB.GetComponentInChildren<Text>().text));
    }

    private void CekJawaban(string jawabanUser)
    {
        if (jawabanUser == soalTerpilih.jawabanBenar)
        {
            audioSource.PlayOneShot(soundJawabanBenar);
            HilangkanHartaKarun();
            missionBar.UpdateMissionBar(0.25f);
        }
        else
        {
            audioSource.PlayOneShot(soundJawabanSalah);
            
            // Mengurangi health sebanyak 3
            playerHealth.TakeDamage(3);

            // Jika health 0, panggil event game over
            if (playerHealth.currentHealth <= 0)
            {
                Debug.Log("Game Over!");
            }
        }

        TutupPertanyaan();
    }


    private void TutupPertanyaan()
    {
        panelPertanyaan.SetActive(false);
        playerMove.enabled = true;
    }

    private void HilangkanHartaKarun()
    {
        hartaKarun.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pemainDiDekat = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pemainDiDekat = false;
        }
    }
}
