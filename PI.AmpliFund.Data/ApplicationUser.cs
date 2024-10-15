namespace PI.AmpliFund.Data;

public class ApplicationUser: ITrackedEntity
{
    public Guid ApplicationUserId { get; set; }
    public byte[] RowVersion { get; set; }
    public string ApplicationUserName { get; set; }

    public Guid StoreMembershipId { get; set; }
    public virtual StoreMembership Membership { get; set; }
    
    public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
}