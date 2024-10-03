using UnityEngine;

public class BarrierChild : PlayerOnDefeatTrigger
{
    [Header("���� ��� ����� ������ �� ������")]
    [SerializeField] private AudioClip _audioClip;
    private AudioSource _audioSource;
    
    private void Awake() => _audioSource = GetComponent<AudioSource>();
    protected override void GameOver(Collision collision)
    {
        base.GameOver(collision);
        _audioSource.PlayOneShot(_audioClip);
    }
}
