using UnityEngine;
public class RandomElementsRespawner : MonoBehaviour
{
    [SerializeField] protected  GameObject[] _itemArray;
    [SerializeField] protected int _countForRespawn;
    public int CountForRespawn { get { return _countForRespawn; } set { _countForRespawn = value; } }

    protected int[] _randomElements;

    protected void Awake() => Initialize();

    protected virtual void Initialize() 
    {
        foreach (GameObject child in _itemArray)
            child.SetActive(false);

        RandomizeElementsAsync();
    }
    protected void RandomizeElements()
    {
        _randomElements = GetRandomElementsClass.GetRandomElementsFromArray(_itemArray.Length, _countForRespawn);
    }
    protected async void RandomizeElementsAsync()
    {
        _randomElements = await GetRandomElementsClass.GetRandomElementsFromArrayAsync(_itemArray.Length, _countForRespawn);
    }

    public virtual void ResetElements() 
    {
        foreach (GameObject child in _itemArray)
            child.SetActive(false);

        for (int i = 0; i < _randomElements.Length; i++)
        {
            _itemArray[_randomElements[i]].SetActive(true);
        }

        RandomizeElementsAsync();
    }
}
