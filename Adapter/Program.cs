using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductManager productManager = new ProductManager(new Log4NetAdapter()); //2sini de çağırabiliyoruz.
            productManager.Save();
            Console.ReadLine();
        }
    }

    class ProductManager
    {
        private ILogger _logger;

        public ProductManager(ILogger logger)
        {
            _logger = logger;
        }

        public void Save()
        {
            _logger.Log("User Data");
            Console.WriteLine("Saved!");
        }
    }

    interface ILogger
    {
        void Log(string message);
    }

    class OngLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine("Logged!, {0}", message); ;
        }
    }

    //Framework varsayalım, yani içeriğini değiştiremiyoruz
    class Log4Net
    {
        public void LogMessage(string message)
        {
            Console.WriteLine("Logged with log4net, {0}", message);
        }
    }

    //Framework'e Adapter ekliyip onu da çağırabiliyoruz.
    class Log4NetAdapter : ILogger
    {
        public void Log(string message)
        {
            Log4Net log4Net = new Log4Net();
            log4Net.LogMessage(message);
        }
    }
}
