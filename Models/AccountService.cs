using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Airline.Models
{
    public class AccountService
    {
        private readonly SqlQueryHelper _queryHelper;
        private readonly SqlConnection _connection;

        public AccountService()
        {
            _queryHelper = new SqlQueryHelper();
            _connection = _queryHelper.GetConnectionObject();
        }

        public string AuthenticatePassenger(string email, string password)
        {
            var query = "SELECT Passport_No FROM passenger WHERE Email = @Email AND password = @Password";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Email", email),
                new SqlParameter("@Password", password)
            };

            var result = _queryHelper.MakeCommandWithReturn(query, _connection, parameters, "Custom", 1);
            return result.Count > 0 ? result[0][0].ToString() : null;
        }

        public string AuthenticateEmployee(string email, string password)
        {
            var query = "SELECT emp_id FROM employee WHERE contact_info = @Email AND password = @Password";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Email", email),
                new SqlParameter("@Password", password)
            };

            var result = _queryHelper.MakeCommandWithReturn(query, _connection, parameters, "Custom", 1);
            return result.Count > 0 ? result[0][0].ToString() : null;
        }

        public bool UpdatePassword(string passportNo, string newPassword)
        {
            var query = "UPDATE passenger SET password = @password WHERE Passport_No = @passportNo";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@password", newPassword),
                new SqlParameter("@passportNo", passportNo)
            };

            return _queryHelper.MakeCommandWithoutReturn(query, parameters, _connection) > 0;
        }
    }
}