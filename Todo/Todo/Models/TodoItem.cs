using SQLite;

namespace Todo
{
	public class TodoItem
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
		public bool Done { get; set; }
        public string NextMessageDT { get; set; }
    }

    public class ObjectGroup
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class Obj
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public double H { get; set; }
        public double W { get; set; }
        public int ObjectTypeID { get; set; }
        public int ObjectGroupID { get; set; }
        public string Hours { get; set; }
        public string Address { get; set; }
    }
    
    public class TodoObject
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int TodoItemID { get; set; }
        public int ObjectGroupID { get; set; }
    }
    
}

