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

    public partial class Dashboard : System.Web.UI.Page
    {
        private String connString;
        SqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            int activeCount = 0;
            int overDueCount = 0;
            int paidCount = 0;

            int customerCount = 0;

            int totalBorrowed = 0, totalPaid = 0, totalDue = 0;

            

            //resetting labels
            string tableHtml = "<table class='table table-hover'><thead><th>First Name</th><th>Last Name</th><th>Principal</th><th>Due Amount</th><th>Amount Paid</th><th>Request Date</th><th>Due Date</th><th></th></thead>";
            lblActive.Text = tableHtml;
            lblOverDue.Text = tableHtml;
            lblPaid.Text = tableHtml;

            Label1.Text = $"<h2>Welcome Back, {Session["Username"]}</h2>";
            

            



                if (Session["Username"] == null || Session["ID"] == null)
                {
                    Response.Redirect("~/");
                }else
                {
                    connString = WebConfigurationManager.ConnectionStrings["localDatabase"].ToString();
                    conn = new SqlConnection(connString);

                img_URL.ImageUrl = Session["imageURL"].ToString();

                int officer_id = Convert.ToInt32(Session["ID"]);
                    try
                    {
                        conn.Open();

                        string query = $"SELECT c.first_name, c.last_name, l.Id, l.request_date, l.principal, l.due_amount, l.due_date, l.amount_paid, l.last_paid_date from customer AS c INNER JOIN loan As l ON c.Id = l.customer_id WHERE l.officer_id = {officer_id}";

                        SqlCommand cmd = new SqlCommand(query, conn);

                        SqlDataReader reader = cmd.ExecuteReader();

                        /*rptContent.DataSource = reader;
                        rptContent.DataBind();*/
                        while (reader.Read())
                        {
                            customerCount++;
                            totalBorrowed += Convert.ToInt32(reader["principal"]);
                            totalPaid += Convert.ToInt32(reader["amount_paid"]);
                            totalDue += Convert.ToInt32(reader["due_amount"]);

                        DateTime dueDate = DateTime.Parse(reader        ["due_date"].ToString());
                            DateTime currentDate = DateTime.Now;

                            double principal = Convert.ToDouble(reader["principal"]);
                            double amount_paid = Convert.ToDouble(reader["amount_paid"]);

                            string ahref = $"LoanDetails.aspx?id={reader["Id"]}";
                            string html = $"<tr> <td>{reader["first_name"]}</td> <td>{reader["last_name"]}</td> <td>{reader["principal"]}</td> <td>{reader["due_amount"]}</td> <td>{reader["amount_paid"]}</td>      <td>{reader["request_date"].ToString().Substring(0, 10)}</td>   <td>{reader["due_date"].ToString().Substring(0,10)}</td>   <td><a href={ahref} class='btn btn-primary'>Add Payment</a></td>  </tr>";

                            if(amount_paid >= principal)
                            {
                                lblPaid.Text += html;
                            paidCount++;
                            }else if(amount_paid <= principal && dueDate > currentDate) { 
                                lblActive.Text += html;
                            activeCount++;
                            }else if(amount_paid <= principal && dueDate <= currentDate)
                            {
                                lblOverDue.Text += html;
                            overDueCount++;
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        Label1.Text = ex.Message;
                    }finally
                {
                    conn.Close();
                    lblActive.Text += "</table>";
                    lblOverDue.Text += "</table>";
                    lblPaid.Text += "</table>";

                    lblSideBar.Text = $"<table class='table text-light'><tr><td>Customers</td><td><span class='btn btn-secondary'>{customerCount}</span></td></tr> <tr><td>Active Loans</td><td > <a href='#activeLoans'><span class='btn btn-primary'>{activeCount}</span></a></td></tr>   <tr><td>Paid Loans</td><td><a href='#paidLoans'> <span class='btn btn-success'>{paidCount}</span></a></td></tr>   <tr><td>OverDue Loans</td><td> <a href='#overDueLoans'> <span class='btn btn-danger'>{overDueCount}</span></a></td> </tr>  <tr> <td>Amount Borrowed </td>  <td>{totalBorrowed}</td> </tr>  <tr> <td>Amount Paid </td>  <td>{totalPaid}</td> </tr>  <tr> <td>Amount Due </td>  <td>{totalDue}</td> </tr></table>";
                }
                }

               


                
            
        }

        protected int CalculateTimeBetweenDates(string date1, string date2)
        {
            DateTime startDate = DateTime.Parse(date1);
            DateTime endDate = DateTime.Parse(date2);

            TimeSpan timeSpan = endDate - startDate;

            double totalYears = timeSpan.TotalDays / 365.25; // Considering leap years

            int years = (int)Math.Floor(totalYears);

            return years;

            // Use the 'years' value as needed
            //lblResult.Text = $"Time between dates: {years} years";
        }

        protected void btnNewCustomer_Click(object sender, EventArgs e)
        {
            connString = WebConfigurationManager.ConnectionStrings["localDatabase"].ToString();
            conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                string query = "INSERT INTO customer (first_name, last_name, trn, date_of_birth) OUTPUT INSERTED.ID VALUES (@FirstName, @LastName, @TRN, @DateOfBirth)";
                SqlCommand cmd = new SqlCommand(query, conn);

                // Set parameter values
                cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                cmd.Parameters.AddWithValue("@TRN", txtTRN.Text);
                cmd.Parameters.AddWithValue("@DateOfBirth", txtDateBirth.Text);

                int insertId = (int)cmd.ExecuteScalar(); // Retrieve the inserted ID

                int timeDiff = CalculateTimeBetweenDates(txtDateStart.Text, txtDateDue.Text);
                double amountDue = Convert.ToInt32(txtPrincipal.Text) * (1 + timeDiff * .25);

                string query2 = "INSERT INTO loan (customer_id, officer_id, request_date, principal, rate, due_date, due_amount, amount_paid, last_paid_date ) VALUES (@customer_id, @officer_id, @request_date, @principal, '0.25', @due_date, @amountDue, 0, '5/5/2023')";

                cmd = new SqlCommand(query2, conn);

                cmd.Parameters.AddWithValue("@customer_id", insertId);
                cmd.Parameters.AddWithValue("@officer_id", Session["ID"]);
                cmd.Parameters.AddWithValue("@request_date", txtDateStart.Text);
                cmd.Parameters.AddWithValue("@principal", txtPrincipal.Text);
                cmd.Parameters.AddWithValue("@due_date", txtDateDue.Text);
                cmd.Parameters.AddWithValue("@amountDue", amountDue);

                cmd.ExecuteNonQuery();

                Label1.Text = "Customer created successfully.";

                // Reset form fields if needed
                txtFirstName.Text = string.Empty;
                txtLastName.Text = string.Empty;
                txtTRN.Text = string.Empty;
                txtDateBirth.Text = string.Empty;
                // Reset other fields as needed
            }
            catch (Exception ex)
            {
                Label1.Text = "An error occurred: " + ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }

    }
}