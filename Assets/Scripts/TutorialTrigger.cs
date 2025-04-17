using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject tutorialPanel;
    public PlayerMove playerMoveScript; 
    private bool hasShown = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasShown && other.CompareTag("Player"))
        {
            if (tutorialPanel != null)
            {
                tutorialPanel.SetActive(true);
                hasShown = true;

                if (playerMoveScript != null)
                {
                    playerMoveScript.canMove = false; 
                }
            }
        }
    }

    public void CloseTutorialPanel()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(false);

            if (playerMoveScript != null)
            {
                playerMoveScript.canMove = true; 
            }
        }
    }
}
