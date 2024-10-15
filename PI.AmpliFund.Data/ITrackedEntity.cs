namespace PI.AmpliFund.Data;

public interface ITrackedEntity
{
    byte[] RowVersion { get; set; }
}