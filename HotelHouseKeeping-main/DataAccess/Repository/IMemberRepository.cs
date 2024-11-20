using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using BusinessObject;
namespace DataAccess.Repository
{
     public interface IMemberRepository
    {
        IEnumerable<Member> GetMembers();
        Member GetMemberByID(int memberid);
        void InsertMember(Member member);
        void DeleteMember(int memberid);
        void UpdateMember(Member member);
        Member GetMemberByEmail(string email);
    }
}
