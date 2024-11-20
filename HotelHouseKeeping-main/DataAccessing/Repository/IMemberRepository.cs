using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessing.Repository
{
    public interface IMemberRepository
    {
        Member GetMemberByEmail(string email);
        public IEnumerable<Member> GetMembers();
        public void Insert(Member member);
        public void Update(Member member);
        public void Delete(int memberid);
        public Member GetMemberByID(int id);
        public IEnumerable<Member> GetMembersByName(string name);
        public IEnumerable<Member> GetMembersByRole(string role);
    }
}
