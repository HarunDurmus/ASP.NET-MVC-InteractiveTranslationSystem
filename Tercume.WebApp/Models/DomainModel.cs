using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Tercume.Entities;

namespace Tercume.WebApp.Models
{
    public class DomainModel
    {
        public string connectionString = "Server=APP-BILGISAYAR\\MSSQLEXPRESS;Database=TercumeDb;Integrated Security = SSPI";
        public void CreateSomething(ViewModel model)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("", connection))
            {
                command.CommandText = "insert into Names values(@Name)";
                command.Parameters.AddWithValue("@Name", model.Name);
                command.ExecuteNonQuery();
            }
        }

        public ViewModel FindSomething(Translate t)
        {
            string kaynak_dil = t.KaynakDil;
            string hedef_dil = t.HedefDil;

            ViewModel model = new ViewModel();
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("WITH EmpCountByCountry (kaynak) AS(" +
                                                                            " select id1.Id as kaynak from[TercumeDb].[dbo].[Diller] as id1 where Dil_isim =@kaynakdil"+
                                                                            " union"+
                                                                            " select id2.Id as hedef from[TercumeDb].[dbo].[Diller] as id2 where Dil_isim =@hedefdil)" +
                                                                            " SELECT * FROM[TercumeDb].[dbo].[DilTercumen] dt1, EmpCountByCountry e where dt1.Dil_Id = e.kaynak" +
                                                                            " intersect" +
                                                                            " SELECT * FROM[TercumeDb].[dbo].[DilTercumen] dt2, EmpCountByCountry e2 where dt2.Dil_Id = e2.kaynak", connection))
            {
                connection.Open();
                
                command.Parameters.AddWithValue("@kaynakdil", kaynak_dil);
                command.Parameters.AddWithValue("@hedefdil", hedef_dil);
                SqlDataReader reader = command.ExecuteReader();
                model.Name = reader["Tercuman_Id"].ToString();
                //model.Surname = reader["Surname"].ToString();
                //model.ProfileImageFilename = reader["p"].ToString();
            }



            return model;
        }

        


        //public void DeleteSomething(ViewModel model)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    using (SqlCommand command = new SqlCommand("", connection))
        //    {
        //        command.CommandText = "delete from Names where Id=@Id";
        //        command.Parameters.AddWithValue("@Id", model.Id);
        //        command.ExecuteNonQuery();
        //    }
        //}

        //public void EditSomething(ViewModel model)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    using (SqlCommand command = new SqlCommand("", connection))
        //    {
        //        command.CommandText = "Update Names set Name=@Name where Id=@Id";
        //        command.Parameters.AddWithValue("@Name", model.Name);
        //        command.Parameters.AddWithValue("@Id", model.Id);
        //        command.ExecuteNonQuery();
        //    }
        //}
    }
}