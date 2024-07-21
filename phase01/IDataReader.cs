namespace phase01;
interface IDataReader<T>
{
    Task<List<T>> Read();
}