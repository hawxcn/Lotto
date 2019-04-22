using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WPFGUI
{
    class LuckyDraw
    {
        public static int ll;
        public static int[] personSpace;
        public static WinnerGroup CreatLuckyDraw(List<GroupMember> groupList, Condition condition)
        {
            int WinnerNum = condition.firstPrizeNumber + condition.secondPrizeNumber + condition.thirdPrizeNumber;
            if (WinnerNum > groupList.Count)
            {
                return new WinnerGroup(condition.key, groupList,condition);
            }

            List<GroupMember> WinnerGroupList = new List<GroupMember>();
            createPearsonSpace(groupList);

            Boolean isPrise = false;

            for (int i = 0; i < WinnerNum;)
            {
                int ran = RandomNum(groupList.Count);
                for (int j = 0; j < personSpace.Length; j++)
                {
                    if (ran <= personSpace[j] && WinnerGroupList.Count == 0)
                    {
                        GroupMember temp = new GroupMember();
                        temp = groupList[j];
                        WinnerGroupList.Add(temp);
                        i++;
                        break;
                    }
                    else if (ran <= personSpace[j] && WinnerGroupList.Count != 0)
                    {
                        for (int k = 0; k < WinnerGroupList.Count; k++)
                        {
                            if (groupList[j].name.Equals(WinnerGroupList[k].name))
                            {
                                isPrise = true;
                            }
                        }
                        if (!isPrise)
                        {
                            GroupMember temp = new GroupMember();
                            temp = groupList[j];
                            WinnerGroupList.Add(temp);
                            i++;
                            break;
                        }
                        else
                        {
                            isPrise = false;
                            break;
                        }
                    }
                }
            }
            return new WinnerGroup(condition.key, WinnerGroupList, condition);
        }

        private static void createPearsonSpace(List<GroupMember> GroupList)
        {
            personSpace = new int[GroupList.Count];
            personSpace[0] = 0;
            for (int i = 0; i < GroupList.Count; i++)
            {
                if (i == 0)
                {
                    personSpace[i] = GroupList[i].PersonalMessage.Count;
                }
                else
                {
                    personSpace[i] = GroupList[i].PersonalMessage.Count + personSpace[i - 1];
                }
            }
        }

        private static int RandomNum(int n)
        {
            int max = 1 << 10;
            RNGCryptoServiceProvider csp = new RNGCryptoServiceProvider();
            byte[] byteCsp = new byte[10];
            csp.GetBytes(byteCsp);
            int t = Convert.ToInt32(byteCsp[0]) % max;
            int m = personSpace[n - 1];
            int a = 9;
            int b = 7;
            ll = personSpace[n - 1];
            Random random = new Random();
            int ran = random.Next(0, ll);
            for (int i = 1; i < t; i++)
            {
                ran = (a * ran + b) % m;
            }
            return ran;
        }
    }
}
