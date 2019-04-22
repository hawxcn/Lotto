using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace WPFGUI
{
    public class Filter
    {
        Condition condition = null;
        public Filter(Condition condition)
        {
            this.condition = condition;
        }

        //暂时无用
        //public List<Message> MessageFilter(List<Message> mh)
        //{
        //    List<Message> ms = mh;    //这里是要筛选的信息History
        //    List<Message> deletemsa = new List<Message>();
        //    foreach(Message msa in ms)
        //    {
        //        if (!(DateTime.Compare(msa.sendTime, condition.starTime) > 0 && DateTime.Compare(msa.sendTime,condition.endTime) < 0 && msa.speaker.PersonalMessage.Count >= condition.frequency))
        //        {
        //            deletemsa.Add(msa);
        //        }
        //    }
        //    foreach(Message msas in deletemsa)
        //    {
        //        ms.Remove(msas);
        //    }
        //    return ms;
        //}


        //GroupMember筛选
        public List<GroupMember> MemberFilter(List<GroupMember> a)
        {
            List<GroupMember> s = a;
            List<GroupMember> deletegm = new List<GroupMember>();
            foreach (GroupMember gm in s)
            {
                //不符合条件的加入删除列表
                if (!(isLuck(gm, condition.type) && gm.PersonalMessage.Count >= condition.frequency && isSpeak(gm.PersonalMessage)))// !(isLuck(gm, condition.type) && gm.PersonalMessage.Count >= condition.frequency && isSpeak(gm.PersonalMessage))
                {
                    deletegm.Add(gm);
                }

            }
            foreach (GroupMember gmsa in deletegm)
            {
                s.Remove(gmsa);
            }
            return s;
        }
        private bool isLuck(GroupMember g, int k)
        {
            int n = 0;
            if (g.type.Equals("student"))
            {
                n = 4;
            }
            else if (g.type.Equals("assistant"))
            {
                n = 2;
            }
            else if (g.type.Equals("teacher"))
            {
                n = 1;
            }

            if (n == (k & n)) return true;
            return false;
        }

        private bool isSpeak(List<Message> list)
        {
            foreach (Message ms in list)
            {
                if (DateTime.Compare(ms.sendTime, condition.starTime) >= 0 && DateTime.Compare(ms.sendTime, condition.endTime) <= 0 && ms.theme.Contains(condition.key))
                {
                    return true;
                }
            }
            return false;
        }
    }

}
