namespace PaymentContext.Domain;

public class Subscription
{
    private IList<Payment> _payments;
    public Subscription(DateTime? expireDate)
    {
        CreateDate = DateTime.Now;
        LastUpdateDate = DateTime.Now;
        ExpireDate = expireDate;
        active = true;
        _payments = new List<Payment>();
    }

    public DateTime CreateDate { get; private set; }
    public DateTime LastUpdateDate { get; private set; }
    public DateTime? ExpireDate { get; private set; }
    public bool active { get; private set; }
    public IReadOnlyCollection<Payment> payments { get; private set; }

    public void AddPayment(Payment payment)
    {
        _payments.Add(payment);
    }

    public void Activate()
    {
        active = true;
        LastUpdateDate = DateTime.Now;

    } 
    public void Inactivate()
    {
        active = false;
        LastUpdateDate = DateTime.Now;

    }
}
