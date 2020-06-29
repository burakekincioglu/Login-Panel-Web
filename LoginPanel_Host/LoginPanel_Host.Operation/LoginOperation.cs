using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using LoginPanel_Host.Operation.Data;
using LoginPanel_Host.Operation.Entity;

namespace LoginPanel_Host.Operation
{
    public class LoginOperation
    {
        private static List<IncorrectEntryUser> incorrectUsers = new List<IncorrectEntryUser>();//Yanlış girişleri tutan liste
        public static string CompareUsernameAndPassword(string username, string password) 
        {
            //Login Panel dbsi için connection objesi
            SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString());
            //Storeprocedure ismi
            string SQL = "UserProfileSelectByUsernameAndPassword";

            //Komut nesnesi oluşturuluyor.
            SqlCommand cmd = new SqlCommand(SQL,conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //StoreProcedure parametreleri oluşturuluyor.
            SqlParameter param;
            param = cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 500);
            param.Value = username;
            param = cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 500);
            param.Value = password;

            if(conn.State==ConnectionState.Closed)
              conn.Open();

            //Sql reader nesnesi oluşturuluyor.Ve execute edilen komut result seti  reader objesine atanıyor.
            SqlDataReader reader = cmd.ExecuteReader();

            UserProfile user = null;
            //reader objesindeki datalar user objesindeki propertilere aktarılıyor.
            if (reader != null && reader.Read())
            {
                user = new UserProfile();
                PopulateUserProfile(reader, user);
            }


            if (incorrectUsers.Count == 0)//Yanlış girişleri tutan liste boşsa userlar dolduruluyor.
            {
               incorrectUsers = GetUsernames();
            }

            string result = "";
            if (user != null)  //user objesi null ise kullanıcı adı veya password yanlış
            {
                switch (user.Stat)
                {
                    case 1:
                        result = "OK";
                        incorrectUsers.Where(x => x.Username == user.CustomerId).FirstOrDefault().IncorrectEntryCount = 0;//Doğru girildiği takdirde Yanlış giriş sayacını sıfırlıyor.
                        UpdateUserLastLoginTime(username, DateTime.Now);
                        break;
                    case 2:
                        result = "BLOCK";
                        break;
                    case 3:
                        result = "CANCEL";
                        break;
                }
            }
            else //3 kere hatalı girme işlemleri kod kısmı
            {
                foreach(IncorrectEntryUser item in incorrectUsers)
                {
                    if (item.Username == username) 
                    {
                        if (item.IncorrectEntryCount < 2)
                        {
                            item.IncorrectEntryCount++;
                            break;
                        }
                        else if(item.IncorrectEntryCount==2)
                        {
                            UpdateUserStat(username,2);//User Bloke Ediliyor.
                            break;
                        }
                    }
                }
                result = "NOT";
            }

            reader.Close();
            reader.Dispose();
            conn.Close();
            conn.Dispose();
            return result;
        }

        //Userın Statını update eden metod
        private static void UpdateUserStat(string username,int stat) 
        {
            SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString());
            string SQL = "UpdateUserProfile";

            SqlCommand cmd = new SqlCommand(SQL, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = cmd.Parameters.Add("@CustomerId", SqlDbType.NVarChar, 500);
            param.Value = username;

            param = cmd.Parameters.Add("@Stat", SqlDbType.Int);
            param.Value = stat;

            if (conn.State == ConnectionState.Closed)
                conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();
            conn.Dispose();
        }

        //Userın LastLoginTime ını update eden metod
        private static void UpdateUserLastLoginTime(string username, DateTime lastLoginTime)
        {
            SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString());
            string SQL = "UpdateUserProfile";

            SqlCommand cmd = new SqlCommand(SQL, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = cmd.Parameters.Add("@CustomerId", SqlDbType.NVarChar, 500);
            param.Value = username;

            param = cmd.Parameters.Add("@LastLoginTime", SqlDbType.DateTime);
            param.Value = lastLoginTime;

            if (conn.State == ConnectionState.Closed)
                conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();
            conn.Dispose();
        }

        //Tüm userların CustomerIdlerini getiren metod -Yanlış giriş yapanları tespit etmek için- 
        private static List<IncorrectEntryUser> GetUsernames() 
        {
            SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString());
            string SQL = "UserProfileSelectUsernames";

            SqlCommand cmd = new SqlCommand(SQL, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (conn.State == ConnectionState.Closed)
                conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            List<IncorrectEntryUser> incorrectEntryUserList =new List<IncorrectEntryUser>();
            IncorrectEntryUser incorrectEntryUser;

            if (reader != null) 
            {
                while (reader.Read())
                {
                    incorrectEntryUser = new IncorrectEntryUser();
                    incorrectEntryUser.Username = reader["CustomerId"].ToString();
                    incorrectEntryUserList.Add(incorrectEntryUser);
                }
            }

            reader.Close();
            reader.Dispose();
            conn.Close();
            conn.Dispose();
            return incorrectEntryUserList;
        }

        //Dbden gelen dataların user objesine aktarılmasını sağlayan metod
        private static void PopulateUserProfile(SqlDataReader reader,UserProfile user)
        {
            user.CustomerId = reader["CustomerId"].ToString();
            user.Password = reader["Password"].ToString();
            user.LastLoginTime = (DateTime)reader["LastLoginTime"];
            user.Stat = (int)reader["Stat"];
            user.LastUpdateDate = (DateTime)reader["LastUpdateDate"];
            user.RecordStat = (int)reader["RecordStat"];
            user.HashType = reader["HashType"].ToString();
        }

    }
}
