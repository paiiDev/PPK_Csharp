using StudentManagement;

string dbAddress = "Data Source=.;Initial Catalog=DotNetDatabase;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";

//Console.WriteLine("Enter student name: ");
//string name = Console.ReadLine();

//Console.WriteLine("Enter student age: ");
//string inputString = Console.ReadLine();
//int age = int.Parse(inputString);

//Student stu = new Student(dbAddress);
//stu.Create(name, age);

//Student stu = new Student(dbAddress);
//stu.ReadAllData();

//Console.WriteLine("Enter student id: ");
//string inputString = Console.ReadLine();
//int id = int.Parse(inputString);

//Student stu = new Student(dbAddress);
//stu.Read(id);

//Console.WriteLine("Enter student id to update: ");
//string inputString = Console.ReadLine();
//int id = int.Parse(inputString);

//Console.WriteLine("Enter student name to update: ");
//string name = Console.ReadLine();

//Console.WriteLine("Enter student age to update: ");
//string ageInput = Console.ReadLine();
//int age = int.Parse(ageInput);

//Student stu = new Student(dbAddress);
//stu.Update(id, name, age);

Console.WriteLine("Enter student id to delete: ");
string inputString = Console.ReadLine();
int id = int.Parse(inputString);

Student stu = new Student(dbAddress);
stu.Delete(id);