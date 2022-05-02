namespace gRpc1ClientDomain.Entities.Base;

abstract public class Entity : IKndEntity<int>
{
    [Key]
    public int Id { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }
}
