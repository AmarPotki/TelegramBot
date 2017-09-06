namespace TelegramBot.Business.DTOs.Logs{
    using System;
    using System.Runtime.Serialization;
    using System.Web.Script.Serialization;


    [DataContract]
    public class FaultDto{
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string StackTrace { get; set; }

        [DataMember]
        [ScriptIgnore]
        public FaultSource FaultSource { get; set; }

        [DataMember]
        public string Location { get; set; }
        public string FaultSourceName{
            get{
                return Enum.GetName(typeof(FaultSource), FaultSource);
            }
        }



        public FaultDto(){
            Id = Guid.NewGuid();
        }

        public FaultDto(string location, string message, string stackTrace, FaultSource faultSource){
            Id = Guid.NewGuid();
            StackTrace = stackTrace;
            Message = message;
            FaultSource = faultSource;
            Location = location;
        }

        public FaultDto(string location, string message, FaultSource faultSource){
            Id = Guid.NewGuid();
            Message = message;
            FaultSource = faultSource;
            Location = location;
        }

        public string ToJsonString(){
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(this);
        }
    }
}