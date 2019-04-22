using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classify
{
    class GroupMember
    {
        public string type;
        public string name;
        public string ID;
        public List<Message> PersonalMessage = new List<Message>();
    }
}
