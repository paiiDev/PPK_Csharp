using System;
using System.Collections.Generic;

namespace DatabaseFirstExample.DataAccess;

public partial class Student
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Age { get; set; }

    public bool DeleteFlag { get; set; }
}
