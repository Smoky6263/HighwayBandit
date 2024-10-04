using System.Linq;
using System.Threading.Tasks;
public static class GetRandomElementsClass
{
    public static int[] GetRandomElementsFromArray(int arrayLength, int countOfNumbersToReturn)
    {
        int[] array = GetNewArray(arrayLength);
        return DoFisherYatesSort(array, arrayLength, countOfNumbersToReturn);
    }

    public static async Task<int[]> GetRandomElementsFromArrayAsync(int arrayLength, int countOfNumbersToReturn)
    {
        return await Task.Run(() =>
        {
            int[] array = GetNewArray(arrayLength);
            return DoFisherYatesSort(array, arrayLength, countOfNumbersToReturn);
        });
    }

    private static int[] GetNewArray(int arrayLength) 
    {
        int[] array = new int[arrayLength];
        for (int i = 0; i < arrayLength; i++)
        {
            array[i] = i;
        }
        return array;
    }

    private static int[] DoFisherYatesSort(int[] array, int arrayLength, int countOfNumbersToReturn)
    {
        System.Random rand = new System.Random();

        // Перемешиваем массив с помощью алгоритма Фишера-Йетса
        for (int i = arrayLength - 1; i > 0; i--)
        {
            int j = rand.Next(i + 1);
            (array[i], array[j]) = (array[j], array[i]);
        }

        return array.Take(countOfNumbersToReturn).ToArray();
    }
}
