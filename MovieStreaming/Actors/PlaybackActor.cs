using System;
using Akka.Actor;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            Console.WriteLine($"Creating {GetType().Name}");

            Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message), message => message.UserId >= 12);
        }

        private void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            Console.WriteLine($"Received movie title '{message.MovieTitle}' from user id {message.UserId}");
        }

        protected override void PreStart()
        {
            Console.WriteLine($"{GetType().Name}: PreStart");
            base.PreStart();
        }

        protected override void PostStop()
        {
            Console.WriteLine($"{GetType().Name}: PostStop");
            base.PostStop();
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine($"{GetType().Name}: PreRestart, because: " + reason.Message);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            Console.WriteLine($"{GetType().Name}: PostRestart, because: " + reason.Message);
            base.PostRestart(reason);
        }
    }
}
