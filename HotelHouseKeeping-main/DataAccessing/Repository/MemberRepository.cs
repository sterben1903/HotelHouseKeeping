using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessing.Repository
{
    public class MemberRepository:IMemberRepository
    {
        public Member GetMemberByEmail(string email) => MemberDAO.Instance.GetMemberByEmail(email);
        public IEnumerable<Member>GetMembers()=>MemberDAO.Instance.GetMemberList();
        public void Insert(Member member)=>MemberDAO.Instance.AddNew(member);
        public void Update(Member member)=>MemberDAO.Instance.Update(member);
        public void Delete (int memberid)=>MemberDAO.Instance.Remove(memberid);
        public Member GetMemberByID(int id) => MemberDAO.Instance.GetMemberByID(id);
        public IEnumerable<Member> GetMembersByName(string name) => MemberDAO.Instance.GetMemberListByName(name);
        public IEnumerable<Member> GetMembersByRole(string role) => MemberDAO.Instance.GetMemberListByRole(role);
    }
}
