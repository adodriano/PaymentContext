﻿using PaymentContext.Domain;
using PaymentContext.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentContext.Tests.Mocks
{
    public class FakeStudentRepository : IStudentRepository
    {
        public void CreateSubscription(Student student)
        {
            throw new NotImplementedException();
        }

        public bool DocumentExists(string document)
        {

            if (document == "99999999999")
            {
                return true;
            }

            return false;
        }

        public bool EmailExists(string email)
        {

            if (email == "ad@ad.com")
            {
                return true;
            }

            return false;
        }

    }
}
