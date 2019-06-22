using Akka.Actor;
using System;
using System.Drawing;
using Console = Colorful.Console;
using MovieStreaming.Common.Messages;

namespace MovieStreaming.Common.Actors
{
    public class UserActor: ReceiveActor
    {
        public string _currentlyWatching;

        public UserActor()
        {
            Console.WriteLine($"Creating a {GetType().Name} named: {this.Self.Path.Name}", Color.Orange);

            Console.WriteLine("Setting initial behaviour to Stopped", Color.Orange);
            Stopped();
        }

        private void Playing()
        {
            Receive<StopMovieMessage>(message => StopPlayingCurrentMovie());
            Receive<PlayMovieMessage>(message => Console.WriteLine($"{this.Self.Path.Name} Error: cannot start playing another movie before stopping existing one", Color.Red));

            Console.WriteLine($"{this.Self.Path.Name} has now become Playing", Color.Orange);
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(message => StartPlayingMovie(message.MovieTitle));
            Receive<StopMovieMessage>(message => Console.WriteLine($"{this.Self.Path.Name} Error: cannot stop, because nothing is being played", Color.Red));

            Console.WriteLine($"{this.Self.Path.Name} has now become Stopped", Color.Orange);
        }

        private void StopPlayingCurrentMovie()
        {
            Console.WriteLine($"{this.Self.Path.Name} has stopped watching {_currentlyWatching}", Color.Green);

            _currentlyWatching = null;

            Become(Stopped);
        }

        private void StartPlayingMovie(string movieTitle)
        {
            _currentlyWatching = movieTitle;
            Console.WriteLine($"{this.Self.Path.Name} is currently watching {_currentlyWatching}", Color.Green);

            Context.ActorSelection("/user/PlaybackActor/PlaybackStatisticsActor/MoviePlayCounterActor").Tell(new IncrementPlayCountMessage(movieTitle));
            Become(Playing);
        }

        protected override void PreStart()
        {
            Console.WriteLine($"{this.Self.Path.Name}: PreStart", Color.Orange);
            base.PreStart();
        }

        protected override void PostStop()
        {
            Console.WriteLine($"{this.Self.Path.Name}: PostStop", Color.Orange);
            base.PostStop();
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine($"{this.Self.Path.Name}: PreRestart, because: {reason.Message}", Color.Orange);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            Console.WriteLine($"{this.Self.Path.Name}: PostRestart, because: {reason.Message}" , Color.Orange);
            base.PostRestart(reason);
        }
    }
}