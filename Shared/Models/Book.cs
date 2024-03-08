using System;
using System.Collections.Generic;

namespace BindingData_SQL_EF.Shared.Models;

public partial class Book
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Author { get; set; } = null!;

    public int? Quantity { get; set; }

    public int Price { get; set; }

    public bool? Available { get; set; }
}
