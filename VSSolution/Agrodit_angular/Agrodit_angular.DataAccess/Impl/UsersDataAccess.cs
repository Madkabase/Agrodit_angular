using Agrodit_angular.DataAccess.Interface;
using Agrodit_angular.Model;
using Agrodit_angular.Utility;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Agrodit_angular.DataAccess.Impl
{
    public class UsersDataAccess : IUsersDataAccess
    {
        private MSSqlDatabase MSSqlDatabase { get; set; }
        public UsersDataAccess(MSSqlDatabase msSqlDatabase)
        {
            MSSqlDatabase = msSqlDatabase;
        }
        public List<UsersModel> GetAllUsers(int page=1,int itemsPerPage=100,List<OrderByModel> orderBy=null)
        {
            var ret = new List<UsersModel>();
			int offset = (page - 1) * itemsPerPage;
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM Users t  Order by t.Id OFFSET @Offset ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
             if(orderBy!=null && orderBy.Count > 0)
            {
                cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText,orderBy);
            }
			cmd.Parameters.AddWithValue("@Offset", offset);
            cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    var t = new UsersModel()
                    {
                       Id= reader.GetValue<Int64>("Id"),
FirstName= reader.GetValue<String>("FirstName"),
LastName= reader.GetValue<String>("LastName"),
Email= reader.GetValue<String>("Email"),
Password= reader.GetValue<String>("Password"),
IsVerified= reader.GetBoolean("IsVerified"),
ConfirmationCode= reader.GetValue<Int64>("ConfirmationCode"),
ConfirmationExpirationDate= reader.GetValue<DateTime>("ConfirmationExpirationDate"),
ConfirmationTriesCounter= reader.GetValue<Int64>("ConfirmationTriesCounter"),
                    };

                    ret.Add(t);
                }
            return ret;
        }

        public List<UsersModel> SearchUsers(string searchKey, int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy=null)
        {
            var ret = new List<UsersModel>();
            int offset = (page - 1) * itemsPerPage;
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM Users t  WHERE 1=1 AND  t.FirstName LIKE CONCAT('%',@SearchKey,'%') OR t.LastName LIKE CONCAT('%',@SearchKey,'%') OR t.Email LIKE CONCAT('%',@SearchKey,'%') OR t.Password LIKE CONCAT('%',@SearchKey,'%') Order by t.Id OFFSET @Offset ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
             if(orderBy!=null && orderBy.Count > 0)
            {
                cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText,orderBy);
            }
            cmd.Parameters.AddWithValue("@SearchKey", searchKey);
            cmd.Parameters.AddWithValue("@Offset", offset);
            cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    var t = new UsersModel()
                    {
                       Id= reader.GetValue<Int64>("Id"),
FirstName= reader.GetValue<String>("FirstName"),
LastName= reader.GetValue<String>("LastName"),
Email= reader.GetValue<String>("Email"),
Password= reader.GetValue<String>("Password"),
IsVerified= reader.GetBoolean("IsVerified"),
ConfirmationCode= reader.GetValue<Int64>("ConfirmationCode"),
ConfirmationExpirationDate= reader.GetValue<DateTime>("ConfirmationExpirationDate"),
ConfirmationTriesCounter= reader.GetValue<Int64>("ConfirmationTriesCounter"),
                    };

                    ret.Add(t);
                }
            return ret;
        }

        public  int GetAllTotalRecordUsers()
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT Count(*) as TotalRecord FROM Users t";
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                { 
                    return reader.GetInt32("TotalRecord");
                }
            return 0;
        }
        public int GetSearchTotalRecordUsers(string searchKey)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT Count(*) as TotalRecord FROM Users t  WHERE 1=1 AND  t.FirstName LIKE CONCAT('%',@SearchKey,'%') OR t.LastName LIKE CONCAT('%',@SearchKey,'%') OR t.Email LIKE CONCAT('%',@SearchKey,'%') OR t.Password LIKE CONCAT('%',@SearchKey,'%')";
            cmd.Parameters.AddWithValue("@SearchKey", searchKey);
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                { 
                    return reader.GetInt32("TotalRecord");
                }
            return 0;
        }

		

        public UsersModel GetUsersByID(int id)
        {

            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM Users t  WHERE t.Id= @Id Order by t.Id OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY";
			cmd.Parameters.AddWithValue("@id", id);

            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    return new UsersModel()
                    {
                        Id= reader.GetValue<Int64>("Id"),
FirstName= reader.GetValue<String>("FirstName"),
LastName= reader.GetValue<String>("LastName"),
Email= reader.GetValue<String>("Email"),
Password= reader.GetValue<String>("Password"),
IsVerified= reader.GetBoolean("IsVerified"),
ConfirmationCode= reader.GetValue<Int64>("ConfirmationCode"),
ConfirmationExpirationDate= reader.GetValue<DateTime>("ConfirmationExpirationDate"),
ConfirmationTriesCounter= reader.GetValue<Int64>("ConfirmationTriesCounter"),
                    };
                }
            return null;
        }

         public List<UsersModel> FilterUsers(List<FilterModel> filterBy,string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy)
        {
            var ret = new List<UsersModel>();
            int offset = (page - 1) * itemsPerPage;
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM Users t  {filterColumns} Order by t.Id OFFSET @Offset ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
            if(filterBy!=null && filterBy.Count > 0)
            {
                var whereClause = string.Empty;
                int paramCount=0;
                foreach (var r in filterBy)
                {
                    if (!string.IsNullOrEmpty(r.ColumnName))
                    {
                    paramCount++;
                        if (!string.IsNullOrEmpty(whereClause))
                        {
                            whereClause=whereClause + " " + andOr + " ";
                        }
                        whereClause = whereClause + "t." + r.ColumnName + " "+UtilityCommon.ConvertFilterToSQLString(r.ColumnCondition) + " @" + r.ColumnName+paramCount;
                        cmd.Parameters.AddWithValue("@"+ r.ColumnName+paramCount, r.ColumnValue);
                    }
                }
                whereClause = whereClause.Trim();
                cmd.CommandText = cmd.CommandText.Replace("{filterColumns}", "Where " + whereClause);
            }
            else
            {
                cmd.CommandText = cmd.CommandText.Replace("{filterColumns}", "");
            }
            if (orderBy != null && orderBy.Count > 0)
            {
                cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
            }
            cmd.Parameters.AddWithValue("@Offset", offset);
            cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                { 
                var t = new UsersModel()
                    {
                       Id= reader.GetValue<Int64>("Id"),
FirstName= reader.GetValue<String>("FirstName"),
LastName= reader.GetValue<String>("LastName"),
Email= reader.GetValue<String>("Email"),
Password= reader.GetValue<String>("Password"),
IsVerified= reader.GetBoolean("IsVerified"),
ConfirmationCode= reader.GetValue<Int64>("ConfirmationCode"),
ConfirmationExpirationDate= reader.GetValue<DateTime>("ConfirmationExpirationDate"),
ConfirmationTriesCounter= reader.GetValue<Int64>("ConfirmationTriesCounter"),
                    };

                    ret.Add(t);
                }
            return ret;
        }

       public int GetFilterTotalRecordUsers(List<FilterModel> filterBy,string andOr)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT Count(*) as TotalRecord FROM Users t {filterColumns}";
            if (filterBy != null && filterBy.Count > 0)
            {
            int paramCount=0;
                var whereClause = string.Empty;
                foreach (var r in filterBy)
                {
                    if (!string.IsNullOrEmpty(r.ColumnName))
                    {paramCount++;
                        if (!string.IsNullOrEmpty(whereClause))
                        {
                            whereClause = whereClause + " " + andOr + " ";
                        }
                        whereClause = whereClause + "t." + r.ColumnName + " "+UtilityCommon.ConvertFilterToSQLString(r.ColumnCondition) + " @" + r.ColumnName+paramCount;
                        cmd.Parameters.AddWithValue("@" + r.ColumnName+paramCount, r.ColumnValue);
                    }
                }
                whereClause = whereClause.Trim();
                cmd.CommandText = cmd.CommandText.Replace("{filterColumns}", "Where " + whereClause);
            }
            else
            {
                cmd.CommandText = cmd.CommandText.Replace("{filterColumns}", "");
            }
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    return reader.GetInt32("TotalRecord");
                }
            return 0;
        }

        public bool UpdateUsers(UsersModel model)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"UPDATE Users SET Id=@Id,FirstName=@FirstName,LastName=@LastName,Email=@Email,Password=@Password,IsVerified=@IsVerified,ConfirmationCode=@ConfirmationCode,ConfirmationExpirationDate=@ConfirmationExpirationDate,ConfirmationTriesCounter=@ConfirmationTriesCounter WHERE Id = @Id;";
            cmd.Parameters.AddWithValue("@Id", Helper.GetNullableParameter(model.Id));
