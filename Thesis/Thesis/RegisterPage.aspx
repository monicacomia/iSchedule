<%@ Page Title="Register Page" Language="C#" AutoEventWireup="true" CodeBehind="RegisterPage.aspx.cs" MasterPageFile="~/Masters/LoginMaster.Master" 
    Inherits="Thesis.RegisterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <p class="title" style="font-family:Megafont; margin-left:10px;">Register Account<asp:LinkButton ID="mainPage" runat="server" Text="×" OnClick="goBackLogin_Click" style="font-size:large; float:right;"/></p>
    <asp:Panel runat="server" ID="unauthorizedUserPanel">
    <asp:Label ID="unauthorizedUserLabel" runat="server" Visible="false"></asp:Label>
    </asp:Panel>

    <label for="facultyId">Faculty ID</label>
    <asp:TextBox ID="facultyId" name="facultyId" ClientIDMode="Static" runat="server" required="required"/>
    <%--<input type="text" placeholder="Username" autofocus/>--%>
    <asp:Panel runat="server" ID="Panel3">
    <asp:Label ID="facultyIdLabel" runat="server" Visible="false" style="font-style:italic; color:red; font-size:10px;"></asp:Label>
    </asp:Panel>
    <label for="accountType">Account Type</label>
                                        <asp:DropDownList ID="dropdownAccount" runat="server" CssClass="ddlAccount">
                                            <asp:ListItem Text="Select From Option" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Faculty" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Department Head" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Academics Assistant" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="VP Academics" Value="4"></asp:ListItem>
    </asp:DropDownList>
    <asp:Panel runat="server" ID="Panel2">
    <asp:Label ID="invalidDropdownSelection" runat="server" Visible="false" style="font-style:italic; color:red; font-size:10px;"></asp:Label>
    </asp:Panel>
    <br/><label for="firstName">Firstname</label>
    <asp:TextBox ID="firstName" name="firstName" ClientIDMode="Static" runat="server" required="required"/>
    <asp:Panel runat="server" ID="Panel1">
    <asp:Label ID="requiredFirstName" runat="server" Visible="false" style="font-style:italic; color:red; font-size:10px;"></asp:Label>
    </asp:Panel>
    <label for="lastName">Lastname</label>
    <asp:TextBox ID="lastName" name="lastName" ClientIDMode="Static" runat="server" required="required"/>
    <asp:Panel runat="server" ID="Panel4">
    <asp:Label ID="requiredLastName" runat="server" Visible="false" style="font-style:italic; color:red; font-size:10px;"></asp:Label>
    </asp:Panel>
    <label for="email">E-mail</label>
    <asp:TextBox ID="email" name="email" ClientIDMode="Static" runat="server" required="required"/>
    <asp:Panel runat="server" ID="Panel5">
    <asp:Label ID="emailLabel" runat="server" Visible="false" style="font-style:italic; color:red; font-size:10px;"></asp:Label>
    </asp:Panel>
    <label for="regPass">Password</label>
    <asp:TextBox ID="regPass" name="regPassword" ClientIDMode="Static" runat="server" required="required" TextMode="Password"/>
    <asp:Panel runat="server" ID="Panel6">
    <asp:Label ID="requiredPassword" runat="server" Visible="false" style="font-style:italic; color:red; font-size:10px;"></asp:Label>
    </asp:Panel>
    <label for="confirmPass">Confirm Password</label>
    <asp:TextBox ID="confirmPass" name="confirmPass" ClientIDMode="Static" runat="server" required="required" TextMode="Password"/>
    <asp:Panel runat="server" ID="Panel7">
    <asp:Label ID="confirmPasswordMatch" runat="server" Visible="false" style="font-style:italic; color:red; font-size:10px;"></asp:Label>
    </asp:Panel>


    <%--<asp:TextBox ID="password" name="password"  runat="server" placeholder="Password" required="required" TextMode="Password"/>--%>
    <%--<input type="password" placeholder="Password" />--%>
    <%--<i class="fa fa-key"></i>--%>
    <asp:LinkButton ID="resetRegButton" OnClick="resetRegButton_Click" runat="server" style="color:gray; font-size:medium; float:left;">Reset Form?  </asp:LinkButton>
    <asp:LinkButton ID="goBackLogin" OnClick="goBackLogin_Click" runat="server" style="color:orange; font-size:medium; float:right;">Go Back to Main Page</asp:LinkButton>
    <button>
      <i class="spinner"></i>
      <span class="state"><asp:LinkButton ID="registerButton" OnClick="registerButton_Click" class="special" runat="server" style="color:white; font-size:larger;">Register</asp:LinkButton></span>
    </button>


    
  <%--<footer><a target="blank" href="http://boudra.me/">boudra.me</a></footer>--%>
</asp:Content>