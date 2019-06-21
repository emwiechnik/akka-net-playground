using Akka.Actor;
using System;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    public class UserActor: ReceiveActor
    {
        public string _currentlyWatching;

        public UserActor()
        {
            Console.WriteLine($"Creating a {GetType().Name}");

            Console.WriteLine("Setting initial behaviour to Stopped");
            Stopped();
        }

        private void Playing()
        {
            Receive<StopMovieMessage>(message => StopPlayingCurrentMovie());
            Receive<PlayMovieMessage>(message => Console.WriteLine("Error: cannot start playing another movie before stopping existing one"));

            Console.WriteLine($"{GetType().Name} has now become Playing");
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(message => StartPlayingMovie(message.MovieTitle));
            Receive<StopMovieMessage>(message => Console.WriteLine("Error: cannot stop, because nothing is being played"));

            Console.WriteLine($"{GetType().Name} has now become Stopped");
        }

        private void StopPlayingCurrentMovie()
        {
            Console.WriteLine($"User has stopped watching {_currentlyWatching}");

            _currentlyWatching = null;

            Become(Stopped);
        }

        private void StartPlayingMovie(string movieTitle)
        {
            _currentlyWatching = movieTitle;
            Console.WriteLine($"User is currently watching {_currentlyWatching}");

            Become(Playing);
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