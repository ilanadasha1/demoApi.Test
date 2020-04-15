using Allure.Commons;
using demoApi.Test.SDK;
using demoApi.Test.SDK.Logger;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using SimpleInjector;
using System;
using System.IO;

namespace demoApi.Test.Bootstrap
{
    public abstract class TestBase 
    {
        protected Container Container => BootStrap.Container;
        protected IHandlerApiSDK HandlerApi;
        protected ILogHandler logger;

        [OneTimeSetUp]
        public void Init()
        {
            logger = Container.GetInstance<ILogHandler>();
            HandlerApi = Container.GetInstance<IHandlerApiSDK>();

            Environment.CurrentDirectory = Path.GetDirectoryName(GetType().Assembly.Location);
            Environment.SetEnvironmentVariable("ALLURE_CONFIG_ENV_VARIABLE",
                Path.Combine(Environment.CurrentDirectory, AllureConstants.CONFIG_FILENAME));
            AllureLifecycle.Instance.CleanupResultDirectory();
            var config = AllureLifecycle.Instance.JsonConfiguration;
        }

        [SetUp]
        public void Setup()
        {
            logger.Info("\n\n");
            logger.Info($"Test {TestContext.CurrentContext.Test.Name} Start");

        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            if (HandlerApi != null)
                HandlerApi = null;

            logger.Info($"Pass: {TestContext.CurrentContext.Result.PassCount}");
            logger.Info($"Fail: {TestContext.CurrentContext.Result.FailCount}");
            logger.Info($"InconclusiveCount: {TestContext.CurrentContext.Result.InconclusiveCount}");
            logger.Info($"SkipCount: {TestContext.CurrentContext.Result.SkipCount}");
        }

        [TearDown]
        public void TearDown()
        {
            var CurrentTest = TestContext.CurrentContext.Test.Name;
            var CurrentStatus = TestContext.CurrentContext.Result.Outcome.Status;
            
            if (CurrentStatus == TestStatus.Passed )
            {
                logger.Info($"Test: {CurrentTest} - {CurrentStatus}");
                logger.Info(TestContext.CurrentContext.Result.Message);
            }
            else
            {
                logger.Error($"Test: {CurrentTest} - {CurrentStatus}");
                logger.Error(TestContext.CurrentContext.Result.Message);
            }
               
            logger.Info($"Test END");
        }
    }
}
