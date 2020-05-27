using CommandLine;
using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace TraffickController
{
    public class Program
    {
        public static string pathUrl;
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No arguments passed");
            }
            else
            {
                try
                {
                    // Parse the command line variables.
                    Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o =>
                    {
                        setPathUrl(o.path.ToString());
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception in file: {e.Source} Message: {e.Message}");
                }
            }
            CreateWebHostBuilder(args).Build().Run();
        }

        public static string setPathUrl(string path)
        {
            pathUrl = path;
            return "";
        }

        public static string getPathUrl()
        {
            if(!String.IsNullOrEmpty(pathUrl))
                return pathUrl;
            return "";
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
              .UseStartup<Startup>();
    }

    internal class Options
    {
        [Option('p', "path", Required = true, HelpText = "Set path for connection.")]
        public string path { get; set; }
    }
}