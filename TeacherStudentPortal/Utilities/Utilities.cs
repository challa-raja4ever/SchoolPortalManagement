using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using TeacherStudentPortal.Models;

namespace TeacherStudentPortal.Utilities
{
    public class Utilities
    {
        public bool RegisterAdmin(RegisterViewModel model)
        {
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand("AddNewAdmin",connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", model.Name);
                command.Parameters.AddWithValue("@Email", model.Email);
                command.Parameters.AddWithValue("@Username", model.Username);
                command.Parameters.AddWithValue("@Password", model.Password);
                connection.Open();
                int i = command.ExecuteNonQuery();
                connection.Close();
                if (i > 0) return true;

            }
            return false;
        }

        public bool ValidateAdminLogin(LoginViewModel model)
        {
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand("ValidateAdminLogin", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", model.Username);
                command.Parameters.AddWithValue("@Password", model.Password);
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                connection.Close();
                if(dataSet.Tables[0].Rows.Count > 0)
                    return true;
            }
            return false;
        }

        public bool ValidateTeacherLogin(LoginViewModel model)
        {
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand("ValidateTeacherLogin", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", model.Username);
                command.Parameters.AddWithValue("@Password", model.Password);
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                connection.Close();
                if (dataSet.Tables[0].Rows.Count > 0)
                    return true;
            }
            return false;
        }

        public bool ValidateStudentLogin(LoginViewModel model)
        {
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand("ValidateStudentLogin", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", model.Username);
                command.Parameters.AddWithValue("@Password", model.Password);
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                connection.Close();
                if (dataSet.Tables[0].Rows.Count > 0)
                    return true;
            }
            return false;
        }

