
using phase01;

public class Program
{
    public static async Task Main()
    {
        var topThreeStuden = await GetTopThreeStudent();
        foreach (var student in topThreeStuden)
        {
            Console.WriteLine(student.ToString());
        }
    }

    public static async Task<IEnumerable<StudentGradeInfo>> GetTopThreeStudent()
    {
        var studentData = new ReadJason<Student>();
        var gradeData = new ReadJason<Grade>();
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
            }).OrderByDescending(student => student.Average).Take(3);

        return topThreeStudent;

    }
}
