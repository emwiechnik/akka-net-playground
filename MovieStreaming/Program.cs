using System;
using Akka.Actor;
using MovieStreaming.Actors;
using MovieStreaming.Messages;

namespace MovieStreaming
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;

        static void Main(string[] args)
        {

            using (MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem"))
            {
                Console.WriteLine("Actor System created");

                Props playbackActorProps = Props.Create<UserActor>();

                IActorRef playbackActorRef = MovieStreamingActorSystem.ActorOf(playbackActorProps, "OurUserActor");

                Console.ReadLine();

                Console.WriteLine("Sending a PlayMovieMessage (Conan the Destroyer)");
                playbackActorRef.Tell(new PlayMovieMessage("Conan the Destroyer", 123));

                Console.ReadLine();

                Console.WriteLine("Sending another PlayMovieMessage (Boolean Lies)");
                playbackActorRef.Tell(new PlayMovieMessage("Boolean Lies", 77));

                Console.ReadLine();

                Console.WriteLine("Sending a StopMovieMessage");
                playbackActorRef.Tell(new StopMovieMessage());

                Console.ReadLine();

                Console.WriteLine("Sending another StopMovieMessage");
                playbackActorRef.Tell(new StopMovieMessage());

                Console.ReadLine();

                MovieStreamingActorSystem.Terminate();
                Console.WriteLine("Actor System terminating");
                MovieStreamingActorSystem.WhenTerminated.Wait();
                Console.WriteLine("Actor System terminated");

                Console.ReadLine();
            }
        }
    }
}
