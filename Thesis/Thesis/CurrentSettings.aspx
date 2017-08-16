<%@ Page Title="Current Settings" Language="C#" MasterPageFile="~/Masters/DefaultMaster.Master" AutoEventWireup="true" CodeBehind="CurrentSettings.aspx.cs" Inherits="Thesis.UploadSchedule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
    .margin{
        margin:20px;
    }

    .mainrow{
        width:400px;
    }

    .controls{
       text-align:right;
       
      
    }

    /*.no-gutter > [class*='col-'] {
    padding-right:0;
    padding-left:0;
    }*/

    /*.ddlalign{
        text-align:left;
    }*/

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div id="page-inner">
                 <div class="row">
           <div class="col-md-12">
                <h2 style="color:deepskyblue; padding-left:15px; font-family:Megafont;">Current Settings</h2>  
            </div>
            </div>
    <hr/>   
     <div class="panel panel-primary">
             <div class="panel panel-body">
    <div class="margin">
            
            
            

            <div class="row">
            <div class="col-md-12">
            
            <asp:Label ID="Label1" runat="server" CssClass="col-md-2 labels"  Text="Semester:"></asp:Label>
            <asp:DropDownList ID="ddlSemester" runat="server" Enabled="false">
                <asp:ListItem Text="" Value="0"></asp:ListItem>
                <asp:ListItem Text="1st" Value="1"></asp:ListItem>
                <asp:ListItem Text="2nd" Value="2"></asp:ListItem>
                <asp:ListItem Text="3rd" Value="3"></asp:ListItem> 
            </asp:DropDownList>
            </div>
            </div>

            <div class="row">
            <div class="col-md-12">
            
            <asp:Label ID="Label2" runat="server" CssClass="col-md-2" Text="School Year:"></asp:Label>
            <%--<asp:TextBox ID="schoolYearTextBox" runat="server" ReadOnly="true" CssClass="col-md-3 controls" Width="100px"></asp:TextBox>--%>
            <div class="input-group input-daterange" data-provide="datepicker" data-date-format="yyyy" data-date-max-view-mode="years" data-date-min-view-mode="years">
                    <asp:TextBox ID="schoolYearStart" runat="server"></asp:TextBox>
                    <asp:Label ID="toLabel" runat="server">to</asp:Label>
                    <asp:TextBox ID="schoolYearEnd" runat="server"></asp:TextBox>
            </div>
            </div>
            </div>        

            <div class="row">
            <div class="col-md-12">
            
            <asp:Label ID="Label3" runat="server" CssClass="col-md-2" Text="Duration:"></asp:Label>
            <div class="input-group input-daterange" data-provide="datepicker" data-date-format="yyyy-mm-dd" data-date-max-view-mode="days" data-date-min-view-mode="days">
                    <asp:TextBox ID="durationStart" runat="server"></asp:TextBox>
                    <asp:Label ID="tolbl" runat="server">to</asp:Label>
                    <asp:TextBox ID="durationEnd" runat="server"></asp:TextBox>
            </div>
            </div>
            </div>

            

         
            
            
    </div>
         
    <div style="margin:20px;"> 
            <asp:Button ID="editCurrentSettingsButton" runat="server" Text="Edit" OnClick="editCurrentSettingsButton_Click" />
            <asp:Button ID="confirmButton" runat="server" Text="Confirm" Visible="false" OnClick="confirmButton_Click" />
            <asp:Button ID="cancelButton" runat="server" Text="Cancel" Visible="false" OnClick="cancelButton_Click" />
    </div>
                 </div>
         </div>
</div>
</asp:Content>
