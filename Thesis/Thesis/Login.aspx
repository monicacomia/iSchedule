<%@ Page Title="" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Thesis.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    
    <div class="row">
        <div class="col-md-4"></div>
                <div class="col-md-4">
                    <div class="parent">
                        
                                
                                <div>
                                <p align="center">    
                                       <%--<asp:Label runat="server">Email:</asp:Label>--%>                                   
                                       <asp:TextBox ID="TextBox1" runat="server" Text="Email"></asp:TextBox>
                                </p>    
                                </div>
                                 
                                <div>
                                    
                                 <p align="center">
                                        <%--<asp:Label ID="Label1" runat="server">Password:</asp:Label>--%>     
                                       <asp:TextBox ID="TextBox2" runat="server" Text="Password" TextMode="Password"></asp:TextBox>
                                  </p>  
                                </div>
                                <p align="center">
                                <asp:LinkButton ID="loginButton" runat="server" Text="Login" CssClass="btn btn-primary" OnClick="loginButton_Click"></asp:LinkButton>
                                </p>
                       


                    </div>
                     


                </div>
        <div class="col-md-4"></div>
       
    </div>
</asp:Content>
