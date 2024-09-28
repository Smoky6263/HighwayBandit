using System.Linq;
using UnityEngine;

public class BarrierRespawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _barrierChild;
    [SerializeField] private int _countForRespawn;
    private int[] _childID;

    private Transform _player;

    private void Awake() => Initialize();

    public void Init(Transform player)
    {
        _player = player;
    }

    private void Initialize()
    {
        foreach (GameObject child in _barrierChild)
            child.SetActive(false);

        _childID = new int[_barrierChild.Length];

        for (int i = 0; i < _childID.Length; i++)
        {
            _childID[i] = i;
        }
    }

    public void ResetBarrier()
    {
        foreach (GameObject child in _barrierChild)
            child.SetActive(false);

        int[] randomElements = GetRandomElements(_childID, _countForRespawn);

        for (int i = 0; i < randomElements.Length; i++)
        {
            _barrierChild[randomElements[i]].SetActive(true);
        }

    }
    private int[] GetRandomElements(int[] array, int count)
    {
        System.Random rand = new System.Random();

        // Перемешиваем массив с помощью алгоритма Фишера-Йетса
        for (int i = array.Length - 1; i > 0; i--)
        {
            int k = rand.Next(i + 1);
            int temp = array[i];
            array[i] = array[k];
            array[k] = temp;
        }

        return array.Take(count).ToArray();
    }
}
