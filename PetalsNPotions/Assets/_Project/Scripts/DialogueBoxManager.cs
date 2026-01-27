using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueBoxManager : MonoBehaviour
{
    [SerializeField] List<string> dialogues;
    [SerializeField] private TextMeshProUGUI _dialogueText;

    private List<int> alreadyShownDialogus = new List<int>();

    private CanvasGroup _canvasGroup;
    private bool shouldShowDialogue = false;


    private void Start()
    {
        _canvasGroup= GetComponent<CanvasGroup>();
        gameObject.SetActive(false);
        _canvasGroup.alpha = 0f;
    }


    public void ShowDialogue(int index , bool isRewardingDialogue = default)
    {
        if (alreadyShownDialogus.Count > 0 && alreadyShownDialogus.Contains(index))
            return;

        if(index < 0 || index >= dialogues.Count)
        {
            Debug.LogWarning("Dialogue index out of range");
            return;
        }
        _dialogueText.text = dialogues[index];
        gameObject.SetActive(true);
        shouldShowDialogue = true;

        alreadyShownDialogus.Add(index);

        StartCoroutine(_DisableDialogueTimer(isRewardingDialogue));

    }

    private IEnumerator _DisableDialogueTimer(bool isrewarding)
    {
        yield return new WaitForSeconds(4f);
        HideDialogue();

        if (isrewarding)
            MenuController.Instance.UpdateCoinsCount(10);

        yield break;
    }

    public void HideDialogue()
    {
        shouldShowDialogue = false;
    }

    private void Update()
    {
        if(shouldShowDialogue)
        {
            _canvasGroup.alpha = Mathf.Lerp(_canvasGroup.alpha, 1f, Time.deltaTime * 2);

        }

        else
        {
            float lerpAlpha = Mathf.Lerp(_canvasGroup.alpha, 0f, Time.deltaTime * 5f);
            _canvasGroup.alpha = lerpAlpha;
            if (_canvasGroup.alpha <= 0.01f)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
