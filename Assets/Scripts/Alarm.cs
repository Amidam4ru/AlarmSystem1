using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _doghouse;
    [SerializeField] private float _volumeChangeSpeed = 0.5f;

    private float _maxAlarmVolume;
    private float _minAlarmVolume;
    private Coroutine _shootCorutine;
    private WaitForEndOfFrame _frameTimer;

    private void Awake()
    {
        _maxAlarmVolume = 1.0f;
        _minAlarmVolume = 0.0f;
        _frameTimer = new WaitForEndOfFrame();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<Player>(out Player player))
        {
            StopActiveCoroutine(_shootCorutine);

            _shootCorutine = StartCoroutine(ChancgeVolume(_maxAlarmVolume));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.TryGetComponent<Player>(out Player player))
        {
            StopActiveCoroutine(_shootCorutine);

            _shootCorutine = StartCoroutine(ChancgeVolume(_minAlarmVolume));
        }
    }

    private void StopActiveCoroutine(Coroutine coroutine)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    private IEnumerator ChancgeVolume(float targetVolume)
    {
        if (_doghouse.volume == 0)
        {
            _doghouse.Play();
        }

        while (_doghouse.volume != targetVolume)
        {
            _doghouse.volume = Mathf.MoveTowards(_doghouse.volume, targetVolume, _volumeChangeSpeed * Time.deltaTime);

            yield return _frameTimer;
        }

        if (_doghouse.volume == 0)
        {
            _doghouse.Pause();
        }

        yield return null;
    }
}
