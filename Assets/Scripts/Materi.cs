using UnityEngine;
using UnityEngine.UI;

public class Materi : MonoBehaviour
{
    [Header("Daftar Materi")]
    [TextArea(3, 10)]
    public string[] daftarMateri;

    [Header("UI Panel Materi")]
    public GameObject panelMateri;
    public Text textMateri;
    public Button buttonLanjut;

    [Header("Player")]
    public PlayerMove playerMove;

    [Header("Mission")]
    public MissionBar missionBar;
    public GameObject panelFinish;

    // Static untuk semua materi di level
    private static int totalMateri;
    private static int materiSelesai = 0;
    private static bool sudahHitung = false;

    private int index = 0;
    private bool pemainDiDekat = false;

    private void Start()
    {
        panelMateri.SetActive(false);
        buttonLanjut.onClick.AddListener(Lanjut);

        // Hitung total materi hanya sekali
        if (!sudahHitung)
        {
            totalMateri = FindObjectsOfType<Materi>().Length;
            sudahHitung = true;
        }
    }

    private void Update()
    {
        if (pemainDiDekat && !panelMateri.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            TampilkanMateri();
        }
    }

    private void TampilkanMateri()
    {
        index = 0;
        textMateri.text = daftarMateri[index];
        panelMateri.SetActive(true);
        playerMove.enabled = false;
    }

    private void Lanjut()
    {
        index++;
        if (index < daftarMateri.Length)
        {
            textMateri.text = daftarMateri[index];
        }
        else
        {
            SelesaiBacaMateri();
        }
    }

    private void SelesaiBacaMateri()
    {
        panelMateri.SetActive(false);
        playerMove.enabled = true;
        gameObject.SetActive(false);

        materiSelesai++;
        missionBar.UpdateMissionBar((float)materiSelesai / totalMateri);

        if (materiSelesai >= totalMateri && panelFinish != null)
        {
            panelFinish.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            pemainDiDekat = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            pemainDiDekat = false;
    }
}
