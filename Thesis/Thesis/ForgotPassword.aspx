<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/LoginMaster.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="Thesis.ForgotPassword1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function sentEmailMsg() {
            $.growl.notice({ title: "Sent!", message: "E-mail has been sent successfully." });
        }
        function unsentEmailMsg() {
            $.growl.notice({ title: "Email not sent!", message: "There was a problem in sending the code. Please check your internet connection." });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <p class="title" style="font-family:Megafont; margin-left:35px;">Forgot Password<asp:LinkButton ID="mainPage" runat="server" Text="×" OnClick="goBackLogin_Click" style="font-size:large; float:right;"/></p>
    <label for="facultyId">Please Enter Faculty ID:</label>
    <asp:TextBox ID="facultyIdTxtbox" name="facultyIdTxtbox" ClientIDMode="Static" runat="server" placeholder="Faculty ID" required="required"/>
		
        <button>
          <i class="spinner"></i>
          <span class="state"><asp:LinkButton ID="recoverPasswordButton" runat="server" OnClick="recoverPasswordButton_Click" style="color:white; font-size:larger;">Recover Password</asp:LinkButton></span>
        </button>
		
        <asp:LinkButton ID="goBackLogin" OnClick="goBackLogin_Click" runat="server" style="color:orange; font-size:medium; float:right;">Go Back to Main Page</asp:LinkButton>


     <script>
         document.getElementById('<%=facultyIdTxtbox.ClientID%>').addEventListener("keyup", function (event) {
             event.preventDefault();
             if (event.keyCode == 13) {
                 console.log("enter pressed");
                 document.getElementById('<%=recoverPasswordButton.ClientID%>').click();
             }
         });
    </script>
</asp:Content>
