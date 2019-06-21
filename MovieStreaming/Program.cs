using System;
using Akka.Actor;
using Akka.Actor.Internal;
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

                Props playbackActorProps = Props.Create<PlaybackActor>();

                IActorRef playbackActorRef = MovieStreamingActorSystem.ActorOf(playbackActorProps, "OurPlaybackActor");

                playbackActorRef.Tell(new PlayMovieMessage("Akka.NET: The Movie", 12));
                playbackActorRef.Tell(new PlayMovieMessage("Partial Recall", 15));
                playbackActorRef.Tell(new PlayMovieMessage("Boolean Lies", 77));
                playbackActorRef.Tell(new PlayMovieMessage("Conan the Destroyer", 123));

                playbackActorRef.Tell(PoisonPill.Instance);

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
