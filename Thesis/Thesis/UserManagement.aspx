<%@ Page Title="User Management" Language="C#" MasterPageFile="~/Masters/DefaultMaster.Master" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="Thesis.UserManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">



<script type="text/javascript">
    function openModal() {
        // $('#addUserModal').modal('show');
        $('#btnShowModal').click;
    }


    function showSuccessMsg() {

        $.growl.notice({ title: "Success!", message: "Eligible Person has been Added" });
    }

    function exist() {

        $.growl.error({ message: "Faculty ID already existed" });
    }


    function showInvalidMsg() {
        $.growl.error({ title: "Deleted!",message: "Person deleted successfully" });
    }

    function showEditedMsg() {
        $.growl.warning({ message: "Person has been edited successfully" });
    }

    function enterFacultyID() {
        
            $.growl.error({ title: "Required", message: "Please Enter Faculty ID" });
        
    }

    function invalidFile() {

        $.growl.error({ title: "Invalid", message: "Please upload only .xlsx file" });

    }

    function uploadSuccess() {
        $.growl.notice({ title: "Success!", message: "Upload Success" });
    }
    function empty() {
        $.growl.error({ message: "Please do not put an empty Faculty ID in the file" });
    }
    
</script>

<style>
    .margin{
        margin:10px;
    }

</style>





