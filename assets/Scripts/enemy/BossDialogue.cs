using UnityEngine;
using Ink.Runtime;
using System.Collections;

public class BossDialogue : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [Header("Boss Fight")]
    [SerializeField] private Boss boss;
    [SerializeField] private Health playerHealth;

    [SerializeField] private int damageToBoss = 1;
    [SerializeField] private int damageToPlayer = 1;
    [SerializeField] private float delayAfterAnswer = 1f;

    private bool playerInRange;
    private bool isFightOngoing = false;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        Debug.Log("BossDialogue: Awake called. Visual cue set to inactive.");
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying && !isFightOngoing)
        {
            visualCue.SetActive(true);
            if (InputManager.GetInstance().GetInteractPressed())
            {
                Debug.Log("BossDialogue: Player interacted. Starting boss fight.");
                StartBossFight();
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void StartBossFight()
    {
        isFightOngoing = true;
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        DialogueManager.GetInstance().OnChoiceMade += HandleChoiceMade;
        Debug.Log("BossDialogue: Boss fight started. Dialogue mode entered.");
    }

    private void HandleChoiceMade(int choiceIndex)
    {
        Debug.Log($"BossDialogue: Choice made. Index: {choiceIndex}");
        StartCoroutine(ProcessAnswerAfterDelay());
    }

    private IEnumerator ProcessAnswerAfterDelay()
    {
        Debug.Log($"BossDialogue: Waiting for {delayAfterAnswer} seconds before processing answer.");
        yield return new WaitForSeconds(delayAfterAnswer);

        bool correct = (bool)DialogueManager.GetInstance().GetVariableState("correct");
        Debug.Log($"BossDialogue: Answer was {(correct ? "correct" : "incorrect")}.");

        if (correct)
        {
            boss.TakeDamage(damageToBoss);
            Debug.Log($"BossDialogue: Boss took {damageToBoss} damage.");
        }
        else
        {
            boss.Attack();
            playerHealth.Damage(damageToPlayer);
            Debug.Log($"BossDialogue: Boss attacked. Player took {damageToPlayer} damage.");
        }

        yield return new WaitForSeconds(0.5f); // Short delay to allow for attack animation

        if (boss.IsDead() || playerHealth.IsDead())
        {
            Debug.Log("BossDialogue: Boss or player is dead. Ending fight.");
            EndBossFight(boss.IsDead());
        }
        else
        {
            Debug.Log("BossDialogue: Fight continues. Continuing dialogue.");
            ContinueDialogue();
        }
    }

    private void ContinueDialogue()
    {
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            Debug.Log("BossDialogue: Re-entering dialogue mode for next question.");
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        }
        else
        {
            Debug.Log("BossDialogue: Dialogue is still playing. Waiting for next choice.");
        }
    }

    private void EndBossFight(bool playerWon)
    {
        DialogueManager.GetInstance().OnChoiceMade -= HandleChoiceMade;
        DialogueManager.GetInstance().ExitDialogueMode();
        isFightOngoing = false;

        if (playerWon)
        {
            Debug.Log("BossDialogue: Player won the boss fight!");
            // Implement win condition here
        }
        else
        {
            Debug.Log("BossDialogue: Player lost the boss fight!");
            // Implement lose condition here
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
            Debug.Log("BossDialogue: Player entered range.");
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
            Debug.Log("BossDialogue: Player exited range.");
        }
    }
}