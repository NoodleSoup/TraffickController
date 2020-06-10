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
    public class Send : ISend
    {
        private string _jsonTrafficLight = null;

        private readonly IData _data;
        private readonly IJsonStringBuilder _jsonStringBuilder;

        public Send(IData data, IJsonStringBuilder jsonStringBuilder) =>
            (_data, _jsonStringBuilder) = (data, jsonStringBuilder);

        private async Task SendSocket(WebSocket webSocket, string state = "Green")
        {
            _jsonTrafficLight = _data.GetNewTrafficLight(state); // Start setting up the JSON string builder

            // If the trafficlight string is empty for some reason send the default response (start state)
            if (String.IsNullOrEmpty(_jsonTrafficLight))
                _jsonTrafficLight = _jsonStringBuilder.BuildJsonString();

            var jsonBytes = Encoding.UTF8.GetBytes(_jsonTrafficLight); // Convert the JSON object to bytes to send over WebSocket connection

            await webSocket.SendAsync(new ArraySegment<byte>(jsonBytes, 0, jsonBytes.Length), 0, true, CancellationToken.None);
        }
        
        public async Task SendState(HttpContext context, WebSocket webSocket)
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

        public async Task<bool> SendStartState(HttpContext context, WebSocket webSocket)
        {
            var jsonTrafficLight = _jsonStringBuilder.BuildJsonString();
            var jsonBytes = Encoding.UTF8.GetBytes(jsonTrafficLight);

            await webSocket.SendAsync(new ArraySegment<byte>(jsonBytes, 0, jsonBytes.Length), 0, true, CancellationToken.None);
            return false;
        }
    }
}
