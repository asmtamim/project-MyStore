using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        { 
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];

            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0 ||
                clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
            {
                errorMessage = "All fields are required!";
                return;
            }

            try
            {
                string connectionString = "Data Source=TAM1M\\DESHERP;Initial Catalog=dbMyStore;User ID=sa;Password=117734";
                using (SqlConnection sCon = new SqlConnection(connectionString))
                {
                    sCon.Open();
                    string sql = "INSERT INTO clients" + "(name, email, phone, address) VALUES" + 
                                 "(@name, @email, @phone, @address);";

                    using (SqlCommand sCmd = new SqlCommand(sql, sCon))
                    {
                        sCmd.Parameters.AddWithValue("@name",clientInfo.name);
                        sCmd.Parameters.AddWithValue("@email", clientInfo.email);
                        sCmd.Parameters.AddWithValue("@phone", clientInfo.phone);
                        sCmd.Parameters.AddWithValue("@address", clientInfo.address);

                        sCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            } 

            clientInfo.name = "";
            clientInfo.email = "";
            clientInfo.phone = "";
            clientInfo.address = "";

            successMessage = "New contact is added successfully!";

            Response.Redirect("/Clients/Index");
        }
    }
}
