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
        private static string _jsonTrafficLight = null;

        #region SendSocket
        private static async Task SendSocket(WebSocket webSocket, string state = "Green")
        {
            _jsonTrafficLight = Data.GetNewTrafficLight(state); // Start setting up the JSON string builder JsonStringBuilder.BuildJsonString();

            // If the trafficlight string is empty for some reason send the default response (start state)
            if (String.IsNullOrEmpty(_jsonTrafficLight))
                _jsonTrafficLight = JsonStringBuilder.BuildJsonString();

            var jsonBytes = Encoding.UTF8.GetBytes(_jsonTrafficLight); // Convert the JSON object to bytes to send over WebSocket connection

            await webSocket.SendAsync(new ArraySegment<byte>(jsonBytes, 0, jsonBytes.Length), 0, true, CancellationToken.None);
        }
        #endregion

        #region SendState
        public static async Task SendState(HttpContext context, WebSocket webSocket)
        {
            try
            {
                await SendSocket(webSocket);
                Thread.Sleep(6000);

                await SendSocket(webSocket, "Orange");
                Thread.Sleep(3500);

                await SendSocket(webSocket, "Red");
                Thread.Sleep(6000);
            }
            catch(WebSocketException we)
            {
                System.Diagnostics.Debug.WriteLine($"Exception: {we.Message} in {we.Source} at {DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")}");
                return;
            }
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
