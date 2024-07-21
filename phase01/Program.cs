using System.Text.Json;

public class Student
{
    private int _studentNumber;
    public int StudentNumber { get { return _studentNumber; } set { _studentNumber = value; } }
    private string _firstName;
    public string FirstName { get { return _firstName; } set { _firstName = value; } }
    private string _lastName;
    public string LastName { get { return _lastName; } set { _lastName = value; } }

    public static List<Student> ReadStudentsFromJsonFile()
    {
        string path = "Students.txt";
        string JsonData = File.ReadAllText(path);
        List<Student> students = JsonSerializer.Deserialize<List<Student>>(JsonData);
        return students;
    }
}

public class Grade
{
    private int _studentNumber;
    public int StudentNumber { get { return _studentNumber; } set { _studentNumber = value; } }
    private string _lesson;
    public string Lesson { get { return _lesson; } set { _lesson = value; } }
    private double _score;
    public double Score { get { return _score; } set { _score = value; } }

    public static List<Grade> ReadGradesFromJsonFile()
    {
        string path = "Grades.txt";
        string JsonData = File.ReadAllText(path);
        List<Grade> grades = JsonSerializer.Deserialize<List<Grade>>(JsonData);
        return grades;
    }
}

public class Program
{
    public static void Main()
    {

        var studentList = Student.ReadStudentsFromJsonFile().GroupJoin(Grade.ReadGradesFromJsonFile(),
            student => student.StudentNumber,
            grade => grade.StudentNumber,
            (student, gradeList) => new
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Average = gradeList.Any() ? gradeList.Average(grade => grade.Score) : 0
            });

        studentList = studentList.OrderByDescending(student => student.Average).Take(3);
        foreach (var student in studentList)
        {
            Console.WriteLine(student);
        }
    }
}