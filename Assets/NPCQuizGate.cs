using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCQuizGate : MonoBehaviour
{
    [Header("UI Components")]
    public GameObject dialogPanel;
    public TMP_Text dialogText;
    public Button nextButton;
    public Button choiceButton1;
    public Button choiceButton2;

    [Header("Dialog Content")]
    public string[] dialogLines;
    public string[] choices;
    public string[] responseGood;
    public string[] responseBad;
    public string[] followUpLines;
    public string followUpQuestion;
    public string[] followUpChoices;
    public string[] followUpResponses;

    [Header("Rintangan (Obstacles)")]
    public GameObject goodPathObstacle;
    public GameObject badPathObstacle;

    [Header("Correct Answer")]
    public int correctAnswerIndex = 0;

    private int currentLine = 0;
    private bool isPlayerInRange = false;
    private bool isChoicePhase = false;
    private bool hasChosen = false;
    private bool isFollowUpPhase = false;
    private bool isSecondChoicePhase = false;

    private int playerChoice = -1;
    private int secondPlayerChoice = -1;
    private int followUpIndex = 0;

    void Start()
    {
        // Menyembunyikan semua UI saat awal
        dialogPanel.SetActive(false);
        nextButton.gameObject.SetActive(false);
        choiceButton1.gameObject.SetActive(false);
        choiceButton2.gameObject.SetActive(false);

        // Menambahkan listener untuk tombol-tombol
        nextButton.onClick.AddListener(OnNextButtonClicked);
        choiceButton1.onClick.AddListener(() => MakeChoice(0));
        choiceButton2.onClick.AddListener(() => MakeChoice(1));
    }

    void Update()
    {
        // Memulai dialog jika pemain menekan E saat berada di dekat NPC
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            dialogPanel.SetActive(true);
            nextButton.gameObject.SetActive(true);
            ShowNextLine();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Menandai bahwa pemain memasuki area interaksi
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Menutup dialog dan reset jika pemain keluar dari area
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogPanel.SetActive(false);
            ResetDialog();
        }
    }

    void OnNextButtonClicked()
    {
        // Mengatur alur dialog tergantung status saat ini
        if (isFollowUpPhase)
        {
            ShowFollowUp();
            return;
        }

        if (isSecondChoicePhase)
        {
            ShowSecondResponse();
            return;
        }

        if (!isChoicePhase && !hasChosen)
        {
            ShowNextLine();
        }
        else if (hasChosen)
        {
            ShowResponse();
        }
    }

    void ShowNextLine()
    {
        // Menampilkan baris berikutnya dari dialog pembuka
        if (currentLine < dialogLines.Length)
        {
            dialogText.text = dialogLines[currentLine];
            currentLine++;
        }
        else
        {
            ShowChoices();
        }
    }

    void ShowChoices()
    {
        // Menampilkan pilihan awal (baik/buruk)
        isChoicePhase = true;
        dialogText.text = "Pilihan ada di tanganmu, apakah kau akan memilih jalan yang benar atau jalan yang gelap?";
        nextButton.gameObject.SetActive(false);
        choiceButton1.gameObject.SetActive(true);
        choiceButton2.gameObject.SetActive(true);

        choiceButton1.GetComponentInChildren<TMP_Text>().text = choices[0];
        choiceButton2.GetComponentInChildren<TMP_Text>().text = choices[1];
    }

    void MakeChoice(int choice)
    {
        // Menyimpan pilihan pemain dan lanjutkan sesuai fase
        if (isSecondChoicePhase)
        {
            secondPlayerChoice = choice;
            ShowSecondResponse();
            return;
        }

        playerChoice = choice;
        hasChosen = true;

        choiceButton1.gameObject.SetActive(false);
        choiceButton2.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);
    }

    void ShowResponse()
    {
        // Menampilkan respon berdasarkan pilihan pertama
        if (playerChoice == correctAnswerIndex)
        {
            dialogText.text = responseGood.Length > 0 ? responseGood[0] : "Jalan kebaikan memang berat, tapi kau memilih dengan bijaksana.";
            goodPathObstacle.SetActive(false);
            badPathObstacle.SetActive(false);
        }
        else
        {
            dialogText.text = responseBad.Length > 0 ? responseBad[0] : "Jalan gelap itu mempesona, tapi penuh jebakan.";
            goodPathObstacle.SetActive(false);
            badPathObstacle.SetActive(true);
        }

        nextButton.gameObject.SetActive(true);
        isFollowUpPhase = true;
        followUpIndex = 0;
    }

    void ShowFollowUp()
    {
        // Menampilkan dialog lanjutan setelah pilihan pertama
        if (followUpIndex < followUpLines.Length)
        {
            dialogText.text = followUpLines[followUpIndex];
            followUpIndex++;
        }
        else
        {
            isFollowUpPhase = false;
            ShowSecondChoices();
        }
    }

    void ShowSecondChoices()
    {
        // Menampilkan pilihan jebakan kedua
        isSecondChoicePhase = true;
        dialogText.text = followUpQuestion;

        choiceButton1.GetComponentInChildren<TMP_Text>().text = followUpChoices[0];
        choiceButton2.GetComponentInChildren<TMP_Text>().text = followUpChoices[1];

        choiceButton1.gameObject.SetActive(true);
        choiceButton2.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(false);
    }

    void ShowSecondResponse()
    {
        // Menampilkan respon dari pilihan jebakan
        dialogText.text = followUpResponses[secondPlayerChoice];
        choiceButton1.gameObject.SetActive(false);
        choiceButton2.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);

        Invoke(nameof(CloseDialog), 3f);
    }

    void CloseDialog()
    {
        // Menutup panel dialog dan reset
        dialogPanel.SetActive(false);
        ResetDialog();
    }

    void ResetDialog()
    {
        // Mengatur ulang semua status dialog ke awal
        currentLine = 0;
        isChoicePhase = false;
        hasChosen = false;
        isFollowUpPhase = false;
        isSecondChoicePhase = false;
        playerChoice = -1;
        secondPlayerChoice = -1;
        followUpIndex = 0;
    }
}
