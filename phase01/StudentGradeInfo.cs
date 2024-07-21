public class StudentGradeInfo
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public double Average { get; set; }

    public string ToString()
    {
        return $"FirstName: {FirstName} LastName: {LastName} Average: {Average}";
    }
}