using Flunt.Validations;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain;

public class Subscription : Entity
{
    private IList<Payment> _payments;
    public Subscription(DateTime? expireDate)
    {
        CreateDate = DateTime.Now;
        LastUpdateDate = DateTime.Now;
        ExpireDate = expireDate;
        Active = true;
        _payments = new List<Payment>();
    }

    public DateTime CreateDate { get; private set; }
    public DateTime LastUpdateDate { get; private set; }
    public DateTime? ExpireDate { get; private set; }
    public bool Active { get; private set; }
    public IReadOnlyCollection<Payment> Payments { get; private set; }

    public void AddPayment(Payment payment)
    {
        AddNotifications(new Contract<Payment>()
            .Requires()
            .IsGreaterThan(payment.PaidDate, DateTime.Now, "Subscription.Payments", "A data do pagamento deve ser futura")
        );

        //if (IsValid()) //validação de data futura se for valido
        _payments.Add(payment);
    }

    public void Activate()
    {
        Active = true;
        LastUpdateDate = DateTime.Now;

    } 
    public void Inactivate()
    {
        Active = false;
        LastUpdateDate = DateTime.Now;

    }
}
