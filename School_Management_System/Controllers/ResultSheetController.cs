using School_Management_System.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace School_Management_System.Controllers
{
    public class ResultSheetController : Controller
    {
        // GET: ResultSheet
        public ActionResult Index()
        {
            var listSection = new List<VmSection>();
            string ECommerce = System.Configuration.ConfigurationManager.ConnectionStrings["ECommerce"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ECommerce))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT [SectionId],[SectionName] FROM Section", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var oVmSection = new VmSection();
                        oVmSection.SectionId = reader["SectionId"] == null ? 0 : Convert.ToInt32(reader["SectionId"]);
                        oVmSection.SectionName = reader["SectionName"] == null ? "" : Convert.ToString(reader["SectionName"]);
                        listSection.Add(oVmSection);
                    }
                }
            }

            return View(listSection);

        }

        [HttpGet]
        public ActionResult GetResultSheet()
        {
            var listResultSheet = new List<VmResultSheet>();
            string ECommerce = System.Configuration.ConfigurationManager.ConnectionStrings["ECommerce"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ECommerce))
            {
                string query = @"SELECT R.StudentId
                , R.StudentName
                , R.Grade
                ,R.GPA
                , S.SectionId
                , S.SectionName 
                FROM ResultSheet R JOIN Section S ON R.SectionId = S.SectionId";
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var oVmResultSheet = new VmResultSheet();
                        oVmResultSheet.StudentId = reader["StudentId"].GetType().Name == "DBNull" ? 0 : Convert.ToInt32(reader["StudentId"]);
                        oVmResultSheet.StudentName = reader["StudentName"].GetType().Name == "DBNull" ? "" : Convert.ToString(reader["StudentName"]);
                        oVmResultSheet.Grade = reader["Grade"].GetType().Name == "DBNull" ? "" : Convert.ToString(reader["Grade"]);
                        oVmResultSheet.GPA = reader["GPA"].GetType().Name == "DBNull" ? 0 : Convert.ToDecimal(reader["GPA"]);
                        oVmResultSheet.SectionId = reader["SectionId"].GetType().Name == "DBNull" ? 0 : Convert.ToInt32(reader["SectionId"]);
                        oVmResultSheet.SectionName = reader["SectionName"].GetType().Name == "DBNull" ? "" : Convert.ToString(reader["SectionName"]);
                        listResultSheet.Add(oVmResultSheet);
                    }
                }
            }
            return Json(listResultSheet, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddResultSheet(List<VmResultSheet> ListResultSheet)
        {
            object result = null;
            string commandText = @"INSERT INTO [dbo].[ResultSheet]
                   ([StudentName]
                   ,[Grade]
                   ,[GPA]
                   ,[SectionId]
            )
             VALUES
                   (@StudentName
                   ,@Grade
                   ,@GPA
                   ,@SectionId
            )";

            string ECommerce = System.Configuration.ConfigurationManager.ConnectionStrings["ECommerce"].ConnectionString;

            Int32 rowsAffected = 0;
            foreach (var resultSheet in ListResultSheet)
            {
                using (SqlConnection connection = new SqlConnection(ECommerce))
                {
                    SqlCommand command = new SqlCommand(commandText, connection);
                    command.Parameters.Add("@StudentName", SqlDbType.NVarChar);
                    command.Parameters["@StudentName"].Value = resultSheet.StudentName;

                    command.Parameters.Add("@Grade", SqlDbType.NVarChar);
                    command.Parameters["@Grade"].Value = resultSheet.Grade;

                    command.Parameters.Add("@GPA", SqlDbType.Decimal);
                    command.Parameters["@GPA"].Value = resultSheet.GPA;

                    command.Parameters.Add("@SectionId", SqlDbType.Int);
                    command.Parameters["@SectionId"].Value = resultSheet.SectionId;

                    try
                    {
                        connection.Open();
                        rowsAffected += command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            result = new { message = rowsAffected + " Result added successfully." };
            return Json(result);
        }
        [HttpGet]
        public ActionResult GetResultSheetById(int id)
        {
            var oResult = new VmResultSheet();
            string ECommerce = System.Configuration.ConfigurationManager.ConnectionStrings["ECommerce"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ECommerce))
            {
                string query = @"SELECT R.StudentId
                , R.StudentName
                , R.Grade
                ,R.GPA
                , S.SectionId
                , S.SectionName 
                FROM ResultSheet R JOIN Section S ON R.SectionId = S.SectionId WHERE R.StudentId  = " + id;
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        oResult.StudentId = reader["StudentId"].GetType().Name == "DBNull" ? 0 : Convert.ToInt32(reader["StudentId"]);
                        oResult.StudentName = reader["StudentName"].GetType().Name == "DBNull" ? "" : Convert.ToString(reader["StudentName"]);
                        oResult.Grade = reader["Grade"].GetType().Name == "DBNull" ? "" : Convert.ToString(reader["Grade"]);
                        oResult.GPA = reader["GPA"].GetType().Name == "DBNull" ? 0 : Convert.ToDecimal(reader["GPA"]);
                        oResult.SectionId = reader["SectionId"].GetType().Name == "DBNull" ? 0 : Convert.ToInt32(reader["SectionId"]);
                        oResult.SectionName = reader["SectionName"].GetType().Name == "DBNull" ? "" : Convert.ToString(reader["SectionName"]);
                    }
                }
            }
            return Json(oResult, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateResultSheet(VmResultSheet resultSheet)
        {
            object result = null;
            string commandText = @"UPDATE [dbo].[ResultSheet]
               SET [StudentName] = @StudentName
                  ,[Grade] = @Grade
                  ,[GPA] = @GPA
                  ,[SectionId] = @SectionId
             WHERE StudentId = @StudentId";

            string ECommerce = System.Configuration.ConfigurationManager.ConnectionStrings["ECommerce"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(ECommerce))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@StudentName", resultSheet.StudentName);
                command.Parameters.AddWithValue("@Grade", resultSheet.Grade);
                command.Parameters.AddWithValue("@GPA", resultSheet.GPA);
                command.Parameters.AddWithValue("@SectionId", resultSheet.SectionId);
                command.Parameters.AddWithValue("@StudentId", resultSheet.StudentId);

                try
                {
                    connection.Open();
                    Int32 rowsAffected = command.ExecuteNonQuery();
                    result = new { message = " Result updated successfully." };
                    return Json(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return Json(result);
        }
        [HttpPost]
        public ActionResult DeleteResultSheet(int id)
        {
            object result = null;
            string commandText = @"DELETE FROM [ResultSheet] WHERE StudentId = @StudentId";

            string ECommerce = System.Configuration.ConfigurationManager.ConnectionStrings["ECommerce"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(ECommerce))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@StudentId", id);

                try
                {
                    connection.Open();
                    Int32 rowsAffected = command.ExecuteNonQuery();
                    result = new { message = " Result deleted successfully." };
                    return Json(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return Json(result);
        }

    }
}