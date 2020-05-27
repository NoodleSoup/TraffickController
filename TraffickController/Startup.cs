using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;

using TraffickController.WebSocketConnection;
using TraffickController.Tests;

namespace TraffickController
{
    public class Startup
    {
        private string _pathUrl = Program.getPathUrl();
        private byte[] _buffer = new byte[1024 * 6];
        private WebSocket _webSocket;
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.AddConsole()
                    .AddDebug()
                    .AddFilter<ConsoleLoggerProvider>(category: null, level: LogLevel.Debug)
                    .AddFilter<DebugLoggerProvider>(category: null, level: LogLevel.Debug);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var receive = true;
            var sendStartState = true;
            Task receiveTask = null;
            Task sendTask = null;
            bool connected = false;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var webSocketOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromSeconds(300),
                ReceiveBufferSize = 6 * 1024,
            };

            app.UseWebSockets(webSocketOptions);

            #region AcceptWebSocket
            app.Use(async (context, next) =>
            {
                bool firstTime = true;

                if (!connected)
                {
                    _webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    connected = true;
                }

                do
                {
                    if (sendStartState) // Send the start state once when there's a connection
                    {
                        //await ServerTest.Test(buffer);
                        sendStartState = await Send.SendStartState(context, _webSocket);
                    }

                    if (sendTask == null || sendTask.IsCompleted)
                    {
                        sendTask = Task.Run(async () => await Send.SendState(context, _webSocket));
                    }

                    if(receiveTask == null || receiveTask.IsCompleted)
                    {
                        receiveTask = Task.Run(async () => receive = await Receive.ReceiveSocket(context, _webSocket, _buffer));
                        if (firstTime)
                        {
                            Thread.Sleep(50);
                            firstTime = false;
                        }
                    }

                    if (_webSocket.State == WebSocketState.Closed)
                    {
                        await _webSocket.CloseAsync(new WebSocketCloseStatus(), "Client disconnected.", new CancellationToken());
                        sendStartState = true;
                        connected = false;
                        _buffer = new byte[1024 * 6];
                        break;
                    }
                } while (receive);

                sendTask.Dispose();
                receiveTask.Dispose();
                sendStartState = true;
                connected = false;
                _buffer = new byte[1024 * 6];

            });
            #endregion
        }

        private async Task<bool> TestReceiveAsync(Microsoft.AspNetCore.Http.HttpContext context, WebSocket webSocket)
        {
            bool receive = await Receive.ReceiveSocket(context, webSocket, _buffer);
            return receive;
        }
    }
}
