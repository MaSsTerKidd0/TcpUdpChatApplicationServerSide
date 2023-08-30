using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.RequestsCommands
{
    interface ICommand<T>
    {
        void Execute(string[] reqParts, T reach, Server<T> server);
    }
}
