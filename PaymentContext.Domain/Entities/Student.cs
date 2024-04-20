using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain;

public class Student : Entity
{
    private IList<Subscription> _subscriptions;
    public Student(Name name, Document document, Email email)
    {
        Name = name;
        Document = document;
        Email = email;
        _subscriptions = new List<Subscription>();

        AddNotifications(name, document, email);
    }

    public Name Name { get; private set; }
    public Document Document { get; private set; }
    public Email Email { get; private set; }
    public Address Andress { get; private set; }
    public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray(); } }

    public void AddSubscription(Subscription subscription)
    {   
        //metodo gerado do chatGPT
        // Verificar se o aluno já possui uma assinatura ativa
        var hasActiveSubscription = _subscriptions.Any(s => s.Active);
        if (hasActiveSubscription)
        {
            AddNotification("Student.Subscriptions", "Você já possui uma assinatura ativa.");
            return;
        }

        // Verificar se a assinatura tem pelo menos um pagamento associado
        if (subscription.Payments == null || subscription.Payments.Count == 0)
        {
            AddNotification("Student.Subscriptions.Payments", "Esta assinatura não possui pagamentos.");
            return;
        }

        // Se não houver problemas, adicionar a assinatura à lista de assinaturas do aluno
        _subscriptions.Add(subscription);
    }

    //metodo do curso com erro nos testes unitarios
    //public void AddSubscription(Subscription subscription)
    //{   
    //    //cancel other subscription an add this one principally
    //    var hasSubscriptionActive = false;
    //    foreach (var sub in Subscriptions)
    //    {
    //        if (sub.Active)
    //            hasSubscriptionActive = true;
    //    }

    //    AddNotifications(new Contract<Subscription>()
    //        .Requires()
    //        .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Você já tem uma assinatura ativa")
    //        .AreEquals(0, subscription.Payments.Count, "Student.Subscriptions.Payments", "Essa assinatura não possui pagamentos")
    //    );

    //    //alternatively

    //    //if (hasSubscriptionActive)
    //    //    AddNotification("Student.Subscriptions", "Você já tem uma assinatura ativa");

    //}

}
