<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DefaultMaster.Master" AutoEventWireup="true" CodeBehind="Holidays.aspx.cs" Inherits="Thesis.Holidays" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 
    
    <script type="text/javascript" src="<%=ResolveUrl("~/") %>Assets/js/moment.min.js"></script>   
    <script type="text/javascript" src="<%=ResolveUrl("~/") %>Assets/js/bootstrap-datetimepicker.min.js"></script>  
    <link rel="stylesheet" href="<%=ResolveUrl("~/") %>Assets/css/bootstrap-datetimepicker.css" type="text/css"/>
   


    <script>


        function showSuccess() {

            $.growl.notice({ title: "Success!", message: "The holiday is added" });
        }


        function showInvalidMsg() {

            $.growl.error({ message: "The holiday is deleted" });
        }

        function showError() {

            $.growl.error({ message: "Invalid input" });
        }



        function pageLoad(sender, args) {

            $('#dateTimePicker').datetimepicker({
                format: 'MMM DD'
            });
           

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="page-inner">
                    <div class="row">
           <div class="col-md-12">
                <h2 style="color:deepskyblue; padding-left:15px; font-family:Megafont;">Holidays</h2>  
            </div>
            </div>
    <hr/>   

         <div class="row">   
        <asp:UpdatePanel runat="server" ID="formPanel" UpdateMode="Conditional">

                <ContentTemplate>
        
                    <div class="col-md-4">

            

            
                        <div class="panel panel-default">
                              <div class="panel-heading">Add Holiday</div>
                              <div class="panel-body">
                                   <div class="form-group">
                                        <label for="holidayTxt">Input Holiday</label>
                                        <asp:TextBox runat="server" ID="holidayTxt" CssClass="form-control"></asp:TextBox>
                                   </div>
                                   <div class="form-group">
                                       <label for="holidayDate">Choose Date</label>
                                        <div class='input-group date' id='dateTimePicker'>
                                            <%--<input id="time" type='text' class="form-control" />--%>
                                            <asp:TextBox runat="server" ID="dateTxt" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon">
                                                <span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                   </div> 
                      
                                   <div class="row">
                                       <div class="col-md-6"></div>
                                       <div class="col-md-6">
                                           <asp:Button runat="server" CssClass="btn btn-primary btn-sm pull-right" ID="addBtn" OnClick="addBtn_Click" Text="Add Holiday" />
                                       </div>
                                   </div>  

                                    
                      
                              </div>
                        </div>


                        <div class="row">
                                <div class="col-md-6">
                                    <asp:FileUpload ID="Uploader" runat = "server"  Visible="false"/>

                                </div>
                                <div class="col-md-6">
                                    <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary btn-sm " Text="Bulk Add" OnClick="btnUpload_Click" Visible="false"/>

                                </div>
                            </div>
                       </div>

          </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUpload" />
            </Triggers>
       </asp:UpdatePanel> 

            <div class="col-md-8">

                <div class="row">
                    <div class="col-md-12">
                    <div class="panel panel-default">
                    <div class="panel-heading">List of Holidays</div>
                        <div class="panel-body">
                   <%-- table for holidays --%>
                            <div class="table-responsive">
                   <asp:UpdatePanel runat="server" ID="holidayPanel" UpdateMode="Conditional">
                       <ContentTemplate>
                           <asp:GridView  runat="server" ID="holidayGrid" AllowPaging="true" AutoGenerateColumns="false" PageSize="15" DataKeyNames="holidayID" CssClass="table table-bordered table-hover dataTables-example" OnRowCommand="holidayGrid_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="holidayID" Visible="false"/>
                                        <asp:BoundField DataField="description" HeaderText="Holiday"/>
                                        <asp:BoundField DataField="date" HeaderText="Date" />

                                         <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="deleteButton" runat="server" CommandName="onDelete" CommandArgument='<%#Eval("holidayID") %>' CssClass="btn btn-danger" OnClientClick="return confirm('Are you sure you want to delete this holiday?');">Delete</asp:LinkButton>
                                                </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                             <HeaderStyle BackColor="#014fb3" ForeColor="White" />
                             <RowStyle BackColor="White" />
                             <AlternatingRowStyle BackColor="Gainsboro" />
                           </asp:GridView>
                        </ContentTemplate>

                           <Triggers>
                               <asp:AsyncPostBackTrigger ControlID="addBtn" EventName="Click" />
                           </Triggers>
                       </asp:UpdatePanel>
                        </div>
                    </div>
                  </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">


                    </div>

                </div>

                 
               </div> <%--end of div col md 8--%>
         </div>

            
            
    </div>
</asp:Content>
