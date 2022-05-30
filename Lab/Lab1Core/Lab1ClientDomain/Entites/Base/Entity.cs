namespace Lab1ClientDomain.Entites.Base;

public abstract class Entity : IKndEntity<int>
{
    [Key]
    public int Id { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }
}
