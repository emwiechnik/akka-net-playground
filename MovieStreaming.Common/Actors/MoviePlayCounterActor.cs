using System;
using System.Collections.Generic;
using System.Drawing;
using Akka.Actor;
using MovieStreaming.Common.Exceptions;
using MovieStreaming.Common.Messages;
using Console = Colorful.Console;

namespace MovieStreaming.Common.Actors
{
    public class MoviePlayCounterActor : ReceiveActor
    {
        private readonly Dictionary<string, int> _moviePlayCounts;

        public MoviePlayCounterActor()
        {
            _moviePlayCounts = new Dictionary<string, int>();

            Receive<IncrementPlayCountMessage>(message =>
            {
                if (!_moviePlayCounts.ContainsKey(message.MovieTitle))
                {
                    _moviePlayCounts.Add(message.MovieTitle, 0);
                }

                _moviePlayCounts[message.MovieTitle] += 1;

                // simulated bugs
                if (_moviePlayCounts[message.MovieTitle] > 3)
                {
                    throw new SimulatedCurruptStateException();
                }

                if (message.MovieTitle == "Partial Recoil")
                {
                    throw new SimulatedTerribleMovieException();
                }


                Console.WriteLine($"{GetType().Name}: The movie '{message.MovieTitle}' has been watched {_moviePlayCounts[message.MovieTitle]} times", Color.Magenta);
            });
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
            Console.WriteLine($"{GetType().Name}: PostRestart, because: {reason.Message}", Color.Orange);
            base.PostRestart(reason);
        }
    }
}