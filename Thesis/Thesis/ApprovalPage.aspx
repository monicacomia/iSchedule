<%@ Page Title="Approval Page" Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/DefaultMaster.Master" 
    CodeBehind="ApprovalPage.aspx.cs" Inherits="Thesis.ApprovalPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

     <div id="page-inner">
           <div class="row">
           <div class="col-md-12">
                <h2 style="color:deepskyblue; padding-left:15px; font-family:Megafont;">Pending Approval</h2>    
                

       <%--<asp:Button ID="addUserModalButton" runat="server" CssClass="btn btn-info btn-lg margin" Text="Add User" onclick="addUserModalButton_Click" Width="200px"/>
       <asp:Button ID="searchUserButton" runat="server"  Text="Search" CssClass="btn btn-info btn-lg margin pull-right" OnClick="searchUserButton_Click" Width="200px"/>
       <asp:TextBox ID="searchTextBox" runat="server" Height="45px" Width="500px" CssClass="margin pull-right"></asp:TextBox>--%>
        
        <%-- Add User Modal --%>
        
        <%-- Search User Modal --%>
        <%--<div aria-hidden="true" aria-labelledby="ModalHeaderLabel" role="dialog" data-backdrop="static"
        data-keyboard="false" tabindex="-1" class="modal modal-xlarge" id="updateUserModal"
        style="display: none;">

        <asp:UpdatePanel runat="server" id="mdlUpdateUserPanel">
            <ContentTemplate>
               <div class="modal-content" style="margin-top:100px;">

                   <div class="modal-body">
                    
                    <p>SchoolID:&nbsp&nbsp<asp:TextBox ID="editSchoolIDTextBox" runat="server"></asp:TextBox></p>
                    <p>Firstname: <asp:TextBox ID="editFirstNameTextBox" runat="server"></asp:TextBox></p>
                    <p>Lastname: <asp:TextBox ID="editLastnameTextBox" runat="server"></asp:TextBox></p>
                   
                    </div>    
                     <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <asp:LinkButton runat="server" ID="EditButton" OnClick="EditButton_Click" Text="Edit" CssClass="btn btn-primary"></asp:LinkButton>--%>
                         <%--<button type="button" class="btn btn-primary">Save</button>--%>
              <%--      </div>

               </div>
            </ContentTemplate>
            <Triggers>
               
            </Triggers>
        </asp:UpdatePanel>
        </div>--%>
        <%-- End of Search User Modal --%>
                    
    </div>
    </div>
    <hr/>   

    <div class="row">
    <div class="col-md-12">
    <!-- Advanced Tables -->
    <%--<div class="panel panel-default">--%>
                        <div class="panel-heading">
                             Approval Page - Pending Schedules
                        </div>    
        <div class="panel-body">
            <div class="table-responsive">                <asp:UpdatePanel ID ="pendingSchedPanel" runat ="server" UpdateMode="Conditional">
                <ContentTemplate>
                <asp:GridView ID="pendingSchedGrid"  runat="server"
                    CssClass="table table-striped table-bordered table-hover"
                    AllowPaging="true" AutoGenerateColumns="false"
                    PageSize="15" DataKeyNames="pendingClassId"
                    OnPageIndexChanged="pendingSchedGrid_PageIndexChanged"
                    OnRowCommand="pendingSchedGrid_RowCommand">
                        <EmptyDataTemplate>
                           <center>
                           <p>
                           <h3><i class="modalPrompt">No Pending Classes Found</i></h3></p>
                           </center>
                        </EmptyDataTemplate>
                        <Columns>
                        <asp:BoundField DataField="pendingClassId" Visible="false"/>
                        <asp:BoundField DataField="dateFiled" HeaderText="Date Filed" HtmlEncode="False" DataFormatString="{0:d}"/>
                        <asp:BoundField DataField="subjCode" HeaderText="Subject Code"/>
                        <asp:BoundField DataField="section" HeaderText="Section"/>
                        <asp:BoundField DataField="facultyName" HeaderText="Faculty Name"/>
                        <asp:BoundField DataField="assignedDate" HeaderText="Assigned Date" HtmlEncode="False" DataFormatString="{0:d}"/>
                        <asp:BoundField DataField="duration" HeaderText="Time"/>
                        <asp:BoundField DataField="numHours" HeaderText="Duration"/>
                        <asp:BoundField DataField="room" HeaderText="Room"/>
                        <asp:BoundField DataField="reason" HeaderText="Reason"/>
                        <%-- For advance and makeup class only: show/display --%>
                        <%--<asp:BoundField DataField="classType" HeaderText="Class Type"/>--%>

                    <%-- Contains the action buttons --%>
                    <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            <asp:LinkButton ID="approveButton" runat="server" CommandName="onApprove" CssClass="btn btn-primary" OnClick="approveButton_Click" OnClientClick="return confirm('Are you sure you want to APPROVE this schedule?');" CommandArgument='<%#Eval("pendingClassId")%>'>Approve</asp:LinkButton>
                            <asp:LinkButton ID="disapproveButton" runat="server" CommandName="onDisapprove" CssClass="btn btn-danger" OnClick="disapproveButton_Click" OnClientClick="return confirm('Are you sure you want to DISAPPROVE this schedule?');" CommandArgument='<%#Eval("pendingClassId")%>'>Disapprove</asp:LinkButton>
                                <%--<asp:LinkButton ID="deleteButton" runat="server" CommandName="onDelete" CommandArgument='<%#Eval("schoolID") %>' CssClass="btn btn-primary">Delete</asp:LinkButton>--%>
                            </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>

                </asp:GridView>
                </ContentTemplate>
                    <Triggers>
           
                    </Triggers>
                </asp:UpdatePanel>
           </div>
           </div>
    <%--</div>--%>
    </div>
    </div>

</div>


</asp:Content>