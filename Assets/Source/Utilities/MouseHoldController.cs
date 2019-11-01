using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MouseHoldController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public UnityEvent Fired = new UnityEvent();

    public float HoldingDelay = 0.064f;
    public float InitialDelayBetweenFiring = 0.75f;
    public float TargetDelayBetweenFiring = 0.075f;
    public float SpeedupDuration = 3f;

    private bool isHolding;
    private float holdElapsed;
    private float firedElapsed;

    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
        holdElapsed = 0f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHolding = false;
        holdElapsed = 0f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;
        holdElapsed = 0f;
    }

    private void Update()
    {
        if (isHolding)
        {
            if (holdElapsed > HoldingDelay)
            {
                var transition = SpeedupDuration == 0 ? 0 : Mathf.Clamp(holdElapsed - HoldingDelay, 0, SpeedupDuration) / SpeedupDuration;
                var delay = Mathf.Lerp(1 - transition, TargetDelayBetweenFiring, InitialDelayBetweenFiring);

                if (firedElapsed > delay)
                {
                    Fired.Invoke();
                    firedElapsed = 0f;
                }

                firedElapsed += Time.deltaTime;
            }

            holdElapsed += Time.deltaTime;
        }
    }
}