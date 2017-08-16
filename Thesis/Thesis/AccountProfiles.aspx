<%@ Page Title="Account Profiles Page" Language="C#" MasterPageFile="~/Masters/DefaultMaster.Master" AutoEventWireup="true" CodeBehind="AccountProfiles.aspx.cs" Inherits="Thesis.UserProfiles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #updateUserModal .modal-dialog  {width:30%;}
    </style>

    <script type="text/javascript">

        function editedMsg() {
            $.growl.warning({ title: "Account Edited", message: "User account has been edited." });
        }
        function deletedMsg() {
            $.growl.error({ title: "Account Deleted!", message: "User account has been deleted." });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    
    <div id="page-inner">
                <div class="row">
           <div class="col-md-12">
                <h2 style="color:deepskyblue; padding-left:15px; font-family:Megafont;">Registered Users</h2>  
            </div>
            </div>
    <hr/>   

        <div class="row">
            <div class="col-md-12">
                <div class="panel-body">
                    <div class="table-responsive">
                        <%--<asp:Button ID="searchUserButton" runat="server"  Text="Search" CssClass="btn btn-info btn-lg margin pull-right" OnClick="searchUserButton_Click" Width="200px"/>
                        <asp:TextBox ID="searchTextBox" runat="server" Height="45px" Width="500px" CssClass="margin pull-right" onkeydown="return (event.keyCode!=13);"></asp:TextBox>--%>
                        <asp:UpdatePanel ID="usersPanel" runat="server" UpdateMode="Conditional" >
                            <ContentTemplate>
                                <asp:GridView runat="server" ID="usersGrid" AllowPaging="true"
                                    AutoGenerateColumns="false" PageSize="15" DataKeyNames="user_id, faculty_id"
                                    CssClass="table table-bordered table-striped table-hover"
                                    OnRowCommand="usersGrid_RowCommand" OnRowDataBound="usersGrid_RowDataBound">
                                    <EmptyDataTemplate>
                                        <center>
                                            <p><h3><i class="modalPrompt">No Accounts Found</i></h3></p>
                                        </center>
                                    </EmptyDataTemplate>    
                                <Columns>
                                    <asp:BoundField DataField="user_id" Visible="false"/>
                                    <asp:BoundField DataField="faculty_id" HeaderText="Faculty ID"/>
                                    <asp:BoundField DataField="first_name" HeaderText="First Name"/>
                                    <asp:BoundField DataField="last_name" HeaderText="Last Name"/>
                                    <asp:BoundField DataField="email_address" HeaderText="Email Address"/>
                                    <asp:BoundField DataField="user_type" HeaderText="User Type"/>
                                    <%-- Contains the action buttons --%>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Justify" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="updateButton" runat="server" CommandName="onUpdate" CommandArgument='<%#Eval("faculty_id") %>' CssClass="btn btn-primary">Update</asp:LinkButton>
                                            <asp:LinkButton ID="deleteButton" runat="server" CommandName="onDelete" CommandArgument='<%#Eval("user_id") %>' CssClass="btn btn-danger">Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                           </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>

    <%-- Edit User Modal --%>

        <!-- Modal -->
          <div class="modal fade" id="updateUserModal" tabindex="-1" role="dialog" aria-labelledby="ModalHeaderLabel" aria-hidden="true">
            <div class="modal-dialog">
                <asp:UpdatePanel runat="server" id="mdlUpdateUserPanel">
                    <ContentTemplate>
              <div class="modal-content">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                  <h4 class="modal-title">Edit Account Profile</h4>
                </div>
                <div class="modal-body">
                  <asp:Panel runat="server" ID="unauthorizedUserPanel">
                            <asp:Label ID="unauthorizedUserLabel" runat="server" Visible="false"></asp:Label>
                        </asp:Panel>
	                        <div>
		                        <label for="facultyId">Faculty ID: </label>
                                <asp:TextBox ID="facultyId" name="facultyId" ClientIDMode="Static" runat="server" required="required" Enabled="false"/>
	                        </div>
	                        <div>
		                        <label for="accountType">Account Type: </label>
                                <asp:DropDownList ID="dropdownAccount" runat="server" >
                                    <asp:ListItem Text="Select From Option" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Faculty" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Department Head" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Academics Assistant" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="VP Academics" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:Panel runat="server" ID="Panel2">
                                    <asp:Label ID="invalidDropdownSelection" runat="server" Visible="false"></asp:Label>
                                </asp:Panel>
	                        </div>
	                        <div class="field">
		                        <label for="firstName">First Name: </label>
                                <asp:TextBox ID="firstName" name="firstName" ClientIDMode="Static" runat="server" required="required"/>
                                <asp:Panel runat="server" ID="Panel1">
                                    <asp:Label ID="firstNameLabel" runat="server" Visible="false"></asp:Label>
                                </asp:Panel>
	                        </div>
	                        <div class="field">
		                        <label for="lastName">Last Name: </label>
                                <asp:TextBox ID="lastName" name="lastName" ClientIDMode="Static" runat="server" required="required"/>
                                <asp:Panel runat="server" ID="Panel4">
                                    <asp:Label ID="lastNameLabel" runat="server" Visible="false"></asp:Label>
                                </asp:Panel>
                                        
	                        </div>
	                        <div class="field">
		                        <label for="email">E-mail: </label>
                                <asp:TextBox ID="email" name="email" ClientIDMode="Static" runat="server" required="required"/>
                                <asp:Panel runat="server" ID="Panel5">
                                    <asp:Label ID="emailLabel" runat="server" Visible="false"></asp:Label>
                                </asp:Panel>
	                        </div>
                </div>
                <div class="modal-footer">
                  <asp:LinkButton ID="editUserButton" class="btn btn-primary" OnClick="editUserButton_Click" runat="server" Text="Edit Profile"/>
                  <asp:LinkButton ID="resetRegButton" class="btn btn-default" OnClick="resetRegButton_Click" runat="server" Text="Reset"/>
                </div>
              </div><!-- /.modal-content -->
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="editUserButton" EventName="Click" />
            </Triggers>
            </asp:UpdatePanel>
            </div><!-- /.modal-dialog -->
          </div><!-- /.modal -->
        <%-- End of Edit User Modal --%>

        <%-- Delete User Modal --%>
        <!-- Modal -->
          <div class="modal fade" id="deleteUserModal" tabindex="-1" role="dialog" aria-labelledby="ModalHeaderLabel" aria-hidden="true">
            <div class="modal-dialog">
              <div class="modal-content">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                  <h4 class="modal-title">Delete Account Profile</h4>
                </div>
                <div class="modal-body">
                    <p>Are you sure you want to delete this user?<asp:Label ID="idToDeleteLabel" runat="server" visible="true"></asp:Label></p>
                </div>
                <div class="modal-footer">
                  <asp:LinkButton ID="deleteYesBtn" class="btn btn-danger" OnClick="deleteYesBtn_Click" runat="server" Text="Yes"/>
                  <asp:LinkButton class="btn btn-primary" data-dismiss="modal" runat="server" Text="No"/>
                </div>
              </div><!-- /.modal-content -->
            </div><!-- /.modal-dialog -->
          </div><!-- /.modal -->
        <%-- End of Delete User Modal --%>
    </div>
</asp:Content>
