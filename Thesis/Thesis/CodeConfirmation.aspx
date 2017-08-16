<%@ Page Title="Code Confirmation" Language="C#" MasterPageFile="~/Masters/LoginMaster.Master" AutoEventWireup="true" CodeBehind="CodeConfirmation.aspx.cs" Inherits="Thesis.CodeConfirmation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <p class="title" style="font-family:Megafont; margin-left:35px;">Code Confirmation<asp:LinkButton ID="mainPage" runat="server" Text="×" OnClick="goBackLogin_Click" style="font-size:large; float:right;"/></p>
    <label for="codeConfirmTextbox">Enter Code:</label>
            <asp:TextBox ID="codeConfirmTextbox" name="codeConfirmTextbox" ClientIDMode="Static" runat="server" placeholder="Enter Code" required="required"/>
            <asp:Panel runat="server" ID="invalidCodePanel">
                <asp:Label ID="invalidCodeLabel" runat="server" Visible="false"></asp:Label>
            </asp:Panel>
		
        <button>
          <i class="spinner"></i>
          <span class="state"><asp:LinkButton ID="codeConfirmButton" runat="server" OnClick="codeConfirmButton_Click" style="color:white; font-size:larger;">Submit Code</asp:LinkButton></span>
        </button>
		
        <asp:LinkButton ID="goBackLogin" OnClick="goBackLogin_Click" runat="server" style="color:orange; font-size:medium; float:right;">Go Back to Main Page</asp:LinkButton>


     <script>
         document.getElementById('<%=codeConfirmTextbox.ClientID%>').addEventListener("keyup", function (event) {
             event.preventDefault();
             if (event.keyCode == 13) {
                 console.log("enter pressed");
                 document.getElementById('<%=codeConfirmButton.ClientID%>').click();
             }
         });
    </script>
</asp:Content>
