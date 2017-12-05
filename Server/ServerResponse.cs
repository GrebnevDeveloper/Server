using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    //todo: убрать из параметров метода этого класса context (check)
    class ServerResponse
    {
        HttpListener listener;
        HttpListenerContext context;
        Serializer serializer;
        string host;
        string port;

        public ServerResponse(string host, string port)
        {
            this.host = host;
            this.port = port;
            listener = new HttpListener();
            context = null;
            serializer = new Serializer();
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
        public void SetContext(HttpListenerContext context)
        {
            this.context = context;
        }
        public HttpListenerContext GetContext()
        {
            return context;
        }
        public T PostData<T>()
        {
            HttpListenerRequest request = context.Request;
            if(request.HasEntityBody)
            {
                using (var sr = new StreamReader(request.InputStream))
                {
                    var source = Encoding.UTF8.GetBytes(sr.ReadToEnd());
                    Console.WriteLine("Запрос получен");
                    return serializer.Deserialize<T>(source);
                }
            }
            return default(T);
        }
        public void GetAnswer<T>(T obj)
        {
            HttpListenerResponse response = context.Response;
            if(obj != null)
            {
                using (var sw = new StreamWriter(response.OutputStream))
                {
                    sw.Write(Encoding.UTF8.GetString(serializer.Serialize<T>(obj)));
                    Console.WriteLine("Ответ отправлен");
                }
            }
            response.Close();
        }
        public HttpStatusCode GetPing()
        {
            if (listener.IsListening)
            {
                HttpListenerResponse response = context.Response;
                using (var sw = new StreamWriter(response.OutputStream))
                {
                    sw.Write(HttpStatusCode.OK);
                    response.Close();
                    return HttpStatusCode.OK;
                }

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
