using Akka.Actor;
using System.Drawing;
using Console = Colorful.Console;

namespace MovieStreaming.Remote
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;

        static void Main(string[] args)
        {
            Console.WriteLine("Creating MovieStreamingActorSystem in remote process", Color.Gray);

            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");

            MovieStreamingActorSystem.WhenTerminated.Wait();
        }
    }
}
