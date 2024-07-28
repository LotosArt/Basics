namespace LibLesson.AuctionDirectory;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Quantity { get; set; }

    public override string ToString()
    {
        return $"Id - {Id}, Name - {Name}, description - {Description}, price - {Price}, startDate - {StartDate}, " +
               $"endDate - {EndDate}, quantity - {Quantity}";
    }
}