        public bool InsertTeacherRecord(AddTeacherModel model)
        {
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand("AddNewTeacher", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", model.Name);
                command.Parameters.AddWithValue("@Email", model.Email);
                command.Parameters.AddWithValue("@CourseTeaching", model.CourseTeaching);
                command.Parameters.AddWithValue("@CourseId", model.CourseId);
                command.Parameters.AddWithValue("@Username", model.Username);
                command.Parameters.AddWithValue("@Password", model.Password);
                connection.Open();
                try
                {
                    int i = command.ExecuteNonQuery();
                    connection.Close();
                    if (i > 0) return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return false;
        }

        public bool InsertStudentRecord(AddStudentModel model)
        {
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand("AddNewStudent", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", model.Name);
                command.Parameters.AddWithValue("@Email", model.Email);
                command.Parameters.AddWithValue("@StudentId", model.StudentId);
                command.Parameters.AddWithValue("@Username", model.Username);
                command.Parameters.AddWithValue("@Password", model.Password);
                connection.Open();
                try
                {
                    int i = command.ExecuteNonQuery();
                    connection.Close();
                    if (i > 0) return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return false;
        }
        public bool AssignGrade(AssignGradeModel model)
        {
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand("AssignGrade", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@StudentId", model.StudentId);
                command.Parameters.AddWithValue("@CourseId", model.CourseId);
                command.Parameters.AddWithValue("@Grade", model.Grade);
                connection.Open();
                try
                {
                    int i = command.ExecuteNonQuery();
                    connection.Close();
                    if (i > 0) return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return false;
        }

        public AdminViewModel GetStudentsAndTeachersList()
        {
            AdminViewModel model = new AdminViewModel();
            using (SqlConnection connection =
                new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand("GetStudentsAndTeachers", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                DataTable teachersDataSetTable = dataSet.Tables[0];
                List<TeacherCourse> teachers = new List<TeacherCourse>();
                foreach (var row in teachersDataSetTable.Rows)
                {
                    TeacherCourse teacherCourse = new TeacherCourse();

                    teacherCourse.TeacherName = ((DataRow)row).ItemArray[0].ToString();
                    teacherCourse.CourseTaught = ((DataRow)row).ItemArray[1].ToString();
                    teachers.Add(teacherCourse);
                }
                DataTable studentsDataSetTable = dataSet.Tables[1];
                List<string> students = new List<string>();
                foreach (var row in studentsDataSetTable.Rows)
                {
                    students.Add(((DataRow)row).ItemArray[0].ToString());
                }

                model.StudentList = students;
                model.TeachersList = teachers;
                connection.Close();
            }
            return model;
        }

        public TeacherViewModel GetStudentsList()
        {
            TeacherViewModel teacherViewModel = new TeacherViewModel();
            using (SqlConnection connection =
                new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var command = new SqlCommand("GetStudentsList", connection) {CommandType = CommandType.StoredProcedure};
                connection.Open();
                var adapter = new SqlDataAdapter(command);
                var dataSet = new DataSet();
                adapter.Fill(dataSet);
                var studentsDataSetTable = dataSet.Tables[0];
                var students = new List<Student>();
                foreach (var row in studentsDataSetTable.Rows)
                {
                    var student = new Student
                    {
                        Id = int.Parse(((DataRow) row).ItemArray[0].ToString()),
                        Name = ((DataRow) row).ItemArray[1].ToString()
                    };
                    students.Add(student);
                }
                connection.Close();
                teacherViewModel.Students = students;
            }
            return teacherViewModel;
        }

        public int FetchCourseIdTaughtByTeacher(string modelUsername)
        {
            int courseId = 0;
            using (SqlConnection connection =
                new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand myCommand = new SqlCommand("Select CourseId from dbo.Teachers where username = '" + modelUsername + "'", connection);
                    using (var reader = myCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (Convert.ToInt32(reader["CourseId"]) > 0)
                            {
                                courseId = Convert.ToInt32(reader["CourseId"]);
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return courseId;
        }

        public int FetchStudentId(string modelUsername)
        {
            int studentId = 0;
            using (SqlConnection connection =
                new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand myCommand = new SqlCommand("Select StudentId from dbo.Students where username = '" + modelUsername + "'", connection);
                    using (var reader = myCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (Convert.ToInt32(reader["StudentId"]) > 0)
                            {
                                studentId = Convert.ToInt32(reader["StudentId"]);
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return studentId;
        }

        public StudentViewModel GetCourseGrades(int studentId)
        {
            var studentViewModel = new StudentViewModel();
            using (SqlConnection connection =
                new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var command =
                    new SqlCommand("GetStudentCourseGrades", connection) {CommandType = CommandType.StoredProcedure};
                command.Parameters.AddWithValue("@StudentId", studentId);
                connection.Open();
                var adapter = new SqlDataAdapter(command);
                var dataSet = new DataSet();
                adapter.Fill(dataSet);
                var listOfCourses = dataSet.Tables[0];
                var coursesList = new List<CoursesList>();
                foreach (var row in listOfCourses.Rows)
                {
                    var course = new CoursesList
                    {
                        Id = int.Parse(((DataRow) row).ItemArray[0].ToString()),
                        Name = ((DataRow) row).ItemArray[1].ToString()
                    };
                    coursesList.Add(course);
                }
                studentViewModel.CoursesList = coursesList;
                var studentGradesDataSetTable = dataSet.Tables[1];
                var coursesGradeses = new List<CoursesGrades>();
                foreach (var row in studentGradesDataSetTable.Rows)
                {
                    var coursesGrades =
                        new CoursesGrades
                        {
                            CourseId = int.Parse(((DataRow) row).ItemArray[0].ToString()),
                            CourseName = ((DataRow) row).ItemArray[1].ToString(),
                            Grade = ((DataRow) row).ItemArray[2].ToString()
                        };
                    coursesGradeses.Add(coursesGrades);
                }

                studentViewModel.CourseGrades = coursesGradeses;
                connection.Close();
            }
            return studentViewModel;
        }

        public string GetStudentName(int studentId)
        {
            string studentName = "";
            using (SqlConnection connection =
                new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand myCommand = new SqlCommand("Select Name from dbo.Students where StudentId = " + studentId, connection);
                    using (var reader = myCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            studentName = reader["Name"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return studentName;
        }

        public string GetTeacherName(int courseId)
        {
            string teacherName = "";
            using (SqlConnection connection =
                new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand myCommand = new SqlCommand("Select Name from dbo.Teachers where CourseId = " + courseId, connection);
                    using (var reader = myCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            teacherName = reader["Name"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return teacherName;
        }
    }
}