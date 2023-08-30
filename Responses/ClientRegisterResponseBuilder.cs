using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Responses
{
    public class ClientRegisterResponseBuilder : IResponseBuilder
    {
        private string _allClientsNamesResponse;
        private List<string> _clientsNames;

        public ClientRegisterResponseBuilder(List<string> clientsNames)
        {
            _allClientsNamesResponse = IResponseBuilder.responseValues["AllClientsNamesResponse"];
            _clientsNames = clientsNames;
        }

        public string BuildResponse()
        {
            string clientsNamesStr = string.Join("#", _clientsNames);
            return _allClientsNamesResponse + clientsNamesStr;
        }
    }
}
