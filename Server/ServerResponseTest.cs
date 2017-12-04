using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    //todo: тесты на сервер надо писать с помощью клиента
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
        public void Ping()
        {
            HttpListenerContext context = serverResponse.GetHttpListener().GetContext();
            Assert.Equals(serverResponse.GetPing(context), HttpStatusCode.OK);
        }
        [Test]
        public void Stop()
        {
            serverResponse.StopServer();
            //todo: проверка что тру равно нефолс? может по-русски?
            Assert.IsTrue(!serverResponse.GetHttpListener().IsListening);
        }
        [Test]
        public void GetAndPost()
        {
            HttpListenerContext context = serverResponse.GetHttpListener().GetContext();
            Input input = serverResponse.PostData(context);
            Assert.NotNull(input);
            Assert.IsInstanceOf(typeof(Input), input);
            serverResponse.GetAnswer(context, new Output(input));
        }
    }
}
