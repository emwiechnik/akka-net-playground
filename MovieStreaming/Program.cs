using System;
using System.Drawing;
using Akka.Actor;
using MovieStreaming.Actors;
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
            using (MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem"))
            {
                Console.WriteLine("Actor System created");

                Console.WriteLine("Creating actor supervisory hierarchy", Color.Gray);
                Props playbackActorProps = Props.Create<PlaybackActor>();

                MovieStreamingActorSystem.ActorOf(playbackActorProps, "PlaybackActor");

                Console.ReadLine();

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
                        Environment.Exit(1);
                    }
                } while (true);
            }
        }
    }
}
