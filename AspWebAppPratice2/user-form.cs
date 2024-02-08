using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspWebAppPratice2
{
    public partial class user_form : System.Web.UI.Page
    {
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            userInput.Text = UserName.Text;
            lastInput.Text = LastName.Text;
            
            genderInput.Text = "";
            if (RadioButton1.Checked)
            {
                genderInput.Text = "Your gender is " + RadioButton1.Text;
            }
            else genderInput.Text = "Your gender is " + RadioButton2.Text;

            checkBoxInput.Text = "";
            var message = "";
            if (CheckBox1.Checked)
            {
                message = CheckBox1.Text + " ";
            }
            if (CheckBox2.Checked)
            {
                message += CheckBox2.Text + " ";
            }
            if (CheckBox3.Checked)
            {
                message += CheckBox3.Text;
            }
            checkBoxInput.Text = message;

            DropDownInput.Text = "";

            if (DropDownList1.SelectedValue == "")
            {
                DropDownInput.Text = "Please Select a City";
            }
            else
            {
                DropDownInput.Text = "Your Choice is: " + DropDownList1.SelectedValue;
            }

        }
    }
}

