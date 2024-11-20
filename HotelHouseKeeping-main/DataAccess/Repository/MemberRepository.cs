using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using BusinessObject;
namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public Member GetMemberByID(int memberid) => MemberDAO.Instance.GetMemberByID(memberid);
        public IEnumerable<Member> GetMembers() => MemberDAO.Instance.GetMemberList();
        public void InsertMember(Member member) => MemberDAO.Instance.AddNew(member);
        public void DeleteMember(int memberid) => MemberDAO.Instance.Remove(memberid);
        public void UpdateMember(Member member) => MemberDAO.Instance.Update(member);
        public Member GetMemberByEmail(string email) => MemberDAO.Instance.GetMemberByEmail(email);  

    }
}
