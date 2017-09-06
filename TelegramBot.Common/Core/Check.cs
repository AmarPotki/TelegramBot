using System;

namespace TelegramBot.Common.Core{
    public class Check{
        public static void Requires<TException>(Func<bool> predicate, string message = null)
            where TException : Exception{
            Requires<TException>(predicate.Invoke());
        }

        public static void Requires<TException>(bool result, string message = null) where TException : Exception{
            if (result) return;
            if (message == null)
                throw Activator.CreateInstance<TException>();
            var exception = Activator.CreateInstance(typeof (TException), message) as TException;
            if (exception != null)
                throw exception;
        }
    }
}