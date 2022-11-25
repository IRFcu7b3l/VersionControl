using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        [Test,
         TestCase("abcd1234", false),
         TestCase("irf@uni-corvinus", false),
         TestCase("irf.uni-corvinus.hu", false),
         TestCase("irf@uni-corvinus.hu", true)

            ]
        void TestValidateEmail(string email, bool expectedResult) {
            //Arrange
            var accountController = new AccountController();
            //Act
            var actualResult = accountController.ValidateEmail(email);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
            
    
        }
        
            [Test,
            TestCase("asd", false),
            TestCase("asdfghjké", false),
            TestCase("Asdfghjk", false),
            TestCase("Asdfghjk1", true)]
        void TestValidatePassword(string password, bool expectedResult) {
            // a jelszó legalább 8 karakter hosszú kell legyen, csak az angol ABC betűiből és számokból állhat,
            // és tartalmaznia kell legalább egy kisbetűt, egy nagybetűt és egy számot.
            
        }
        public bool ValidatePassword(string password)
        {
            Regex regex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$");
            return regex.IsMatch(password);
        }

    }
}
