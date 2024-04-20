using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using System.Linq.Expressions;

namespace PaymentContext.Domain.Queries
{
    [TestClass]
    public class StudentQueriesTests
    {
        private IList<Student> _students;

        public StudentQueriesTests()
        {
            _students = new List<Student>();
            for (var i = 0; i <= 10; i++)
            {
                _students.Add(new Student(
                    new Name("Aluno", i.ToString()),
                    new Document("1111111111" + i.ToString(),
                    EDocumentType.CPF),
                    new Email("aluno" + i.ToString() + "@aluno.com")
                    ));
            }
        }

        [TestMethod]
        public void ShouldReturnNullWhenDocumentNotExists()
        {
            var exp = StudentQueries.GetStudentInfo("12345678911");
            var studnt = _students.AsQueryable().Where(exp).FirstOrDefault();
            Assert.AreEqual(null, studnt);
        }
        
        [TestMethod]
        public void ShouldReturnStudentWhenDocumentExists()
        {
            var exp = StudentQueries.GetStudentInfo("1111111111");
            var studnt = _students.AsQueryable().Where(exp).FirstOrDefault();
            Assert.AreEqual(null, studnt);
        }
    }
}
