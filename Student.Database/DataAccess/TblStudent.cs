using System;
using System.Collections.Generic;

namespace Student.Database.DataAccess;

public partial class TblStudent
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Age { get; set; }

    public bool DeleteFlag { get; set; }
}
