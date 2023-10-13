using BusinessObject;
using DataAccess.AccessData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        IEnumerable<MemberObject> IMemberRepository.GetMembers() => MemberDAO.Instance.GetMemberList();
        MemberObject IMemberRepository.GetMemberByID(int memberId) => MemberDAO.Instance.GetMemberByID(memberId);
        void IMemberRepository.InsertCar(MemberObject member) => MemberDAO.Instance.AddNew(member);
        void IMemberRepository.DeleteCar(int memberId) => MemberDAO.Instance.Remove(memberId);
        void IMemberRepository.UpdateCar(MemberObject member) => MemberDAO.Instance.Update(member);

        MemberObject IMemberRepository.Login(string email, string password) => MemberDAO.Instance.Login(email, password);
        bool IMemberRepository.IsAdmin(string email, string password) => MemberDAO.Instance.isAdmin(email, password);
    }
}
