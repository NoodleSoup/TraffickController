using Microsoft.AspNetCore.Http;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using TraffickController.JsonStrings;
using TraffickController.TrafficLight;

namespace TraffickController.WebSocketConnection
{
    public class Send
    {
        #region SendState
        public static async Task SendState(HttpContext context, WebSocket webSocket)
        {            
            string jsonTrafficLight = Data.GetNewTrafficLight(); // Start setting up the JSON string builder TODO: Make it send dynamic states

            if (String.IsNullOrEmpty(jsonTrafficLight))
                jsonTrafficLight = JsonStringBuilder.BuildJsonString();

            var jsonBytes = Encoding.UTF8.GetBytes(jsonTrafficLight); // Convert the JSON object to bytes to send over WebSocket connection

            await webSocket.SendAsync(new ArraySegment<byte>(jsonBytes, 0, jsonBytes.Length), 0, true, CancellationToken.None);
        }
        #endregion

        #region SendStartState
        public static async Task<bool> SendStartState(HttpContext context, WebSocket webSocket)
        {
            var jsonTrafficLight = JsonStringBuilder.BuildJsonString();
            var jsonBytes = Encoding.UTF8.GetBytes(jsonTrafficLight);

            await webSocket.SendAsync(new ArraySegment<byte>(jsonBytes, 0, jsonBytes.Length), 0, true, CancellationToken.None);
            return false;
        }
        #endregion
    }
}
