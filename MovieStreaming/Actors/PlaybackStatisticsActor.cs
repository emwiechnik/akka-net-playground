using Akka.Actor;
using System;
using System.Drawing;
using Console = Colorful.Console;

namespace MovieStreaming.Actors
{
    public class PlaybackStatisticsActor: ReceiveActor
    {
        public PlaybackStatisticsActor()
        {
            Context.ActorOf<MoviePlayCounterActor>("MoviePlayCounterActor");
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