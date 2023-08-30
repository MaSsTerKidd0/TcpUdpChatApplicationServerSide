using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Responses
{
    internal class JoinedGroupResponseBuilder : IResponseBuilder
    {
        private string _groupName;
        private string _joinedGroupResponse;
        public JoinedGroupResponseBuilder(string groupName) 
        {
            _joinedGroupResponse = IResponseBuilder.responseValues["JoinedChatResponse"];
            _groupName = groupName;
        } 
        public string BuildResponse()
        {
            return _joinedGroupResponse + _groupName;
        }
    }
}
