using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _doghouse;
    [SerializeField] private float _volumeChangeSpeed = 0.5f;

    private float _maxAlarmVolume;
    private float _minAlarmVolume;
    private float _targetVolume;

    private void Awake()
    {
        _maxAlarmVolume = 1.0f;
        _minAlarmVolume = 0.0f;
        _targetVolume = 0.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<Player>(out Player player))
        {
            _doghouse.Play();
            _doghouse.volume += _volumeChangeSpeed * Time.deltaTime;
            _targetVolume = _maxAlarmVolume;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.TryGetComponent<Player>(out Player player))
        {
            _targetVolume = _minAlarmVolume;
        }
    }

    private void Update()
    {
        if (_doghouse.volume != _minAlarmVolume)
        {
            _doghouse.volume = Mathf.MoveTowards(_doghouse.volume, _targetVolume, _volumeChangeSpeed * Time.deltaTime);
        }
        else
        {
            _doghouse.Pause();
        }
    }
}
