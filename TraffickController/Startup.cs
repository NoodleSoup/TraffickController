using System;
using System.Linq;
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
        private string pathUrl = Program.getPathUrl();
        private byte[] buffer = new byte[1024 * 4];
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

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var webSocketOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromSeconds(120),
                ReceiveBufferSize = 4 * 1024,
            };

            webSocketOptions.AllowedOrigins.Any(); // Set app to accept all header formats

            app.UseWebSockets(webSocketOptions);

            #region AcceptWebSocket
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == pathUrl)
                {
                    if (context.WebSockets.IsWebSocketRequest) // Check if the recieve command didn't send 'close' as a disconnect notice
                    {
                        WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();

                        do
                        {
                            if (sendStartState) // Send the start state once when there's a connection
                            {

                                //await ServerTest.Test(buffer);
                                sendStartState = await Send.SendStartState(context, webSocket);
                            }

                            await Send.SendState(context, webSocket); // Needs some smart stuff to do the traffic lights logic
                            receive = await Receive.ReceiveSocket(context, webSocket, buffer);
                        } while (receive);
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }
                else
                {
                    await next();
                }

            });
            #endregion
        }
    }
}
