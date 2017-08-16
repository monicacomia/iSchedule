<%@ Page Title="Sign In Page" Language="C#" AutoEventWireup="true" CodeBehind="SignInPage.aspx.cs" MasterPageFile="~/Masters/LoginMaster.Master" 
    Inherits="Thesis.SignInPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <p class="title" style="font-family:Megafont; margin-left:35px;">Login Account</p>
    <asp:Panel runat="server" ID="Panel8">
    <asp:Label ID="invalidLoginLabel" runat="server" Visible="false"></asp:Label>
    </asp:Panel>
     <asp:TextBox ID="facultyIDTxtbox" name="facultyIdSignIn" ClientIDMode="Static" runat="server" placeholder="Faculty ID" required="required" autofocus/>
    <%--<input type="text" placeholder="Username" autofocus/>--%>
    <i class="fa fa-user"></i>
    <asp:TextBox ID="password" name="password"  runat="server" placeholder="Password" required="required" TextMode="Password"/>
    <%--<input type="password" placeholder="Password" />--%>
    <i class="fa fa-key"></i>
    <asp:LinkButton ID="forgotPasswordButton" runat="server" Text="Forgot your Password?" OnClick="forgotPasswordButton_Click" style="color:cadetblue; font-size:small; float:right;"/>
    <button>
      <i class="spinner"></i>
      <span class="state"><asp:LinkButton ID="logInButton" runat="server" OnClick="logInButton_Click" style="color:white; font-size:larger;">Log In</asp:LinkButton></span>
    </button>

     <script>
         document.getElementById('<%=password.ClientID%>').addEventListener("keyup", function (event) {
             event.preventDefault();
             if (event.keyCode == 13) {
                 console.log("enter pressed");
                 document.getElementById('<%=logInButton.ClientID%>').click();
                                    }
                                });
    </script>
    
  <%--<footer><a target="blank" href="http://boudra.me/">boudra.me</a></footer>--%>
</asp:Content>
