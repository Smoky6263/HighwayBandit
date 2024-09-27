using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [Header("Звуки шагов")]
    [SerializeField] private AudioClip[] _footSteps;

    private AudioSource _audioSource;

    private void Awake() =>_audioSource = GetComponent<AudioSource>(); 

    public void StepEvent()
    {
        int randomSound = Random.Range(0, _footSteps.Length);
        _audioSource.PlayOneShot(_footSteps[randomSound]);
    }
}
