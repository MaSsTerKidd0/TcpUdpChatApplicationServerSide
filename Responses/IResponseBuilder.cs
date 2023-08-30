using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Responses
{
    interface IResponseBuilder
    {
        public static Dictionary<string, string> responseValues = new Dictionary<string, string>()
        {
            { "AllClientsNamesResponse", "500#" },
            { "MessageSentResponse", "600#" },
            { "JoinedChatResponse", "700#" },
            { "CreatedChatResponse", "800#" }
        };
        string BuildResponse();
    }
}
