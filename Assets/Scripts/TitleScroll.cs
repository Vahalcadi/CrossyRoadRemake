using System.Collections;
using UnityEngine;

public class MoveImage : MonoBehaviour
{
    [SerializeField] RectTransform imageTransform;
    [SerializeField] Vector2 targetPosition;
    [SerializeField] float duration = 5f;
    private bool started;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            started = true;
            StartCoroutine(MoveImageToPosition());
        }
    }

    IEnumerator MoveImageToPosition()
    {
        Vector2 startPosition = imageTransform.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            imageTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        imageTransform.anchoredPosition = targetPosition;
    }
}
