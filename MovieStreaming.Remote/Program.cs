using Akka.Actor;
using System.Drawing;
using System.IO;
using Akka.Configuration;
using Console = Colorful.Console;

namespace MovieStreaming.Remote
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;

        static void Main(string[] args)
        {
            Console.WriteLine("Creating MovieStreamingActorSystem in remote process", Color.Gray);

            var config = ConfigurationFactory.ParseString(File.ReadAllText("settings.hocon"));

            using (MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem", config))
            {
                Console.ReadKey();
                MovieStreamingActorSystem.Terminate();
                Console.WriteLine("Actor System terminating", Color.Gray);
                MovieStreamingActorSystem.WhenTerminated.Wait();
                Console.WriteLine("Actor System terminated", Color.Gray);
            }
        }
    }
}
