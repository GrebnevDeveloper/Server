using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [TestFixture]
    class ServerResponseTest
    {
        public ServerResponse serverResponse;

        [SetUp]
        public void Setup()
        {
            serverResponse = new ServerResponse("127.0.0.1", "8080");
//            context = serverResponse.GetHttpListener().GetContext();
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
