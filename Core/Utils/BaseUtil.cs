using Core.Constant;
using Core.Data.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data.Common;
using System.Data;
using System.Drawing;
using System.Linq.Expressions;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Core.Data.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Core.Utils
{
    public static class BaseUtil
    {
     
        public static List<T> RawSqlQuery<T>(string query, Func<DbDataReader, T> map)
        {
            using (var context = new ApplicationDbContext())
            {
                using (var command = context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    context.Database.OpenConnection();

                    using (var result = command.ExecuteReader())
                    {
                        var entities = new List<T>();

                        while (result.Read())
                        {
                            entities.Add(map(result));
                        }

                        return entities;
                    }
                }
            }
        }
        public static DataTable GetAllDBRows(string tableName, string whereClause, SqlConnection cn)
        {
            return GetAllDBRows("select * from " + tableName + " where " + whereClause, cn);
        }

        public static DataTable GetAllDBRows(string query, SqlConnection cn)
        {
            SqlDataAdapter da = new SqlDataAdapter(query, cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        //public static Expression<Func<T, bool>> AddSearchCriteria<T>(FilterVM model)
        //{
        //    Expression<Func<T, bool>> predicate = x=> true;

        //    if (!string.IsNullOrEmpty(model.SearchBy))
        //    {
        //        IList<SearchParams> filters = JsonConvert.DeserializeObject<List<SearchParams>>(model.SearchBy);

        //        // Apply advanced search criteria from SearchBy
        //        if (filters != null && filters.Any())
        //        {
        //            foreach (var criteria in filters)
        //            {
        //                var parameterExp = Expression.Parameter(typeof(T), "x");
        //                var propertyExp = Expression.Property(parameterExp, criteria.Key);
        //                var valueExp = Expression.Constant(criteria.Value.ToLower());
        //                var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
        //                var propertyToLowerCase = Expression.Call(propertyExp, toLowerMethod);
        //                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        //                var containsExpression = Expression.Call(propertyToLowerCase, containsMethod, valueExp);

        //                var lambda = Expression.Lambda<Func<T, bool>>(containsExpression, parameterExp);

        //                predicate = predicate.And(lambda);
        //            }

        //        }
        //    }
        //    return predicate;

        //}
        //public static Expression<Func<T, bool>> AddSearchCriteria<T>(FilterVM model, Expression<Func<T, bool>> predicate)
        //{
        //    //Expression<Func<T, bool>> predicate = x => true;

        //    if (!string.IsNullOrEmpty(model.SearchBy) && model.SearchBy != "[]")
        //    {
        //        IList<SearchParams> filters = JsonConvert.DeserializeObject<List<SearchParams>>(model.SearchBy);
        //        if (filters != null && filters.Any())
        //        {
        //            foreach (var criteria in filters)
        //            {
        //                var parameterExp = Expression.Parameter(typeof(T), "x");
        //                var propertyExp = Expression.Property(parameterExp, criteria.Key);
        //                var propertyType = typeof(T).GetProperty(criteria.Key).PropertyType;
        //                Expression comparisonExp = null;

        //                if (propertyType == typeof(string))
        //                {
        //                    var valueExp = Expression.Constant(criteria.Value.ToLower());
        //                    var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
        //                    var propertyToLowerCase = Expression.Call(propertyExp, toLowerMethod);
        //                    var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        //                    comparisonExp = Expression.Call(propertyToLowerCase, containsMethod, valueExp);
        //                }
        //                else if (propertyType == typeof(int))
        //                {
        //                    var valueExp = Expression.Constant(int.Parse(criteria.Value));
        //                    comparisonExp = Expression.Equal(propertyExp, valueExp);
        //                }
        //                else if (propertyType == typeof(int?))
        //                {
        //                    var valueExp = Expression.Constant((int?)int.Parse(criteria.Value), typeof(int?));
        //                    comparisonExp = Expression.Equal(propertyExp, valueExp);
        //                }
        //                else if (propertyType == typeof(bool))
        //                {
        //                    var valueExp = Expression.Constant(bool.Parse(criteria.Value));
        //                    comparisonExp = Expression.Equal(propertyExp, valueExp);
        //                }
        //                else if (propertyType == typeof(DateTime))
        //                {
        //                    var valueExp = Expression.Constant(DateTime.Parse(criteria.Value));
        //                    comparisonExp = Expression.Equal(propertyExp, valueExp);
        //                }
        //                else if (propertyType == typeof(DateTime?))
        //                {
        //                    var valueExp = Expression.Constant(DateTime.Parse(criteria.Value).Date);
        //                    if (valueExp!=null&&valueExp.Value!=null)
        //                        comparisonExp = Expression.Equal(propertyExp, valueExp);
        //                }
        //                if (comparisonExp != null)
        //                {
        //                    var lambda = Expression.Lambda<Func<T, bool>>(comparisonExp, parameterExp);
        //                    predicate = predicate.And(lambda);
        //                }
        //            }
        //        }
        //    }
        //    return predicate;
        //}

        private static bool IsNotDateNumberOrBool(string value)
        {
            if (DateTime.TryParse(value, out _))
                return false;

            if (bool.TryParse(value, out _))
                return false;

            if (int.TryParse(value, out _))
                return false;

            if (double.TryParse(value, out _))
                return false;

            return true;
        }
        public static string GenerateSearchQuery<T>(string searchValue)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            string query = "";

            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(string) || Nullable.GetUnderlyingType(property.PropertyType) == typeof(int))
                {
                    query += $"{property.Name} like '%{searchValue}%' OR ";
                }
            }

            query = query.TrimEnd(" OR ".ToCharArray()); // Remove trailing " OR "

            return query;
        }
        // Generate random digit code
        public static string GenerateRandomDigitCode(int length)
        {
            using var random = new SecureRandomNumberGenerator();
            var str = string.Empty;
            for (var i = 0; i < length; i++)
                str = string.Concat(str, random.Next(10).ToString());
            return str;
        }

        // Returns an random integer number within a specified rage
        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue)
        {
            using var random = new SecureRandomNumberGenerator();
            return random.Next(min, max);
        }
        #region SendMessage
        public static bool SendMessage(string message, string number)
        {
            SqlDataReader cmdReader;
            var con = new SqlConnection(Config.config.GetSection("ConnectionStringsForMessage").GetSection("DawlanceContext").Value);
            try
            {
                con.Open();
                SqlCommand cmdProc = new(@$"EXEC [SP_SMS_Svc_SalesmanApp_Common] '" + number + "' , '" + message + "'", con);
                cmdProc.CommandTimeout = 100;
                cmdReader = cmdProc.ExecuteReader();
                if (cmdReader.HasRows)
                {
                    cmdReader.Close();
                    return true;
                }
                cmdReader.Close();
            }
            catch
            {
                return false;
            }
            finally
            { con.Close(); }
            return false;
        }
        #endregion
        //public static bool ArePointsNear(LocationVM checkPoint, LocationVM centerPoint, double km)
        //{
        //    const double Ky = 40000 / 360.0;

        //    var kx = Math.Cos(Math.PI * centerPoint.X / 180.0) * Ky;
        //    var dx = Math.Abs(centerPoint.Y - checkPoint.Y) * kx;
        //    var dy = Math.Abs(centerPoint.X - checkPoint.X) * Ky;

        //    var distance = Math.Sqrt(dx * dx + dy * dy);
        //    return distance <= (km/1000);
        //}
        public static string GetRawUrlForScan(HttpRequest request,string code)
        {
            var httpContext = request.HttpContext;
            return $"{request.Headers.FirstOrDefault(x=>x.Key == SiteUrl.Referer).Value}{Config.config.GetSection("RedirectLink").Value}/{code}";
        } 
        public static string GetRawUrl(HttpRequest request)
        {
            var httpContext = request.HttpContext;
            return $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";
        }
        public static string GetRawUrlComplete(HttpRequest request)
        {
            var httpContext = request.HttpContext;
            return $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.Path}{httpContext.Request.QueryString}";
        }
    }
}
