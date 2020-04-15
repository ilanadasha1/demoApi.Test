using log4net.Core;
using System;

namespace demoApi.Test.SDK.Logger
{
    public interface ILogHandler 
    {
        void Info(string message, object obj = null);
        //void Info(object obj = null);
        //void Error(object obj = null);
        void Error(string message, object obj = null);

    }
}
