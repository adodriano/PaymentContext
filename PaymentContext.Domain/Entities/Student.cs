namespace PaymentContext.Domain;

public class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Document { get; set; }
    public string Email { get; set; }
    public string Andress { get; set; }
    public List<Subscription> Subscriptions { get; set; }

}
