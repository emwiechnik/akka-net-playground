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

            Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message));
            Receive<StopMovieMessage>(message => HandleStopMovieMessage(message));
        }

        private void HandleStopMovieMessage(StopMovieMessage message)
        {
            if (_currentlyWatching == null)
            {
                Console.WriteLine("Error: cannot stop, because nothing is being played");
            }
            else
            {
                StopPlayingCurrentMovie();
            }

        }

        private void StopPlayingCurrentMovie()
        {
            Console.WriteLine($"User has stopped watching {_currentlyWatching}");

            _currentlyWatching = null;
        }

        private void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            if (_currentlyWatching != null)
            {
                Console.WriteLine("Error: cannot start playing another movie before stopping existing one");
            }
            else
            {
                StartPlayingMovie(message.MovieTitle);
            }
        }

        private void StartPlayingMovie(string movieTitle)
        {
            _currentlyWatching = movieTitle;
            Console.WriteLine($"User is currently watching {_currentlyWatching}");
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