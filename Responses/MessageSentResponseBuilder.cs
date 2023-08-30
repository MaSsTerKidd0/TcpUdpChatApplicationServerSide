using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Responses
{
    public class MessageSentResponseBuilder : IResponseBuilder
    {
        private string _messageSentResponse;
        private string _from;
        private string _to;
        private string _message;
        private string _arrivalTime;

        public MessageSentResponseBuilder(string from, string to, string message, string arrivalTime)
        {
            _messageSentResponse = IResponseBuilder.responseValues["MessageSentResponse"];
            _from = from;
            _to = to;
            _message = message;
            _arrivalTime = arrivalTime;
        }

        public string BuildResponse()
        {
            return $"{_messageSentResponse}{_from}#{_to}#{_message}#{_arrivalTime}";
        }
    }
}
