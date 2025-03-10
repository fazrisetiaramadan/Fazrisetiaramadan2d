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
    public MissionBar missionBar; // Tambahkan reference ke MissionBar

    private bool pemainDiDekat = false;
    private Soal soalTerpilih;

    private void Start()
    {
        panelPertanyaan.SetActive(false);
    }

    private void Update()
    {
        if (pemainDiDekat && !panelPertanyaan.activeSelf)
        {
            TampilkanPertanyaan();
        }
    }

    private void TampilkanPertanyaan()
    {
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
            Debug.Log("Jawaban Benar!");
            HilangkanHartaKarun();
            missionBar.UpdateMissionBar(0.25f); // Tambah progress bar misi 25%
        }
        else
        {
            Debug.Log("Jawaban Salah! Coba lagi.");
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
