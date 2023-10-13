using BusinessObject;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.AccessData
{
    public class MemberDAO : BaseDAL
    {
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                }
                return instance;
            }
        }

        public IEnumerable<MemberObject> GetMemberList()
        {
            IDataReader dataReader = null;
            string SQLSelect = "Select * from Member";
            var member = new List<MemberObject>();
            try
            {
                dataReader = dataProvider.GetDataReader(SQLSelect,
                                                        CommandType.Text,
                                                        out connection);
                while (dataReader.Read())
                {
                    member.Add(new MemberObject
                    {
                        MemberId = dataReader.GetInt32(0),
                        Email = dataReader.GetString(1),
                        CompanyName = dataReader.GetString(2),
                        City = dataReader.GetString(3),
                        Country = dataReader.GetString(4),
                        Password = dataReader.GetString(5)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return member;
        }
        public MemberObject GetMemberByID(int memberID)
        {
            MemberObject member = null;
            IDataReader dataReader = null;
            string SQLSelect = "Select * from Member where MemberId=@MemberId";
            try
            {
                var param = dataProvider.CreateParameter("@MemberId", 4, memberID, DbType.Int32);
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                if (dataReader.Read())
                {
                    member = new MemberObject
                    {
                        MemberId = dataReader.GetInt32(0),
                        Email = dataReader.GetString(1),
                        CompanyName = dataReader.GetString(2),
                        City = dataReader.GetString(3),
                        Country = dataReader.GetString(4),
                        Password = dataReader.GetString(5)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return member;

        }

        public void AddNew(MemberObject member)
        {
            try
            {
                MemberObject pro = GetMemberByID(member.MemberId);
                if (pro == null)
                {
                    string SQLInsert = "Insert Member values(@MemberId,@Email,@CompanyName,@City,@Country,@Password)";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter("@MemberId", 4, member.MemberId, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@Email", 100, member.Email, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@CompanyName", 40, member.CompanyName, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@City", 15, member.City, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Country", 15, member.Country, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Password", 30, member.Password, DbType.String));
                    dataProvider.Insert(SQLInsert, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("The member is already exist.");
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { CloseConnection(); }
        }

        public void Update(MemberObject member)
        {
            try
            {
                MemberObject m = GetMemberByID(member.MemberId);
                if (m != null)
                {
                    string SQLUpdate = "Update Member set Email=@Email,CompanyName=@CompanyName," +
                        "City=@City,Country=@Country,Password=@Password where MemberId=@MemberId";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter("@MemberId", 4, member.MemberId, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@Email", 100, member.Email, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@CompanyName", 40, member.CompanyName, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@City", 15, member.City, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Country", 15, member.Country, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Password", 30, member.Password, DbType.String));
                    dataProvider.Insert(SQLUpdate, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("The member does not already exist.");
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { CloseConnection(); }
        }

        public void Remove(int memberId)
        {
            try
            {
                MemberObject member = GetMemberByID(memberId);
                if (member != null)
                {
                    string SQLDelete = "Delete Member where MemberId=@MemberId";
                    var param = dataProvider.CreateParameter("@MemberId", 4, member.MemberId, DbType.Int32);
                    dataProvider.Delete(SQLDelete, CommandType.Text, param);
                }
                else
                {
                    throw new Exception("The member does not already exist.");
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { CloseConnection(); }
        }

        public MemberObject GetDefautAccount()
        {
            MemberObject Default = null;
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            string email = config["DefaultAccount:email"];
            string password = config["DefaultAccount:password"];
            Default = new MemberObject
            {
                MemberId = 1,
                Email = email,
                CompanyName = "",
                City = "",
                Country = "",
                Password = password
            };
            return Default;
        }

        public MemberObject Login(string email, string password)
        {
            MemberObject admin = GetDefautAccount();
            IEnumerable<MemberObject> memberList = GetMemberList();
            MemberObject member = memberList.SingleOrDefault(mb => mb.Email.Equals(email) && mb.Password.Equals(password));
            if (member == null)
            {
                if (admin != null && admin.Email.Equals(email) && admin.Password.Equals(password))
                {
                    return admin;
                }
            }
            return member;
        }

        public bool isAdmin(string email, string password)
        {
            MemberObject admin = GetDefautAccount();
            IEnumerable<MemberObject> memberList = GetMemberList();
            MemberObject member = memberList.SingleOrDefault(mb => mb.Email.Equals(email) && mb.Password.Equals(password));
            if (member == null)
            {
                if (admin != null && admin.Email.Equals(email) && admin.Password.Equals(password))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
