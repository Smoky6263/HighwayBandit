using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [Header("����� �����")]
    [SerializeField] private AudioClip[] _footSteps;

    [Header("���� ������")]
    [SerializeField] private AudioClip _jumpSound;

    private AudioSource _audioSource;

    private void Awake() =>_audioSource = GetComponent<AudioSource>(); 

    public void StepEvent()
    {
        int randomSound = Random.Range(0, _footSteps.Length);
        _audioSource.PlayOneShot(_footSteps[randomSound]);
    }

    public void PlayerOnJumpEvent() => _audioSource.PlayOneShot(_jumpSound);
}
