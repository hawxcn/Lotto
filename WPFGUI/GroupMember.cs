using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGUI
{
    public class GroupMember
    {
        public string type;
        public string name;
        public string ID;
        public List<Message> PersonalMessage = new List<Message>();
        //public List<Message> PersonalMessage;
    }
}
