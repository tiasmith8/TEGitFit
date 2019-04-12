﻿using SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    public class ProfileSqlDAO : IProfileDAO
    {
        private string connectionString;

        public ProfileSqlDAO(string databaseConnectionString)
        {
            connectionString = databaseConnectionString;
        }
        /// <summary>
        /// Creates a user profile and saves it to the database.
        /// </summary>
        /// <param name="profile"></param>
        public void CreateProfile(Profile profile)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Open the connection
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO user_profiles VALUES (@userId, @name, " +
                        "@currentWeight, @goalWeight, @birthDate, @height, @activityLevel, @gender);", conn);
                    cmd.Parameters.AddWithValue("@userId", profile.UserId);
                    cmd.Parameters.AddWithValue("@name", profile.Name);
                    cmd.Parameters.AddWithValue("@currentWeight", profile.CurrentWeight);
                    cmd.Parameters.AddWithValue("goalWeight", profile.GoalWeight);
                    cmd.Parameters.AddWithValue("@age", profile.BirthDate);
                    cmd.Parameters.AddWithValue("@height", profile.Height);
                    cmd.Parameters.AddWithValue("@activityLevel", profile.ActivityLevel);
                    cmd.Parameters.AddWithValue("@gender", profile.Gender);

                    cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets a user's profile from the user's id number.
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public Profile GetProfile(int currentUserId)
        {
            Profile profile = new Profile();
            string sql = "SELECT * FROM user_profiles WHERE userId = @userId;";
            try
            {
                using(SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userId", currentUserId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        profile.UserId = Convert.ToInt32(reader["userId"]);
                        profile.Name = Convert.ToString(reader["[name]"]);
                        profile.CurrentWeight = Convert.ToInt32(reader["currentWeight"]);
                        profile.GoalWeight = Convert.ToInt32(reader["goalWeight"]);
                        profile.BirthDate = Convert.ToDateTime(reader["birthDate"]);
                        profile.Height = Convert.ToInt32(reader["height"]);
                        profile.ActivityLevel = Convert.ToString(reader["activityLevel"]);
                        profile.Gender = Convert.ToChar(reader["gender"]);
                    }

                }
            }
            catch(SqlException ex)
            {
                throw;
            }
            return profile;
        }
    }
}