cmd.Parameters.AddWithValue("@FirstName", Helper.GetNullableParameter(model.FirstName));
cmd.Parameters.AddWithValue("@LastName", Helper.GetNullableParameter(model.LastName));
cmd.Parameters.AddWithValue("@Email", Helper.GetNullableParameter(model.Email));
cmd.Parameters.AddWithValue("@Password", Helper.GetNullableParameter(model.Password));
cmd.Parameters.AddWithValue("@IsVerified", Helper.GetNullableParameter(model.IsVerified));
cmd.Parameters.AddWithValue("@ConfirmationCode", Helper.GetNullableParameter(model.ConfirmationCode));
cmd.Parameters.AddWithValue("@ConfirmationExpirationDate", Helper.GetNullableParameter(model.ConfirmationExpirationDate));
cmd.Parameters.AddWithValue("@ConfirmationTriesCounter", Helper.GetNullableParameter(model.ConfirmationTriesCounter));
            var recs = cmd.ExecuteNonQuery();
            if (recs > 0)
            {  
            transaction.Commit();
                return true;
            }
            return false;
        }

        public long AddUsers(UsersModel model)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"INSERT INTO Users (Id,FirstName,LastName,Email,Password,IsVerified,ConfirmationCode,ConfirmationExpirationDate,ConfirmationTriesCounter) VALUES (@Id,@FirstName,@LastName,@Email,@Password,@IsVerified,@ConfirmationCode,@ConfirmationExpirationDate,@ConfirmationTriesCounter);SELECT SCOPE_IDENTITY();";
            cmd.Parameters.AddWithValue("@Id", Helper.GetNullableParameter(model.Id));
