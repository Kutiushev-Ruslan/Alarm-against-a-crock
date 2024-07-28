using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _volumeChangeSpeed;
    [SerializeField] private float _volumeChangeDelay;
    [SerializeField] private float _minimumVolume;
    [SerializeField] private float _maximumVolume;

    private Coroutine _volumeChangeCoroutine;

    public void TurnAlarmOn()
    {
        if (_volumeChangeCoroutine != null)
        {
            StopCoroutine(_volumeChangeCoroutine);
        }

        _volumeChangeCoroutine = StartCoroutine(StartChangingVolume(_maximumVolume));
    }

    public void TurnAlarmOff()
    {
        if (_volumeChangeCoroutine != null)
        {
            StopCoroutine(_volumeChangeCoroutine);
        }

        _volumeChangeCoroutine = StartCoroutine(StartChangingVolume(_minimumVolume));
    }

    private IEnumerator StartChangingVolume(float targetVolume)
    {
        WaitForSeconds wait = new WaitForSeconds(_volumeChangeDelay);

        if (_audioSource.isPlaying == false)
        {
            _audioSource.Play();
        }

        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _volumeChangeSpeed);

            yield return wait;
        }

        if (_audioSource.volume == _minimumVolume)
        {
            _audioSource.Stop();
        }
    }
}
