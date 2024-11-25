using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SkilltonTestProject
{
    internal static class DbWork
    {
        private static string _connectionString
        {
            get { return ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString; }
            set { }
        }

        public static Employee GetEmployeeById(int id)
        {
            var retValue = new Employee();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM Employees WHERE EmployeeID=@Id";
                    SqlCommand cmd = new SqlCommand(sql,conn);
                    SqlParameter param = new SqlParameter("@Id", id);
                    cmd.Parameters.Add(param);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    retValue = Dapper.SqlMapper.Parse<Employee>(rdr).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка - {ex.Message}");
            }
            return retValue;
        }

        public static List<Employee> GetAllEmployees()
        {
            var retValue = new List<Employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM Employees";
                    SqlCommand cmd = new SqlCommand(sql,conn);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    retValue = Dapper.SqlMapper.Parse<Employee>(rdr).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка - {ex.Message}");
            }
            return retValue;
        }

        public static void DeleteEmployee(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = "DELETE FROM Employees WHERE EmployeeID=@Id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlParameter param = new SqlParameter("@Id", id);
                    cmd.Parameters.Add(param);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка - {ex.Message}");
            }
        }

        public static int GetMaxIdEmployee()
        {
            string sql = "GetMaxIdEmployeeProc";
            object maxId = -1;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter maxIdSqlParameter = new SqlParameter 
                    { 
                        ParameterName = "@MaxId",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add (maxIdSqlParameter);
                    cmd.ExecuteNonQuery();
                     maxId = cmd.Parameters["@MaxId"].Value;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка - {ex.Message}");
            }
            return (int)maxId;
        }

        public static void UpdateEmployee(Employee employee)
        {
            string sql = "UpdateEmployee";
            object maxId = -1;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    SqlParameter idParam = new SqlParameter
                    {
                        ParameterName = "@EmployeeID",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input,
                        Value=employee.EmployeeID
                    };
                    cmd.Parameters.Add(idParam);

                    SqlParameter firstNameParam = new SqlParameter
                    {
                        ParameterName = "@FirstName",
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input,
                        Value=employee.FirstName
                    };
                    cmd.Parameters.Add(firstNameParam);

                    SqlParameter lastNameParam = new SqlParameter
                    {
                        ParameterName = "@LastName",
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input,
                        Value = employee.LastName
                    };
                    cmd.Parameters.Add(lastNameParam);

                    SqlParameter emailParam = new SqlParameter
                    {
                        ParameterName = "@Email",
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input,
                        Value = employee.Email
                    };
                    cmd.Parameters.Add(emailParam);

                    SqlParameter dateOfBirthParam = new SqlParameter
                    {
                        ParameterName = "@DateOfBirth",
                        SqlDbType = SqlDbType.Date,
                        Direction = ParameterDirection.Input,
                        Value = employee.DateOfBirth
                    };
                    cmd.Parameters.Add(dateOfBirthParam);

                    SqlParameter salaryParam = new SqlParameter
                    {
                        ParameterName = "@Salary",
                        SqlDbType = SqlDbType.Decimal,
                        Direction = ParameterDirection.Input,
                        Value = employee.Salary
                    };
                    cmd.Parameters.Add(salaryParam);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка - {ex.Message}");
            }
        }

        public static void AddEmployee(Employee employee)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = "INSERT INTO Employees (EmployeeID, FirstName, LastName, Email, DateOfBirth, Salary) VALUES (@Id,@FirstName,@LastName,@Email,@DateOfBirth,@Salary)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlParameter paramId = new SqlParameter("@Id", employee.EmployeeID);
                    cmd.Parameters.Add(paramId);
                    SqlParameter paramFirstName = new SqlParameter("@FirstName", employee.FirstName);
                    cmd.Parameters.Add(paramFirstName);
                    SqlParameter paramLastName = new SqlParameter("@LastName", employee.LastName);
                    cmd.Parameters.Add(paramLastName);
                    SqlParameter paramEmail = new SqlParameter("@Email", employee.Email);
                    cmd.Parameters.Add(paramEmail);
                    SqlParameter paramDateOfBirth = new SqlParameter("@DateOfBirth", employee.DateOfBirth);
                    cmd.Parameters.Add(paramDateOfBirth);
                    SqlParameter paramSalary = new SqlParameter("@Salary", employee.Salary);
                    cmd.Parameters.Add(paramSalary);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка - {ex.Message}");
            }
        }
    }
}