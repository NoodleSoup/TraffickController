using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

using TraffickController.TrafficLight;

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

            if (ValidateJson.Validate(rcvMsg))
            {
                var jsonObject = JsonConvert.DeserializeObject<TrafficLightObject>(rcvMsg);
                Data.SetTrafficData(jsonObject);
            }

            if (result.CloseStatus.HasValue)
                return false;
            
            return true;
        }
        #endregion
    }
}
