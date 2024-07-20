using System;
using System.IO;
using System.Text.Json;

public class Student
{
    private int _studentNumber;
    public int StudentNumber { get { return _studentNumber; } set { _studentNumber = value; } }
    private string _firstName;
    public string FirstName { get { return _firstName; } set { _firstName = value; } }
    private string _lastName;
    public string LastName { get { return _lastName; } set { _lastName = value; } }
}

public class Grade
{
    private int _studentNumber;
    public int StudentNumber { get { return _studentNumber; } set { _studentNumber = value; } }
    private string _lesson;
    public string Lesson { get { return _lesson; } set { _lesson = value; } }
    private double _score;
    public double Score { get { return _score; } set { _score = value; } }
}

public class Program
{
    public static void Main()
    {
        
    }
}