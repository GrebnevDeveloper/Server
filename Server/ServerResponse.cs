using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{

    class ServerResponse
    {
        HttpListener listener;
        string host;
        string port;

        public ServerResponse(string host, string port)
        {
            this.host = host;
            this.port = port;
            listener = new HttpListener();
            listener.Prefixes.Add($"http://{host}:{port}/ping/");
            listener.Prefixes.Add($"http://{host}:{port}/stop/");
            listener.Prefixes.Add($"http://{host}:{port}/getanswer/");
            listener.Prefixes.Add($"http://{host}:{port}/postinputdata/");
            listener.Start();
            Console.WriteLine("Сервер работает...");
        }

        public HttpListener GetHttpListener()
        {
            return listener;
        }
        public Input PostData(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            if(request.HasEntityBody)
            {
                var sr = new StreamReader(request.InputStream);
                var source = Encoding.UTF8.GetBytes(sr.ReadToEnd());
                return Serializer.Deserialize(source);
            }
            return null;
        }
        public void GetAnswer(HttpListenerContext context, Output output)
        {
            HttpListenerResponse response = context.Response;
            if(output != null)
            {
                var sw = new StreamWriter(response.OutputStream);
                sw.Write(Encoding.UTF8.GetString(Serializer.Serialize(output)));
            }
            response.Close();
        }
        public HttpStatusCode GetPing(HttpListenerContext context)
        {
            if (listener.IsListening)
            {
                HttpListenerResponse response = context.Response;
                var sw = new StreamWriter(response.OutputStream);
                sw.Write(HttpStatusCode.OK);
                response.Close();
                return HttpStatusCode.OK;
            }
            else
            {
                return HttpStatusCode.Conflict;
            }
        }
        public void StopServer()
        {
            listener.Stop();
            Console.WriteLine("Сервер отключён...");
        }
    }
}
