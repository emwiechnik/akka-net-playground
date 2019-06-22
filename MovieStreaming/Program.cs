using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Configuration;
using MovieStreaming.Common.Actors;
using MovieStreaming.Common.Messages;
using Console = Colorful.Console;

namespace MovieStreaming
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;

        static void Main(string[] args)
        {
            Console.WriteLine("Creating MovieStreamingActorSystem", Color.Gray);

            var config = ConfigurationFactory.ParseString(File.ReadAllText("settings.hocon"));

            using (MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorLocalSystem", config))
            {
                Console.WriteLine("Actor System created");

                Console.WriteLine("Creating actor supervisory hierarchy", Color.Gray);
                Props playbackActorProps = Props.Create<PlaybackActor>();

                MovieStreamingActorSystem.ActorOf(playbackActorProps, "PlaybackActor");

                Task.Delay(TimeSpan.FromSeconds(1)).Wait(); // so that the messages from the system come before the user prompt is displayed

                do
                {
                    Console.WriteLine();
                    Console.WriteLine("Enter a command and hit enter", Color.DarkGray);

                    var command = Console.ReadLine();

                    if (command.StartsWith("play"))
                    {
                        var userId = int.Parse(command.Split(',')[1]);
                        var movieTitle = command.Split(',')[2];

                        var message = new PlayMovieMessage(movieTitle, userId);
                        MovieStreamingActorSystem.ActorSelection("/user/PlaybackActor/UserCoordinatorActor").Tell(message);
                    }

                    if (command.StartsWith("stop"))
                    {
                        var userId = int.Parse(command.Split(',')[1]);

                        var message = new StopMovieMessage(userId);
                        MovieStreamingActorSystem.ActorSelection("/user/PlaybackActor/UserCoordinatorActor").Tell(message);
                    }

                    if (command.StartsWith("exit"))
                    {
                        MovieStreamingActorSystem.Terminate();
                        Console.WriteLine("Actor System terminating", Color.Gray);
                        MovieStreamingActorSystem.WhenTerminated.Wait();
                        Console.WriteLine("Actor System terminated", Color.Gray);
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                } while (true);
            }
        }
    }
}
