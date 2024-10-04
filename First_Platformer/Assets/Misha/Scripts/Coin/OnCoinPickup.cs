using Supercyan.FreeSample;
using UnityEngine;

public class OnCoinPickup : MonoBehaviour
{
    CoinController _coinController;
    private AudioSource _audioSource;

    private void Awake()
    {
        _coinController = GetComponentInParent<CoinController>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        OnPlayerPickup(collider);
    }

    private void OnPlayerPickup(Collider collider)
    {
        if (collider.GetComponent<PlayerController>() != null)
        {
            _audioSource.Play();
            PlayerWallet.AddCoin();
            _coinController.CoinOnCarCrash(collider.transform.forward);
        }
    }
}
