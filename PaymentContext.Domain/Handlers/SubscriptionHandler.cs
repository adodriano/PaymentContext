using Flunt.Notifications;
using PaymentContext.Domain;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentContext.Shared.Handlers
{
    public class SubscriptionHandler :
        Notifiable<Notification>,
        IHandler<CreateBoletoSubscriptionCommand>,
        IHandler<CreatePayPalSubscriptionCommand>,
        IHandler<CreateCreditCardSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }
        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            // Fail Fast Validation
            command.Validate();
            if (command.IsValid == false)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            }
            // verificar se documento ja existe
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF ja esta em uso");

            // verificar se email ja existe
            if (_repository.EmailExists(command.Email))
                AddNotification("Email", "Este E-mail ja esta em uso");

            // gerar as VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(
                command.Street,
                command.Number,
                command.Neighborhood,
                command.City,
                command.State,
                command.Country,
                command.ZipCode
                );

            // gerar as entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(
                command.BarCode,
                command.BoletoNumber,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                new Document(command.PayerDocument, command.PayerDocumentType),
                address,
                email
                );

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // checar as notificações
            if (IsValid == false)
                return new CommandResult(false, "Não foi possível realizar sua assinatura");

            // salvar as informações
            _repository.CreateSubscription(student);

            // enviar email de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo", "Sua assinatura foi criada");

            // retornar informações

            return new CommandResult(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            //CREATE
            // Fail Fast Validation 
            //command.Validate();
            //if (command.IsValid == false)
            //{
            //    AddNotifications(command);
            //    return new CommandResult(false, "Não foi possível realizar sua assinatura");
            //}

            // verificar se documento ja existe
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF ja esta em uso");

            // verificar se email ja existe
            if (_repository.EmailExists(command.Email))
                AddNotification("Email", "Este E-mail ja esta em uso");

            // gerar as VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(
                command.Street,
                command.Number,
                command.Neighborhood,
                command.City,
                command.State,
                command.Country,
                command.ZipCode);

            // gerar as entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(
                 command.TransactionCode,
                 command.PaidDate,
                 command.ExpireDate,
                 command.Total,
                 command.TotalPaid,
                 command.Payer,
                 new Document(command.PayerDocument, command.PayerDocumentType),
                 address,
                 email
                 );

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // checar as notificações
            if (IsValid == false)
                return new CommandResult(false, "Não foi possível realizar sua assinatura");

            // salvar as informações
            _repository.CreateSubscription(student);

            // enviar email de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo", "Sua assinatura foi criada");

            // retornar informações

            return new CommandResult(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
        {
            //CREATE
            // Fail Fast Validation 
            //command.Validate();
            //if (command.IsValid == false)
            //{
            //    AddNotifications(command);
            //    return new CommandResult(false, "Não foi possível realizar sua assinatura");
            //}

            // verificar se documento ja existe
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF ja esta em uso");

            // verificar se email ja existe
            if (_repository.EmailExists(command.Email))
                AddNotification("Email", "Este E-mail ja esta em uso");

            // gerar as VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(
                command.Street,
                command.Number,
                command.Neighborhood,
                command.City,
                command.State,
                command.Country,
                command.ZipCode);

            // gerar as entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new CreditoPayment(
                 command.CardHolderName,
                 command.CardNumber,
                 command.LastTransactionNumber,
                 command.PaidDate,
                 command.ExpireDate,
                 command.Total,
                 command.TotalPaid,
                 command.Payer,
                 new Document(command.PayerDocument, command.PayerDocumentType),
                 address,
                 email
                 );
            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // checar as notificações
            if (IsValid == false)
                return new CommandResult(false, "Não foi possível realizar sua assinatura");

            // salvar as informações
            _repository.CreateSubscription(student);

            // enviar email de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo", "Sua assinatura foi criada");

            // retornar informações

            return new CommandResult(true, "Assinatura realizada com sucesso");
        }
    }
}
