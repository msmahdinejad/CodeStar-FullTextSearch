using phase01;

public class Program
{
    public static async Task Main()
    {
        try
        {
            var topThreeStuden = await GetTopStudent(3);
            foreach (var student in topThreeStuden)
            {
                Console.WriteLine(student.ToString());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public static async Task<IEnumerable<StudentGradeInfo>> GetTopStudent(int num)
    {
        var studentData = new JsonReader<Student>();
        var gradeData = new JsonReader<Grade>();
        var studentList = await studentData.Read();
        var gradesList = await gradeData.Read();
        var topThreeStudent = studentList.GroupJoin(gradesList,
            student => student.StudentNumber,
            grade => grade.StudentNumber,
            (student, gradeList) => new StudentGradeInfo
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Average = gradeList.Any() ? gradeList.Average(grade => grade.Score) : 0
            }).OrderByDescending(student => student.Average).Take(num);

        return topThreeStudent;

    }
}
