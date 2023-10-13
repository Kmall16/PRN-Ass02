using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        IEnumerable<MemberObject> GetMembers();
        MemberObject GetMemberByID(int memberId);
        void InsertCar(MemberObject member);
        void DeleteCar(int memberId);
        void UpdateCar(MemberObject member);
        MemberObject Login(string email, string password);
        bool IsAdmin(string email, string password);
    }
}
