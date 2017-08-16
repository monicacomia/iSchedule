<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DefaultMaster.Master" AutoEventWireup="true" CodeBehind="Announcements.aspx.cs" Inherits="Thesis.Announcements" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
   <script type="text/javascript" src="<%=ResolveUrl("~/") %>Assets/js/moment.min.js"></script>   
   <script type="text/javascript" src="<%=ResolveUrl("~/") %>Assets/js/bootstrap-datetimepicker.min.js"></script>  
   <link rel="stylesheet" href="<%=ResolveUrl("~/") %>Assets/css/bootstrap-datetimepicker.css" type="text/css"/>
  

  <script type="text/javascript">
         $(document).ready(function () {
             $("#<%= txtSummernote.ClientID %>").summernote({
                 height: 300,
                 
                 minHeight: null,
                 maxHeight: null,
                 focus: true,


             });

             $("#<%= txtSummernote.ClientID %>").on('summernote.blur', function () {
                 $('#<%= txtSummernote.ClientID %>').html($('#<%= txtSummernote.ClientID %>').summernote('code'));
             });

             $(function () {
                 $('#dateTimePicker').datetimepicker({
                     
                     sideBySide: true
                 });

             });
             
             
         });
        
      function showSuccessMsg() {

          $.growl.notice({ title: "Success!", message: "The announcement is broadcasted" });
      }

      function showInvalidMsg() {
          $.growl.error({ message: "The expiry date cannot be set in the past" });
      }
      function input() {
          $.growl.error({ message: "Please set expiration date" });
      }

</script>
         


    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
 
                <div class="row">
           <div class="col-md-12">
                <h2 style="color:deepskyblue; padding-left:15px; font-family:Megafont;">Announcements</h2>  
            </div>
            </div>
    <hr/>   


<div class="panel panel-primary">

    <div class="panel-heading"></div>

    <div class="panel-body">



    

    <div class="row-fluid">
        <div class="col-md-3"><asp:Label runat="server" Text="Set Expiry Date of Announcement"></asp:Label></div>
        <div class="col-md-5">
             <div class="form-group">
                <div class='input-group date' id='dateTimePicker'>
                    <%--<input id="time" type='text' class="form-control" />--%>
                    <asp:TextBox ID="time" runat="server" CssClass="form-control"></asp:TextBox>
                    <span class="input-group-addon">
                        <span class="glyphicon glyphicon-calendar"></span>
                    </span>
                </div>
            </div>
        </div>
        <div class="col-md-4"></div>
    </div>


        <br />
        <br />
        <br />
 <div class="row-fluid">

     <asp:TextBox ID="txtSummernote" runat="server" TextMode="MultiLine" Rows="2"></asp:TextBox>
 </div>
            
   
<asp:Button ID="submitBtn" OnClick="submitBtn_Click" runat="server" Text="Broadcast Announcement"/>

<br />
<br />

   


    
     <asp:UpdatePanel runat="server" ID="announcementPanel" UpdateMode="Conditional">
                       <ContentTemplate>
                           <asp:GridView runat="server" ID="announcementGrid" AllowPaging="true" AutoGenerateColumns="false" PageSize="15" DataKeyNames="announcementID" CssClass="table table-bordered" 
                               EmptyDataText="No announcements" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-Font-Size="Large" EmptyDataRowStyle-BackColor="White" EmptyDataRowStyle-ForeColor="#014fb3" EmptyDataRowStyle-BorderColor="Black" OnRowCommand="announcementGrid_RowCommand" OnPageIndexChanging="announcementGrid_PageIndexChanging" >
                                    <Columns>
                                        <asp:BoundField DataField="announcementID" Visible="false"/>
                                        
                                        <asp:BoundField DataField="announcementMsg" HeaderText="Announcements" HtmlEncode="false" />
                                        <asp:BoundField DataField="expiryDate" HeaderText="Expiry Date" />
                                     <asp:TemplateField>
                               
                                           <ItemTemplate>
                                               <asp:LinkButton runat="server" ID="viewBtn" CommandName="onStop" CommandArgument='<%#Eval("announcementID") %>' CssClass="btn btn-danger" OnClientClick="return confirm('Are you sure you want to stop broadcasting this announcement?');">Stop Broadcast</asp:LinkButton>
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                    
                                    
                                    
                                    </Columns>



                             <HeaderStyle BackColor="#014fb3" ForeColor="White" />
                             <RowStyle BackColor="White" />
                             <AlternatingRowStyle BackColor="Gainsboro" />
                           </asp:GridView>
                        </ContentTemplate>

                       <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="submitBtn" EventName="Click" />
                       </Triggers>
      </asp:UpdatePanel>
      </div>         
</div>
  
</asp:Content>
