<%@ Page Title="Cancel Class Sched" Language="C#" AutoEventWireup="true" CodeBehind="CancelClassSched.aspx.cs"
     MasterPageFile="~/Masters/DefaultMaster.Master" Inherits="Thesis.CancelClassSched" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
        $(function () {
            $("#cancelClassDate").datepicker({
                altField: "#makeUpClassDay",
                altFormat: "DD"
            });
        });
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

     <div id="page-inner">
           <div class="row">
           <div class="col-md-12">
                <h2 style="color:deepskyblue; padding-left:15px; font-family:Megafont;">Cancel Class</h2>  
            </div>
            </div>
    <hr/>   

    <div class="row">
    <div class="col-md-12">
    <!-- Advanced Tables -->
    <%--<div class="panel panel-default">--%>
                    <%--<div class="panel-heading">
                           
                        </div>--%>        <div class="panel-body">
            <div class="table-responsive">                <div class="form-group" style="float:right; padding-right:5px;">
                            <label style="padding-right:10px;">Set Date of Cancel:</label>
                            <asp:TextBox ID="cancelClassDate" runat="server" CssClass="form-control" ClientIDMode="Static" style="max-width:160px; display:inline;"></asp:TextBox>  
                            <asp:LinkButton ID="assignCancelDate" runat="server" CssClass="btn btn-primary" OnClick="assignCancelDate_Click">Search</asp:LinkButton>
                </div>
                <asp:UpdatePanel ID ="cancelSchedPanel" runat ="server" UpdateMode="Conditional">
                <ContentTemplate>
                    
                <asp:GridView ID="cancelSchedGrid"  runat="server" CssClass="table table-striped table-bordered table-hover" AllowPaging="true" AutoGenerateColumns="false"
                     PageSize="15" DataKeyNames="schedId" OnPageIndexChanging="cancelSchedGrid_PageIndexChanging" OnRowCommand="cancelSchedGrid_RowCommand" OnRowDataBound="cancelSchedGrid_RowDataBound">
                    <EmptyDataTemplate>
                           <center>
                           <p>
                           <h3><i class="modalPrompt">No Regular Classes Found</i></h3></p>
                           </center>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="schedId" Visible="false"/>
                        <asp:BoundField DataField="room" HeaderText="Room Number" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="subjectCode" HeaderText="Subject Code"/>
                        <asp:BoundField DataField="section" HeaderText="Section"/>
                        <asp:BoundField DataField="facultyName" HeaderText="Faculty Name"/>
                        <asp:BoundField DataField="time" HeaderText="Time"/>
                        
                       
                        <%-- For advance and makeup class only: show/display --%>
                        <%--<asp:BoundField DataField="classType" HeaderText="Class Type"/>--%>
                    <%-- Contains the action buttons --%>
                    <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                            <asp:LinkButton ID="btnCancel" runat="server" CommandName="onCancel" CssClass="btn btn-primary" OnClick="btnCancel_Click" OnClientClick="return confirm('Are you sure you want to cancel this schedule?');" CommandArgument='<%#Eval("schedId")%>'>Cancel</asp:LinkButton>
                                <%--<asp:LinkButton ID="deleteButton" runat="server" CommandName="onDelete" CommandArgument='<%#Eval("schoolID") %>' CssClass="btn btn-primary">Delete</asp:LinkButton>--%>
                            </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                </ContentTemplate>
                    <Triggers>
                            <asp:AsyncPostBackTrigger EventName="Click" ControlID="assignCancelDate"></asp:AsyncPostBackTrigger>
                    </Triggers>
                </asp:UpdatePanel>
           </div>
           </div>
    <%--</div>--%>
    </div>
    </div>

</div>


</asp:Content>