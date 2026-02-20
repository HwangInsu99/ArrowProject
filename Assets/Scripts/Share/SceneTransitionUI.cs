using System.Collections;
using UnityEngine;

public class SceneTransitionUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _fadeGroup;
    [SerializeField] private float _defaultFadeDuration = 0.25f;
    [SerializeField] private bool _useUnscaledTime = true;

    private Coroutine _fadeRoutine;

    public void Init()
    {
        if (_fadeGroup == null)
        {
            Debug.LogWarning("FadeGroup 비어 있음");
            return;
        }

        _fadeGroup.alpha = 0.0f;
        _fadeGroup.blocksRaycasts = false;
        _fadeGroup.interactable = false;
    }

    public IEnumerator CO_FadeTo(float targetAlpha, float duration = -1f, bool blockRayCastsWhileFading = true)
    {
        if (_fadeGroup == null)
        {
            Debug.LogWarning("Co_FadeTo 실패 -> _fadeGroup 확인");
            yield break;
        }

        if (duration < 0f)
        {
            duration = _defaultFadeDuration;
        }

        if (_fadeRoutine != null)
        {
            StopCoroutine(_fadeRoutine);
            _fadeRoutine = null;
        }

        _fadeRoutine = StartCoroutine(Co_Fade_Internal(targetAlpha, duration, blockRayCastsWhileFading));

        yield return _fadeRoutine;

        _fadeRoutine = null;
    }

    private IEnumerator Co_Fade_Internal(float targetAlpha, float duration, bool blockRayCastsWhileFading)
    {
        float startAlpha = _fadeGroup.alpha;

        _fadeGroup.blocksRaycasts = blockRayCastsWhileFading;
        _fadeGroup.interactable = false;

        if (duration <= 0f)
        {
            _fadeGroup.alpha = targetAlpha;
            _fadeGroup.blocksRaycasts = targetAlpha >= 0.99f;

            yield break;
        }

        float t = 0;

        while (t < duration)
        {
            float dt = _useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;

            t += dt;

            float lerp = Mathf.Clamp01(t / duration);
            _fadeGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, lerp);

            yield return null;
        }

        _fadeGroup.alpha = targetAlpha;
        _fadeGroup.blocksRaycasts = targetAlpha >= 0.99f;
    }
}