cmd.Parameters.AddWithValue("@FirstName", Helper.GetNullableParameter(model.FirstName));
cmd.Parameters.AddWithValue("@LastName", Helper.GetNullableParameter(model.LastName));
cmd.Parameters.AddWithValue("@Email", Helper.GetNullableParameter(model.Email));
cmd.Parameters.AddWithValue("@Password", Helper.GetNullableParameter(model.Password));
cmd.Parameters.AddWithValue("@IsVerified", Helper.GetNullableParameter(model.IsVerified));
cmd.Parameters.AddWithValue("@ConfirmationCode", Helper.GetNullableParameter(model.ConfirmationCode));
cmd.Parameters.AddWithValue("@ConfirmationExpirationDate", Helper.GetNullableParameter(model.ConfirmationExpirationDate));
cmd.Parameters.AddWithValue("@ConfirmationTriesCounter", Helper.GetNullableParameter(model.ConfirmationTriesCounter));
              var recs = cmd.ExecuteScalar();
            if (recs!=null)
            {
                transaction.Commit();
                long.TryParse(recs.ToString(), out long result);
                return result>0? result : 1;
            }
            return -1;
          
        }

        public bool DeleteUsers(int id)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"DELETE FROM Users Where Id=@Id";
			cmd.Parameters.AddWithValue("@id", id);
            var recs = cmd.ExecuteNonQuery();
            if (recs > 0)
            { 
                transaction.Commit();
                return true;
            }
            return false;
        }
        public bool DeleteMultipleUsers(List<DeleteMultipleModel> deleteParam,string andOr)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            SqlTransaction transaction = this.MSSqlDatabase.Connection.BeginTransaction("");
            cmd.Transaction = transaction;
            cmd.CommandText = @"DELETE FROM Users Where";
            int count = 0;
            foreach (var r in deleteParam)
            {
                if (count == 0)
                {
                    cmd.CommandText = cmd.CommandText + " " + r.ColumnName + "=@" + r.ColumnName;
                }
                else
                {
                    cmd.CommandText = cmd.CommandText + " "+andOr+" " + r.ColumnName + "=@" + r.ColumnName;
                }
                cmd.Parameters.AddWithValue("@" + r.ColumnName, r.ColumnValue);
                count++;
            }

            var recs = cmd.ExecuteNonQuery();
            if (recs > 0)
            {
                transaction.Commit();
                return true;
            }
            return false;
        }
        
    }
}

