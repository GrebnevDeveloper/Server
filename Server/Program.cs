using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Input input = null;
            string host = "127.0.0.1";
            string port = Console.ReadLine();
            ServerResponse server = new ServerResponse(host, port);
            while (server.GetHttpListener().IsListening)
            {
                HttpListenerContext context = server.GetHttpListener().GetContext();
                switch (context.Request.RawUrl)
                {
                    case "/ping/":
                        if (server.GetPing(context) == HttpStatusCode.OK)
                        {
                            Console.WriteLine("Сервер доступен...");
                        }
                        else
                        {
                            Console.WriteLine("Сервер не доступен...");
                        }
                        break;
                    case "/postinpuntdata/":
                        input = server.PostData(context);
                        break;
                    case "/getanswer/":
                        if(input != null)
                        {
                            server.GetAnswer(context, new Output(input));
                        }
                        break;
                    case "/stop/":
                        server.StopServer();
                        break;
                } 
            }
        }
    }
}
