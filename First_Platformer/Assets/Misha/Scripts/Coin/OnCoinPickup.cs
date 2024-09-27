using Supercyan.FreeSample;
using UnityEngine;

public class OnCoinPickup : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    private void OnTriggerEnter(Collider collider)
    {
        OnPlayerPickup(collider);
        _audioSource.Play();
    }

    private void OnPlayerPickup(Collider collider)
    {
        if (collider.GetComponent<SimpleSampleCharacterControl>() != null)
        {
            PlayerWallet.AddCoin();
            GetComponentInParent<CoinController>().CoinOnCarCrash(collider.transform.forward);
        }
    }
}
