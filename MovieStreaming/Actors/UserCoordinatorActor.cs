using Akka.Actor;
using MovieStreaming.Common.Messages;
using System;
using System.Collections.Generic;
using System.Drawing;
using Console = Colorful.Console;

namespace MovieStreaming.Actors
{
    public class UserCoordinatorActor: ReceiveActor
    {
        private readonly Dictionary<int, IActorRef> _users;

        public UserCoordinatorActor()
        {
            _users = new Dictionary<int, IActorRef>();

            Receive((Action<PlayMovieMessage>)(message =>
            {
                CreateUserActorIfNotExists(message.UserId);
                _users[message.UserId].Tell(message);
            }));

            Receive<StopMovieMessage>(message =>
            {
                CreateUserActorIfNotExists(message.UserId);
                _users[message.UserId].Tell(message);
            });
        }

        private void CreateUserActorIfNotExists(int userId)
        {
            if (!_users.ContainsKey(userId))
            {
                _users.Add(userId, Context.ActorOf<UserActor>($"UserActor{userId}"));
                Console.WriteLine($"{GetType().Name} has just created a new child UserActor for {userId} (Total users: {_users.Count})", Color.Orange);
            }
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