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
            Console.WriteLine("{0}", rcvMsg);

            var jsonObject = JsonConvert.DeserializeObject<TrafficLightObject>(rcvMsg);
            Console.WriteLine(jsonObject.A1);

            while (!result.CloseStatus.HasValue)
            {
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                Console.WriteLine(result.Count);
            }

            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            return false;
        }
        #endregion
    }
}
