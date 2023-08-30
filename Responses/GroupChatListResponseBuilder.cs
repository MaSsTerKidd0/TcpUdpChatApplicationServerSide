using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Responses
{
    public class GroupChatListResponseBuilder : IResponseBuilder
    {
        private string _groupChatListResponse;
        private List<string> _chatNames;

        public GroupChatListResponseBuilder(List<string> chatNames)
        {
            _groupChatListResponse = IResponseBuilder.responseValues["CreatedChatResponse"];
            _chatNames = chatNames;
        }

        public string BuildResponse()
        {
            string chatNamesStr = string.Join("#", _chatNames);
            return _groupChatListResponse + chatNamesStr;
        }
    }
}
