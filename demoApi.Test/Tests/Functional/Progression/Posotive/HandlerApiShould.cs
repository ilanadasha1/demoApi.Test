using Allure.Commons;
using demoApi.Test.Bootstrap;
using demoApi.Test.SDK.DTO;
using FluentAssertions;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

namespace demoApi.Test
{
    [AllureNUnit]
    [TestFixture]
    [AllureSubSuite("Demo")]
    public class HandlerApiShould : TestBase
    {
        [Test(Description = "Return Ok when Setting Handler Position succedded")]
        [AllureSuite("Posetive")]
        [AllureSubSuite("Move")]
        [AllureTag("Status", "Move")]
        [AllureIssue("Issue#1")]
        [AllureFeature("Move")]
        [AllureSeverity(SeverityLevel.critical)]
        public async Task ReturnOkOnSetHandlerPosition()
        {
            //Arrange
            var (X, Y, Z) = (10, 20, 30);

            //Act
            var response = await HandlerApi.SetHandlerPosition(X, Y, Z);

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "response.StatusCode should be EqualTo HttpStatusCode.OK");
        }

        [Test(Description = "Return Ack when Set Handler Position succedded")]
        [AllureSuite("Posetive")]
        [AllureSubSuite("Move")]
        [AllureTag("Status", "Move")]
        [AllureFeature("Move")]
        [AllureSeverity(SeverityLevel.minor)]
        public async Task ReturnAckOnSetHandlerPosition()
        {
            //Arrange
            var (X, Y, Z) = (10, 20, 30);

            //Act
            var response = await HandlerApi.SetHandlerPosition(X, Y, Z);

            //Assert
            response.Content.Status.Should().Equals(CommandStatus.Ack);
        }

        [Test(Description = "Return the Position when set Handler Position succedded")]
        [AllureSuite("Posetive")]
        [AllureSubSuite("Move")]
        [AllureTag("Position")]
        [AllureFeature("Move")]
        [AllureSeverity(SeverityLevel.critical)]
        [TestCase(5,23,55)]
        [TestCase(54, -2, 0)]
            public async Task ReturnPositionOnSetHandlerPosition(int x, int y, int z)
        {
            //Arange
            var (X, Y, Z) = (x,y, z);

            //Act
            var response = await HandlerApi.SetHandlerPosition(X, Y, Z);

            //Assert
            Assert.AreEqual("Move", response.Content?.Type, "Type should be Move");
            Assert.AreEqual(X, response.Content?.position.X);
            Assert.AreEqual(Y, response.Content?.position.Y);
            Assert.AreEqual(Z, response.Content?.position.Z);
        }

        [Test(Description = "Return the current Position when Get Handler Position succedded")]
        [AllureSuite("Posetive")]
        [AllureSubSuite("Get")]
        [AllureTag("Position", "Get")]
        [AllureFeature("Get")]
        [AllureSeverity(SeverityLevel.blocker)]
        public async Task ReturnCurrentPositionOnGetHandlerPosition()
        {
            //Arrange
            //Set handler position (Note: The Api generate Random 'current' 
            //position and save it to memory)
            
            var response = await HandlerApi.SaveHandlerPosition();
            var (X, Y, Z) =
                (response.Content.position.X,
                response.Content.position.Y,
                response.Content.position.Z);

            //Act
            response = await HandlerApi.GetCurrentHandlerPosition();

            //Assert
            Assert.AreEqual(X, response.Content?.position.X);
            Assert.AreEqual(Y, response.Content?.position.Y);
            Assert.AreEqual(Z, response.Content?.position.Z);
        }
        
       
        [Test(Description = "Return the Position wheb saving handler position")]
        [AllureSuite("Posetive")]
        [AllureSubSuite("Save")]
        [AllureTag("Position", "Save")]
        [AllureFeature("Save")]
        [AllureSeverity(SeverityLevel.critical)]
        public async Task ReturnPositionOnSave()
        {
            // Arrange + Act
            var response = await HandlerApi.SaveHandlerPosition();

            //Assert
            Assert.That(response.Content.position, Is.Not.Null, $"the {response.Content.position} should not be null");
        }
        
        [Test(Description = "Return the OK status when saving handler position")]
        [AllureSuite("Posetive")]
        [AllureSubSuite("Save")]
        [AllureTag("Status")]
        [AllureFeature("Save")]
        [AllureSeverity(SeverityLevel.critical)]
        public async Task ReturnOKOnSaveHandlerPosition()
        {
            // Arrange + Act
            var response = await HandlerApi.SaveHandlerPosition();

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Content.Type, Is.EqualTo("Save"));
        }
        
        [Test(Description = "Return the OK status, CommandStatus.Ack when locking handler position")]
        [AllureSuite("Posetive")]
        [AllureSubSuite("Lock")]
        [AllureTag("Status", "Lock")]
        [AllureFeature("Lock")]
        [AllureSeverity(SeverityLevel.normal)]
        public async Task ReturnOKOnLockHandlerPosition()
        {
            // Arrange + Act
            var response = await HandlerApi.LockHandlerPosition();

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "response.StatusCode, Is.EqualTo(HttpStatusCode.OK");
            Assert.That(response.Content.Status, Is.EqualTo(CommandStatus.Ack), "response.Content.Status, Is.EqualTo(CommandStatus.Ack)");
        }
        
        [Test(Description = "Return Position (0,0,0) when locking handler position")]
        [AllureSuite("Posetive")]
        [AllureSubSuite("Lock")]
        [AllureTag("Position", "Lock")]
        [AllureFeature("Lock")]
        [AllureSeverity(SeverityLevel.normal)]
        public async Task ReturnPositionZeroOnLockHandlerPosition()
        {
            // Arrange + Act
            var response = await HandlerApi.LockHandlerPosition();

            //Assert
            Assert.That(response.Content.position.X, Is.EqualTo(0), "X, Is.EqualTo(0)");
            Assert.That(response.Content.position.Y, Is.EqualTo(0), "Y, Is.EqualTo(0)");
            Assert.That(response.Content.position.Z, Is.EqualTo(0), "Z, Is.EqualTo(0)");
        }

        
        [Test(Description = "Return any PositionId when locking handler position")]
        [AllureSuite("Posetive")]
        [AllureSubSuite("Lock")]
        [AllureTag("Posetive", "Lock")]
        [AllureFeature("Lock")]
        [AllureSeverity(SeverityLevel.critical)]
        public async Task ReturnPositionIdNullOnLockHandlerPosition()
        {
            // Arrange + Act
            var response = await HandlerApi.LockHandlerPosition();

            //Assert
            Assert.That(response.Content.position.PositionId, Is.Not.Null);
        }

        
        [Test(Description = "Return any PositionId when locking handler position")]
        [AllureSuite("Posetive")]
        [AllureSubSuite("UnLock")]
        [AllureTag("Posetive", "UnLock")]
        [AllureIssue("Issue#2")]
        [AllureFeature("UnLock")]
        [AllureSeverity(SeverityLevel.critical)]
        public async Task ReturnOKOnUnLockHandlerPosition()
        {
            // Arrange + Act
            var response = await HandlerApi.UnlockHandlerPosition();

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "response.StatusCode, Is.EqualTo(HttpStatusCode.OK)");
        }
    }
}