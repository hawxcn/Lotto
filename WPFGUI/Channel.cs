using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGUI
{
    class Channel
    {
        public Dictionary<GroupMember, List<Message>> keyByGroupMember = new Dictionary<GroupMember, List<Message>>();

        public List<GroupMember> groupMember;
        public string name;
        public List<Message> mainMessageHistory;

        //构造函数
        public Channel(List<Message> messageHistory)
        {
            groupMember = new List<GroupMember>();
            mainMessageHistory = new List<Message>();
            mainMessageHistory = messageHistory;
        }
        //返回所有Member
        public List<GroupMember> getGroupMember()
        {
            return groupMember;
        }

        public bool addGroupmember(GroupMember groupMember)
        {
            if (groupMember != null)
            {
                this.groupMember.Add(groupMember);
                return true;
            }
            else return false;

        }
        
        //初始化
        public void InitializeGroupMember()
        {

            //遍历记录
            foreach(Message message in mainMessageHistory)
            {
                int isExit = 0;
                foreach(GroupMember i in groupMember)
                {
                    if (message.type.Equals("system"))
                    {
                        isExit = -1;
                        break;
                    }
                    //判断新来的Message的ID是否已存在
                    if (message.ID.Equals(i.ID))//存在
                    {
                        if (!message.type.Equals(i.type))
                        {
                            i.type = message.type;
                        }
                        isExit = 1;
                        keyByGroupMember[i].Add(message);//添加入字典
                        i.PersonalMessage.Add(message);
                        break;
                    }
                }
                if (isExit == 0)
                {
                    GroupMember member = new GroupMember();
                    member.ID = message.ID;
                    member.name = message.name;
                    member.type = message.type;
                    member.PersonalMessage.Add(message);
                    groupMember.Add(member);
                    List<Message> newmemberlist = new List<Message>();
                    newmemberlist.Add(message);
                    keyByGroupMember.Add(member, newmemberlist);
                }
            }
        }
        public WinnerGroup GetLuckyGuys(Condition c)
        {
            Filter filter = new Filter(c);
            return LuckyDraw.CreatLuckyDraw(filter.MemberFilter(groupMember), c);
        }

    }
    
}
