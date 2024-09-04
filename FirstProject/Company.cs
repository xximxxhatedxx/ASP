public class Company
{
    public int ID { get; set; }
    public string Name { get; set; }

    public Company(int id, string name) {
        ID = id;
        Name = name;
    }

    public override string ToString()
    {
        return $"( {ID} - {Name} )";
    }
}
