namespace phase01;
interface IReadData<T>
{
    Task<List<T>> Read();
}