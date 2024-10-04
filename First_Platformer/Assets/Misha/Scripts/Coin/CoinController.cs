using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Transform _coinSpawnPosition;
    [SerializeField, Range(0f, 100f)] private float _spawnChance;

    private CoinPhysics _addForceToCoin;
    private BoxCollider _boxCollider;

    private GameObject _coinObject;
    private Animator _coinAnimator;

    private void Awake() => Init();

    private void Init()
    {
        float randomPercent = Random.Range(0f, 100f);
        _coinObject = Instantiate(_coinPrefab, _coinSpawnPosition.position, Quaternion.identity, _coinSpawnPosition);

        _addForceToCoin = _coinObject.GetComponentInChildren<CoinPhysics>();
        _boxCollider = _coinObject.GetComponent<BoxCollider>();
        _coinAnimator = _coinObject.GetComponent<Animator>();

        if (randomPercent <= _spawnChance)
            _coinSpawnPosition.gameObject.SetActive(true);

        else
            _coinSpawnPosition.gameObject.SetActive(false);
    }

    public void RespawnCoin()
    {
        _addForceToCoin.TurnOffPhysics();
        _coinObject.transform.SetParent(_coinSpawnPosition);
        _coinObject.transform.localPosition = _coinSpawnPosition.localPosition;
        _coinObject.transform.rotation = Quaternion.Euler(Vector3.zero);
        _coinSpawnPosition.gameObject.SetActive(false);

        float randomPercent = Random.Range(0f, 100f);

        _coinObject.transform.SetParent(_coinSpawnPosition);
        _coinObject.transform.position = _coinSpawnPosition.position;
        _boxCollider.enabled = true;
        _coinAnimator.enabled = true;

        if (randomPercent <= _spawnChance)
            _coinSpawnPosition.gameObject.SetActive(true);
    }

    public void CoinOnCarCrash(Vector3 direction)
    {
        if (_coinSpawnPosition.gameObject.activeInHierarchy)
        {
            _boxCollider.enabled = false;
            _coinAnimator.enabled = false;
            _coinObject.transform.SetParent(null);
            _addForceToCoin.TurnOnPhysics(direction);
        }
    }

}
