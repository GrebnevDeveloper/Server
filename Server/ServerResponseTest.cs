using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    //todo: тесты на сервер надо писать с помощью клиента (check)
    [TestFixture]
    class ServerResponseTest
    {
        public ServerResponse serverResponse;

        [SetUp]
        public void Setup()
        {
            serverResponse = new ServerResponse("127.0.0.1", "8080");
        }
        [Test]
        public void Stop()
        {
            serverResponse.StopServer();
            //todo: проверка что тру равно нефолс? может по-русски? (check)
            Assert.IsFalse(serverResponse.GetHttpListener().IsListening);
        }
    }
}
