using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using FluentValidation.Results;

namespace TelegramBot.Common.Exceptions
{
    [DataContract]
    public class ValidationFault
    {
        [DataMember]
        public string Message { get; set; }

        public ValidationFault(IEnumerable<ValidationFailure> errors)
        {
            Message = BuildErrorMesage(errors);
        }

        private static string BuildErrorMesage(IEnumerable<ValidationFailure> errors)
        {
            return string.Join("", errors.Select(x => x.ErrorMessage).ToArray());
        }
    }
}