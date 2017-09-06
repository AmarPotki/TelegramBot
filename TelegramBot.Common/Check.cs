using System;
using System.Diagnostics;
using TelegramBot.Common.Extensions;
namespace TelegramBot.Common{
    public static class Check{
        public static class Argument{
            [DebuggerStepThrough]
            public static void IsNotNull(object parameter, string parameterName){
                if (parameter == null) {
                    throw new ArgumentNullException(parameterName, "{0} cannot be null".FormatWith(parameterName));
                }
            }
            [DebuggerStepThrough]
            public static void IsNotNullOrEmpty(string parameter, string parameterName){
                if (string.IsNullOrWhiteSpace(parameter)) {
                    throw new ArgumentException("{0} cannot be null or empty".FormatWith(parameterName), parameterName);
                }
            }
            [DebuggerStepThrough]
            public static void IsNotZeroOrNegative(int parameter, string parameterName){
                if (parameter <= 0){
                    throw new ArgumentOutOfRangeException(parameterName,
                        "{0} cannot be negative or zero".FormatWith(parameterName));
                }
            }
            [DebuggerStepThrough]
            public static void IsNotNegative(int parameter, string parameterName){
                if (parameter < 0){
                    throw new ArgumentOutOfRangeException(parameterName,
                        "{0} cannot be negative".FormatWith(parameterName));
                }
            }
            [DebuggerStepThrough]
            public static void IsNotZeroOrNegative(long parameter, string parameterName){
                if (parameter <= 0){
                    throw new ArgumentOutOfRangeException(parameterName,
                        "{0} cannot be negative or zero".FormatWith(parameterName));
                }
            }
            [DebuggerStepThrough]
            public static void IsNotNegative(long parameter, string parameterName){
                if (parameter < 0){
                    throw new ArgumentOutOfRangeException(parameterName,
                        "{0} cannot be negative".FormatWith(parameterName));
                }
            }
            [DebuggerStepThrough]
            public static void IsNotZeroOrNegative(float parameter, string parameterName){
                if (parameter <= 0){
                    throw new ArgumentOutOfRangeException(parameterName,
                        "{0} cannot be negative or zero".FormatWith(parameterName));
                }
            }
            [DebuggerStepThrough]
            public static void IsNotNegative(float parameter, string parameterName){
                if (parameter < 0){
                    throw new ArgumentOutOfRangeException(parameterName,
                        "{0} cannot be negative".FormatWith(parameterName));
                }
            }
            [DebuggerStepThrough]
            public static void IsNotInvalidEmail(string argument, string argumentName){
                IsNotNullOrEmpty(argument, argumentName);

                //if (!argument.IsEmail())
                //{
                //    throw new ArgumentException("\"{0}\" is not a valid email address.".FormatWith(argumentName), argumentName);
                //}
            }
        }
    }
}