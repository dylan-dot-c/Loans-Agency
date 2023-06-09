using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loans_Agency
{
    public partial class LoanDetails : System.Web.UI.Page
    {
        private String connString;
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null || Session["Id"] == null)
            {
                Response.Redirect("~/Default.aspx");

            }else
            {
                string id = Request.QueryString["id"];
                lblinfo.Text = $"{id} is ID";
                int officer_id = Convert.ToInt32(Session["ID"]);
                connString = WebConfigurationManager.ConnectionStrings["localDatabase"].ToString();
                conn = new SqlConnection(connString);
                
                try
                {
                    conn.Open();
                    string query = $"SELECT l.*, c.* from customer AS c INNER JOIN loan As l ON c.Id = l.customer_id WHERE l.officer_id = {officer_id} AND l.Id = '{id}' ";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    SqlDataReader copy = reader;

                    /*rptContent.DataSource = reader;*/


                    while (reader.Read())
                    {
                        lblheader.Text = $"Loan Details for {reader["first_name"]} {reader["last_name"]}";
                    }

                    reader.Close();

                    reader = cmd.ExecuteReader();
                    rptContent.DataSource = reader;
                    rptContent.DataBind();
                }
                catch (Exception ex)
                {
                    lblinfo.Text = ex.Message;
                }
            }
        }

        protected void btnAddPayment_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            connString = WebConfigurationManager.ConnectionStrings["localDatabase"].ToString();
            conn = new SqlConnection(connString);
            int currentValue = 0;

            try
            {
                //getting value
                string selectQuery = $"SELECT amount_paid FROM loan WHERE Id = {id}";
                SqlCommand command = new SqlCommand(selectQuery, conn);
                conn.Open();
                currentValue = Convert.ToInt32(command.ExecuteScalar());
            }
            catch(Exception ex)
            {
                lblinfo.Text += ex.Message;
            }
            finally
            {
                conn.Close();
            }

            try
            {
                int newValue = Convert.ToInt32(txtPayment.Text) + currentValue;
                conn.Open();

                string query = $"UPDATE loan SET amount_paid = '{newValue}', last_paid_date = '{txtDatePaid.Text}' WHERE Id = {id} ";

                SqlCommand cmd = new SqlCommand(query, conn);

                int value = cmd.ExecuteNonQuery();

                lblinfo.Text = $"{value} Customer has been updated";
            }catch (Exception ex) { 
                lblinfo.Text=ex.Message;
            }
        }
    }
}