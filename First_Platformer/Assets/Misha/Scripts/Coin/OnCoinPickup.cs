using Supercyan.FreeSample;
using UnityEngine;

public class OnCoinPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        OnPlayerPickup(collider);
    }

    private void OnPlayerPickup(Collider collider)
    {
        if (collider.GetComponent<SimpleSampleCharacterControl>() != null)
        {
            PlayerWallet.AddCoin();
            GetComponentInParent<CoinController>().CoinOnCarCrash();
        }
    }
}
