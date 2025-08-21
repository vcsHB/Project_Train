namespace Project_Train.Core.Attribute
{
    public class DisplayNameAttribute : System.Attribute
    {
        public string Name { get; }
        public DisplayNameAttribute(string name) => Name = name;
    }

}