</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
   <div id="page-inner"> 
           <div class="row">
            <div class="col-md-12">
                <h2 style="color:deepskyblue; padding-left:15px; font-family:Megafont;">Allowed Users</h2>    
            </div>
        </div>
        <hr/>
       <asp:Button ID="addUserModalButton" runat="server" CssClass="btn btn-info btn-lg margin" Text="Add User" onclick="addUserModalButton_Click" Width="200px"/>
       
       <asp:Button ID="searchUserButton" runat="server"  Text="Search" CssClass="btn btn-info btn-lg margin pull-right" OnClick="searchUserButton_Click" Width="200px"/>
       <asp:TextBox ID="searchTextBox" runat="server" Height="45px" Width="500px" CssClass="margin pull-right" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
      <script>

              $(document).ready(function () {
                  $('<%=searchTextBox.ClientID%>').keydown(function (event) {
                     if (event.keyCode == 13) {
                         console.log("Enter Key Pressed");
                         event.preventDefault();
                         return false;
                     }
                 });
             });
        </script> 




        <%-- Add User Modal --%>
        <div aria-hidden="true" aria-labelledby="ModalHeaderLabel" role="dialog" data-backdrop="static"
        data-keyboard="false" tabindex="-1" class="modal modal-xlarge" id="addUserModal"
        style="display: none;">

        <asp:UpdatePanel runat="server" id="mdlAddUserPanel" UpdateMode="Conditional">
            <ContentTemplate>
               <div class="modal-content" style="margin-top:100px;">

                   <div class="modal-body">
                    
                    <p>Faculty ID:&nbsp&nbsp<asp:TextBox ID="schoolIDTextBox" runat="server"></asp:TextBox></p>
                    <%--<asp:RequiredFieldValidator runat="server" id="reqName" controltovalidate="schoolIDTextBox" errormessage="Please enter Faculty ID!" />--%>
                    <p>Firstname: <asp:TextBox ID="firstNameTextBox" runat="server"></asp:TextBox></p>
                    <p>Lastname: <asp:TextBox ID="lastNameTextBox" runat="server"></asp:TextBox></p>
                     <p>Position: <asp:DropDownList ID="ddlPosition" runat="server" Width="300px">
                            <asp:ListItem Text="Faculty" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Department Head" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Academics Assistant" Value="3"></asp:ListItem>
                            <asp:ListItem Text="VP Academics" Value="4"></asp:ListItem>
                    </asp:DropDownList></p>
                    

                    </div>    
                     <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <asp:LinkButton runat="server" ID="saveButton" OnClick="saveButton_Click" Text="Save" CssClass="btn btn-primary"></asp:LinkButton>
                         <%--<button type="button" class="btn btn-primary">Save</button>--%>
                    </div>

               </div>
               
                
               
                
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="addUserModalButton" EventName="Click" />
                <%--<asp:AsyncPostBackTrigger ControlID="searchUserButton" EventName="Click" />--%>
            </Triggers>
        </asp:UpdatePanel>
        </div>
        <%-- End of Add User Modal --%>
        

        <%-- Edit User Modal --%>
        <div aria-hidden="true" aria-labelledby="ModalHeaderLabel" role="dialog" data-backdrop="static"
        data-keyboard="false" tabindex="-1" class="modal modal-xlarge" id="updateUserModal"
        style="display: none;">

        <asp:UpdatePanel runat="server" id="mdlUpdateUserPanel">
            <ContentTemplate>
               <div class="modal-content" style="margin-top:100px;">

                   <div class="modal-body">
                    <asp:Label runat="server" ID="facultyIDLbl" Visible="false"></asp:Label>
                    <p>Faculty ID:&nbsp&nbsp<asp:TextBox ID="editSchoolIDTextBox" runat="server" Enabled="false"></asp:TextBox></p>
                    <p>Firstname: <asp:TextBox ID="editFirstNameTextBox" runat="server"></asp:TextBox></p>
                    <p>Lastname: <asp:TextBox ID="editLastnameTextBox" runat="server"></asp:TextBox></p>
                    <p>Position: <asp:DropDownList ID="ddlPositionEdit" runat="server" Width="300px">
                            <asp:ListItem Text="Faculty" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Department Head" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Academics Assistant" Value="3"></asp:ListItem>
                            <asp:ListItem Text="VP Academics" Value="4"></asp:ListItem>
                    </asp:DropDownList></p>
                    

                    </div>    
                     <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <asp:LinkButton runat="server" ID="EditButton" OnClick="EditButton_Click" Text="Edit" CssClass="btn btn-primary"></asp:LinkButton>
                         <%--<button type="button" class="btn btn-primary">Save</button>--%>
                    </div>

               </div>
            </ContentTemplate>
            <Triggers>
               
            </Triggers>
        </asp:UpdatePanel>
        </div>

        <%-- End of Edit User Modal --%>

  
    <asp:UpdatePanel ID="usersPanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>


    <asp:GridView runat="server" ID="usersGrid" AllowPaging="true" AutoGenerateColumns="false" PageSize="15" DataKeyNames="ID,schoolID" OnPageIndexChanging="usersGrid_PageIndexChanging" CssClass="table table-bordered" OnRowCommand="usersGrid_RowCommand" OnRowDataBound="usersGrid_RowDataBound"
        EmptyDataText="There are no allowed users found" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-Font-Size="Large"
        EmptyDataRowStyle-BackColor="White" EmptyDataRowStyle-ForeColor="#014fb3" EmptyDataRowStyle-BorderColor="Black">
        <Columns>

            <asp:BoundField DataField="ID" Visible="false" />
            <asp:BoundField DataField="schoolID" HeaderText="Faculty ID"/>
            <asp:BoundField DataField="firstName" HeaderText="Firstname"/>
            <asp:BoundField DataField="lastName" HeaderText="Lastname"/>
            <asp:BoundField DataField="position" HeaderText="Position" />
        <%-- Contains the action buttons --%>
        <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                   
                    <asp:LinkButton ID="updateButton" runat="server" CommandName="onUpdate" CommandArgument='<%#Eval("schoolID") %>' CssClass="btn btn-primary">Update</asp:LinkButton>
                    <asp:LinkButton ID="deleteButton" runat="server" CommandName="onDelete" CommandArgument='<%#Eval("schoolID") %>' CssClass="btn btn-primary" OnClientClick="return confirm('Are you sure you want to delete this person?');">Delete</asp:LinkButton>
                </ItemTemplate>
        </asp:TemplateField>
        
        </Columns>

    </asp:GridView>
    </ContentTemplate>
        <Triggers>
           
        </Triggers>
    </asp:UpdatePanel>

    <div class="row">
        <div class="col-md-2"><asp:Button ID="btnGenerate" runat="server" CssClass="btn btn-info btn-lg margin" Text="Generate Template"  Width="200px" OnClick="btnGenerate_Click"/></div>
        <div class="col-md-2">
            <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-info btn-lg margin" Text="Bulk Add"  Width="200px" OnClick="btnUpload_Click"/>
            
        </div>
        <div class="col-md-2"><br /><asp:FileUpload ID= "Uploader" runat = "server" /></div>
    </div>



    </div>
</asp:Content>
