using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MateriPeta : MonoBehaviour
{
    [Header("Daftar Materi")]
    [TextArea(3, 10)]
    public string[] daftarMateri;

    [Header("UI Panel Materi")]
    public Text textMateri;
    public Button buttonLanjut;
    public GameObject panelMateri;

    [Header("Player Settings")]
    public PlayerMove playerMove;

    [Header("Misi / Progress Bar")]
    public MissionBar missionBar;
    public float progressValue = 0.25f;

    [Header("Efek Ketik")]
    public float kecepatanKetik = 0.02f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip soundBukaMateri;
    public AudioClip soundKlikLanjut;

    private int indexMateri = 0;
    private bool pemainDiDekat = false;
    private static MateriPeta aktifSekarang = null;

    private Coroutine ketikCoroutine;

    private void Start()
    {
        if (buttonLanjut != null)
            buttonLanjut.onClick.AddListener(LanjutkanMateri);

        if (panelMateri != null)
            panelMateri.SetActive(false);
    }

    private void Update()
    {
        if (pemainDiDekat && aktifSekarang == null && Input.GetKeyDown(KeyCode.E))
        {
            TampilkanMateri();
        }
    }

    private void TampilkanMateri()
    {
        if (audioSource != null && soundBukaMateri != null)
        {
            audioSource.PlayOneShot(soundBukaMateri);
        }

        aktifSekarang = this;
        indexMateri = 0;
        panelMateri.SetActive(true);
        playerMove.enabled = false;

        if (ketikCoroutine != null) StopCoroutine(ketikCoroutine);
        ketikCoroutine = StartCoroutine(TampilkanDenganAnimasi(daftarMateri[indexMateri]));
    }

    private void LanjutkanMateri()
    {
        if (audioSource != null && soundKlikLanjut != null)
        {
            audioSource.PlayOneShot(soundKlikLanjut);
        }

        if (aktifSekarang != this) return;

        indexMateri++;
        if (indexMateri < daftarMateri.Length)
        {
            if (ketikCoroutine != null) StopCoroutine(ketikCoroutine);
            ketikCoroutine = StartCoroutine(TampilkanDenganAnimasi(daftarMateri[indexMateri]));
        }
        else
        {
            TutupPanelMateri();
        }
    }

    private void TutupPanelMateri()
    {
        panelMateri.SetActive(false);
        playerMove.enabled = true;
        aktifSekarang = null;

        if (missionBar != null)
        {
            missionBar.UpdateMissionBar(progressValue);
        }

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pemainDiDekat = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pemainDiDekat = false;
        }
    }

    private IEnumerator TampilkanDenganAnimasi(string teks)
    {
        textMateri.text = "";
        foreach (char huruf in teks)
        {
            textMateri.text += huruf;
            yield return new WaitForSeconds(kecepatanKetik);
        }
    }
}
