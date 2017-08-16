<%@ Page Title="Upload Page" Language="C#" MasterPageFile="~/Masters/DefaultMaster.Master" AutoEventWireup="true" CodeBehind="UploadSchedule.aspx.cs" Inherits="Thesis.UploadSchedule1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
    .col-md-4{
        text-align:center;
    }

    </style>

    <script>
        function showErrorMsg() {

            $.growl.error({title:"Invalid File!", message: "Please use .xlsx file type only" });
        }

        function showSuccessMsg() {

            $.growl.notice({title:"Success!", message: "The file was uploaded successfully" });
        }
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<div id="page-inner">
                 <div class="row">
           <div class="col-md-12">
                <h2 style="color:deepskyblue; padding-left:15px; font-family:Megafont;">Upload Schedule</h2>  
            </div>
            </div>
    <hr/>   
    <%--<div class="panel panel-default">
        <div class="panel-heading">--%>
<%--            Upload Schedule--%>
    <%--    </div>
    </div>--%>
       
            <div class="panel panel-info">

                <div class="panel-heading">
                <div class="row">
                <div class="col-md-4">School Year: <asp:Label runat="server" ID="lblSchoolYear"></asp:Label></div>
                <div class="col-md-4">Semester: <asp:Label runat="server" ID="lblSemester"></asp:Label></div>
                <div class="col-md-4">Duration: <asp:Label runat="server" ID="lblDuration"></asp:Label></div>
                </div>
                </div>

            </div>
            
    
            <br />


            <div class="row">
                <div class="col-md-4"><asp:Button runat="server" Text="Generate Template" ID="templateBtn" OnClick="templateBtn_Click" /></div>
                <div class="col-md-4"><asp:FileUpload ID= "Uploader" runat = "server" /></div>
                <div class="col-md-4"></div>
            </div>
    
            <br />
            <div class="row">
                <div class="col-md-4"></div>
                <div class="col-md-4"><asp:Button ID="btnUpload" OnClick="btnUpload_Click" Text="Upload File" runat="server" /></div>
                <div class="col-md-4"></div>
            </div>




            <asp:UpdatePanel ID="schedPanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>


            <asp:GridView runat="server" ID="schedGrid" AllowPaging="true" AutoGenerateColumns="false" PageSize="30" DataKeyNames="schedID" CssClass="table" OnRowDataBound="schedGrid_RowDataBound" OnPageIndexChanging="schedGrid_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="schedID" Visible="false"  />
                    <asp:BoundField DataField="roomNumber" HeaderText="Room Number" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="days" HeaderText="Days"/>
                    <asp:BoundField DataField="time" HeaderText="Time"/>
                    <%--<asp:BoundField DataField="endTime" HeaderText="End Time"/>--%>
                    <asp:BoundField DataField="subjectCode" HeaderText="Subject Code"/>
                    <asp:BoundField DataField="section" HeaderText="Section"/>
                    <asp:BoundField DataField="instructor" HeaderText="Instructor"/>
                    <%--<asp:BoundField DataField="numOfHours" HeaderText="Number of Hours"/>--%>
            
                </Columns>
                <HeaderStyle BackColor="#014fb3" ForeColor="White" />
                <RowStyle BackColor="White" />
                
            </asp:GridView>
            </ContentTemplate>
                <Triggers>
           
                </Triggers>
            </asp:UpdatePanel>

      

    


    



</div>
</asp:Content>
