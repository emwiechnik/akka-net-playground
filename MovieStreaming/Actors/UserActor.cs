using Akka.Actor;
using System;
using System.Drawing;
using MovieStreaming.Messages;
using Console = Colorful.Console;

namespace MovieStreaming.Actors
{
    public class UserActor: ReceiveActor
    {
        public string _currentlyWatching;

        public UserActor()
        {
            Console.WriteLine($"Creating a {GetType().Name}", Color.Orange);

            Console.WriteLine("Setting initial behaviour to Stopped",Color.Orange);
            Stopped();
        }

        private void Playing()
        {
            Receive<StopMovieMessage>(message => StopPlayingCurrentMovie());
            Receive<PlayMovieMessage>(message => Console.WriteLine("Error: cannot start playing another movie before stopping existing one", Color.Red));

            Console.WriteLine($"{GetType().Name} has now become Playing", Color.Orange);
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(message => StartPlayingMovie(message.MovieTitle));
            Receive<StopMovieMessage>(message => Console.WriteLine("Error: cannot stop, because nothing is being played", Color.Red));

            Console.WriteLine($"{GetType().Name} has now become Stopped", Color.Orange);
        }

        private void StopPlayingCurrentMovie()
        {
            Console.WriteLine($"User has stopped watching {_currentlyWatching}", Color.Green);

            _currentlyWatching = null;

            Become(Stopped);
        }

        private void StartPlayingMovie(string movieTitle)
        {
            _currentlyWatching = movieTitle;
            Console.WriteLine($"User is currently watching {_currentlyWatching}", Color.Green);

            Become(Playing);
        }

        protected override void PreStart()
        {
            Console.WriteLine($"{GetType().Name}: PreStart", Color.Orange);
            base.PreStart();
        }

        protected override void PostStop()
        {
            Console.WriteLine($"{GetType().Name}: PostStop", Color.Orange);
            base.PostStop();
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine($"{GetType().Name}: PreRestart, because: {reason.Message}", Color.Orange);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            Console.WriteLine($"{GetType().Name}: PostRestart, because: {reason.Message}" , Color.Orange);
            base.PostRestart(reason);
        }
    }
}