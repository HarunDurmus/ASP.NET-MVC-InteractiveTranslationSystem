using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Tercume.Entities;

namespace Tercume.WebApp.Models
{
    public static class HelperFunction
    {
        public static string connectionString = "Server=APP-BILGISAYAR\\MSSQLEXPRESS;Database=TercumeDb;Integrated Security = SSPI";
        
        public static IEnumerable<ViewModel> dil(Translate t)
        {
            string kaynak_dil = t.KaynakDil;
            string hedef_dil = t.HedefDil;
            ViewModel model = new ViewModel();
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("", conn))
            {
                cmd.CommandText = "SELECT * FROM  Tercumanlar as tercuman,DilTercumen as dt WHERE dt.Tercuman_Id = tercuman.Id and dt.Dil_Id = all( SELECT d1.Id FROM Diller d1 WHERE d1.Dil_isim=@kaynakdil and d1.Id = all( SELECT dd.Id FROM Diller as dd WHERE dd.Dil_isim=@hedefdil))";
                conn.Open();
                cmd.Parameters.AddWithValue("@kaynakdil", kaynak_dil);
                cmd.Parameters.AddWithValue("@hedefdil", hedef_dil);
                SqlDataReader reader = cmd.ExecuteReader();
                model.Name = reader["Name"].ToString();
                model.Surname = reader["Surname"].ToString();
                model.ProfileImageFilename = reader["ProfileImageFilename"].ToString();
                
            }
            yield return model;
        }
    }
}