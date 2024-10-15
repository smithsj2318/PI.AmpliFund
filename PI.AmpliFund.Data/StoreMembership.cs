namespace PI.AmpliFund.Data;

public class StoreMembership: ITrackedEntity
{
    public Guid StoreMembershipId { get; set; }
    public byte[] RowVersion { get; set; }
    public string Description { get; set; }
    public float Discount { get; set; }
    
    public virtual IEnumerable<ApplicationUser> ApplicationUsers { get; set; } = null!;
}