namespace PaymentContext.Domain;

public class Subscription
{
    public DateTime CreateDate { get; set; }
    public DateTime LastUpdateDate { get; set; }
    public DateTime? ExpireDate { get; set; }
    public bool active { get; set; }
    public List<Payment> payments { get; set; }


}
