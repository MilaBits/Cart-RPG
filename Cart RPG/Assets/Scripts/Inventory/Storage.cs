using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Storage {
    public int id { get; private set; }
    public List<KeyValuePair<int, int>> items { get; private set; }

    public Storage(int id, List<KeyValuePair<int, int>> items)
    {
        this.id = id;
        this.items = items;
    }
}

