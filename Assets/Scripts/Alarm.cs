using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _volumeChangeSpeed = 0.5f;
    [SerializeField] private AlarmZone _alarmZone;

    private float _maxAlarmVolume;
    private float _minAlarmVolume;
    private Coroutine _chancgeVolumeCorutine;
    private WaitForEndOfFrame _frameTimer;
    private AudioSource _doghouse;

    private void Awake()
    {
        _maxAlarmVolume = 1.0f;
        _minAlarmVolume = 0.0f;
        _frameTimer = new WaitForEndOfFrame();
        _doghouse = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _alarmZone.PlayerIsNoticed += SwitchAlarm;
    }

    private void OnDisable()
    {
        _alarmZone.PlayerIsNoticed -= SwitchAlarm;
    }

    private void SwitchAlarm(bool isWork)
    {
        if (_chancgeVolumeCorutine != null)
        {
            StopCoroutine(_chancgeVolumeCorutine);
        }

        if (isWork == true)
        {
            _chancgeVolumeCorutine = StartCoroutine(ChangeVolume(_maxAlarmVolume));
        }
        else
        {
            _chancgeVolumeCorutine = StartCoroutine(ChangeVolume(_minAlarmVolume));
        }  
    }

    private IEnumerator ChangeVolume(float targetVolume)
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
