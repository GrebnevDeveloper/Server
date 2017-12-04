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
                server.SetContext(context);
                switch (context.Request.RawUrl)
                {
                    case "/ping/":
                        if (server.GetPing() == HttpStatusCode.OK)
                        {
                            Console.WriteLine("Сервер доступен...");
                        }
                        else
                        {
                            Console.WriteLine("Сервер не доступен...");
                        }
                        break;
                    case "/postinputdata/":
                        input = server.PostData();
                        break;
                    case "/getanswer/":
                        if(input != null)
                        {
                            server.GetAnswer(new Output(input));
                        }
                        else
                        {
                            server.GetAnswer(null);
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
