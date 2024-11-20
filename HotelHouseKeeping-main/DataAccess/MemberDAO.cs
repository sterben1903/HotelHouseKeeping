using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BusinessObject;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
        public class MemberDAO:BaseDAL
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
                        return instance;
                    }
                }
            }
            public IEnumerable<Member> GetMemberList()
            {
                IDataReader dataReader = null;
                string SQLSelect = "Select MemberID,Email,Password,Name,Address,Phone,Role from Member";
                var mems = new List<Member>();
                try
                {
                    dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection);
                    while (dataReader.Read())
                    {
                        mems.Add(new Member
                        {
                            MemberID = dataReader.GetInt32(0),
                            Email = dataReader.GetString(1),                        
                            Password = dataReader.GetString(2),
                            Name = dataReader.GetString(3),
                            Address = dataReader.GetString(4),
                            Phone = dataReader.GetString(5),
                            Role = dataReader.GetString(6)
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
                return mems;
            }
            public Member GetMemberByID(int memberid)
            {
                Member member = null;
                IDataReader dataReader = null;
                string SQLSelect = "Select MemberID,Email,Password,Name,Address,Phone,Role from Member where MemberID=@MemberID";
                try
                {
                    var param = dataProvider.CreateParameter("@MemberID", 4, memberid, DbType.Int32);
                    dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                    if (dataReader.Read())
                    {
                        member = new Member
                        {
                            MemberID = dataReader.GetInt32(0),
                            Email = dataReader.GetString(1),
                            Password = dataReader.GetString(2),
                            Name = dataReader.GetString(3),
                            Address = dataReader.GetString(4),
                            Phone = dataReader.GetString(5),
                            Role = dataReader.GetString(6)
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
            public Member GetMemberByEmail(string email)
            {
                Member member = null;
                IDataReader dataReader = null;
                string SQLSelect = "Select MemberID,Email,Password,Name,Address,Phone,Role from Member where Email=@Email";
                try
                {
                    var param = dataProvider.CreateParameter("@Email", 100, email, DbType.String);
                    dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                    if (dataReader.Read())
                    {
                        member = new Member
                        {
                            MemberID = dataReader.GetInt32(0),
                            Email = dataReader.GetString(1),
                            Password = dataReader.GetString(2),
                            Name = dataReader.GetString(3),
                            Address = dataReader.GetString(4),
                            Phone = dataReader.GetString(5),
                            Role = dataReader.GetString(6)
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
            public void AddNew(Member member)
            {
                try
                {
                    Member pro = GetMemberByID(member.MemberID);
                    if (pro == null)
                    {
                        string SQLInsert = "Insert Member values (@Email,@Password,@Name,@Address,@Phone,@Role) ";
                        var parameters = new List<SqlParameter>();
                        //parameters.Add(dataProvider.CreateParameter("@MemberID", 4, member.MemberID, DbType.Int32));
                        parameters.Add(dataProvider.CreateParameter("@Email", 100, member.Email, DbType.String));         
                        parameters.Add(dataProvider.CreateParameter("@Password", 30, member.Password, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Name", 40, member.Name, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Address", 100, member.Address, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Phone", 15, member.Phone, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Role", 15, member.Role, DbType.String));
                        dataProvider.Insert(SQLInsert, CommandType.Text, parameters.ToArray());
                    }
                    else
                    {
                        throw new Exception("Member is already exist");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    CloseConnection();
                }
            }
            public void Update(Member member)
            {
                try
                {
                    Member pro = GetMemberByID(member.MemberID);
                    if (pro != null)
                    {
                        string SQLUpdate = "Update Member set Email=@Email,CompanyName=@CompanyName,City=@City,Country=@Country,Password=@Password,Role=@Role where MemberID=@MemberID ";
                        var parameters = new List<SqlParameter>();
                        parameters.Add(dataProvider.CreateParameter("@MemberID", 4, member.MemberID, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter("@Email", 100, member.Email, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Password", 30, member.Password, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Name", 40, member.Name, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Address", 100, member.Address, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Phone", 15, member.Phone, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Role", 15, member.Role, DbType.String));
                }
                    else
                    {
                        throw new Exception("Member does not already exist");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    CloseConnection();
                }
            }
            public void Remove(int memberid)
            {
                try
                {
                    Member member = GetMemberByID(memberid);
                    if (member != null)
                    {
                        string SQLDelete = "Delete Member where MemberID=@MemberID";
                        var param = dataProvider.CreateParameter("@MemberID", 4, memberid, DbType.Int32);
                        dataProvider.Delete(SQLDelete, CommandType.Text, param);
                    }
                    else
                    {
                        throw new Exception("Member does not already exist");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    CloseConnection();
                }
            }
        }
}
