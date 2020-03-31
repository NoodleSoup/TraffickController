using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

using TraffickController.JsonStrings;

namespace TraffickController.WebSocketConnection
{
    public class Receive
    {
        #region Receive
        public static async Task<bool> ReceiveSocket(HttpContext context, WebSocket webSocket, byte[] buffer)
        {
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            byte[] msgBytes = buffer.Take(result.Count).ToArray();
            string rcvMsg = Encoding.UTF8.GetString(msgBytes);

            var jsonObject = JsonConvert.DeserializeObject<TrafficLightObject>(rcvMsg);
            Console.WriteLine(jsonObject.A1);

            Console.WriteLine(result.CloseStatus.HasValue);
            if (result.CloseStatus.HasValue)
                return false;
            
            return true;
        }
        #endregion
    }
}
