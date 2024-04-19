using PaymentContext.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class StudentTests
    {
        [TestMethod]
        public void TestMethod1()
        {   
            var subscription = new Subscription(null);
            var student = new Student("Cleber", "Ferreira", "12345678912", "cleber@gmail.com");
            student.AddSubscription(subscription);


        }

    }
}
