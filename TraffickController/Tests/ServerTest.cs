using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using TraffickController.JsonStrings;

namespace TraffickController.Tests
{
    public class ServerTest
    {
        #region Test
        public static async Task Test(byte[] buffer)
        {
            ClientWebSocket cWebSocket = new ClientWebSocket();
            var uriLink = new Uri("ws://trafic.azurewebsites.net/controller");
            try
            {
                await cWebSocket.ConnectAsync(uriLink, CancellationToken.None);
                Console.WriteLine("Connected");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            while (cWebSocket.State == WebSocketState.Open)
            {
                await ServerTest.SendStateClient(cWebSocket, buffer);
            }
        }
        #endregion

        #region SendStateClient
        private static async Task SendStateClient(ClientWebSocket cWebSocket, byte[] buffer)
        {
            var jsonTrafficLight = JsonStringBuilder.BuildJsonString(); // Start setting up the JSON string builder TODO: Make it send dynamic states
            var jsonBytes = Encoding.UTF8.GetBytes(jsonTrafficLight); // Convert the JSON object to bytes to send over WebSocket connection

            await cWebSocket.SendAsync(new ArraySegment<byte>(jsonBytes, 0, jsonBytes.Length), WebSocketMessageType.Text, true, CancellationToken.None);

            WebSocketReceiveResult result = await cWebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            byte[] msgBytes = buffer.Take(result.Count).ToArray();
            string rcvMsg = Encoding.UTF8.GetString(msgBytes);
            Console.WriteLine("{0}", rcvMsg);

            while (!result.CloseStatus.HasValue)
            {
                result = await cWebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                Console.WriteLine(result.Count);
            }
            await cWebSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);

        }
        #endregion
    }
}
