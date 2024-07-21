
public class Program
{
    public static async Task Main()
    {
        string studentFilePath = Console.ReadLine();
        string gradesFilePath = Console.ReadLine();
        var topThreeStuden = await GetTopThreeStudent(studentFilePath, gradesFilePath);
        foreach (var student in topThreeStuden)
        {
            Console.WriteLine(student.ToString());
        }
    }

    public static async Task<IEnumerable<StudentGradeInfo>> GetTopThreeStudent(string studentFilePath, string gradesFilePath)
    {
        var studentList = await Json<Student>.ReadStudentsFromJsonFile(studentFilePath);
        var gradesList = await Json<Grade>.ReadStudentsFromJsonFile(gradesFilePath);
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
