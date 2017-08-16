<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Masters/LoginMaster.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Thesis.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <p class="title" style="font-family:Megafont; margin-left:35px;">Change Password<asp:LinkButton ID="mainPage" runat="server" Text="×" OnClick="goBackLogin_Click" style="font-size:large; float:right;"/></p>
    <label for="passwordChangeTextbox">Enter New Password:</label>
            <asp:TextBox ID="passwordTextbox" name="passwordTextbox" ClientIDMode="Static" runat="server" placeholder="Enter New Password" required="required" TextMode="Password"/>
            <asp:Panel runat="server" ID="passwordPanel">
                <asp:Label ID="passwordLabel" runat="server" Visible="false"></asp:Label>
            </asp:Panel>
    <label for="confirmPasswordTextbox">Confirm New Password:</label>
            <asp:TextBox ID="confirmPasswordTextbox" name="confirmPasswordTextbox" ClientIDMode="Static" runat="server" placeholder="Confirm New Password" required="required" TextMode="Password"/>
            <asp:Panel runat="server" ID="confirmPasswordPanel">
                <asp:Label ID="confirmPasswordLabel" runat="server" Visible="false"></asp:Label>
            </asp:Panel>
		
        <button>
          <i class="spinner"></i>
          <span class="state"><asp:LinkButton ID="passwordButton" runat="server" OnClick="passwordButton_Click" style="color:white; font-size:larger;">Change Password</asp:LinkButton></span>
        </button>
		
        <asp:LinkButton ID="goBackLogin" OnClick="goBackLogin_Click" runat="server" style="color:orange; font-size:medium; float:right;">Go Back to Main Page</asp:LinkButton>

    
</asp:Content>
