
public class SpawnRandomProps : RandomElementsRespawner
{
    protected override void Initialize()
    {
        GetFirstRandomSpawn();
    }

    private void GetFirstRandomSpawn()
    {
        RandomizeElements();
        ResetElements();
    }

    public override void ResetElements()
    {
        TurnOffProps();

        for (int i = 0; i < _itemArray.Length; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                _itemArray[i].transform.GetChild(_randomElements[i]).gameObject.SetActive(true);
            }
        }

        RandomizeElementsAsync();
    }
    private void TurnOffProps()
    {
        for (int i = 0; i < _itemArray.Length; i++)
        {
            for (int j = 0; j < _itemArray[i].transform.childCount; j++)
            {
                _itemArray[i].transform.GetChild(j).gameObject.SetActive(false);
            }
        }
    }
}
