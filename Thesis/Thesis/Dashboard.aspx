<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DefaultMaster.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Thesis.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>

.setup img {
    max-width:100%;
    white-space:nowrap;
}
/*.AlternateRowStyle {
     max-width: 50px;
}*/

table-left {
    float:left;
}

table-right {
  float:right;
  width: 80%;
  margin: 0 auto;
}

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <div id="page-inner" style="display:block; overflow:initial;">

      
        <div class="row">
            <div class="col-md-12">
                <h2 style="color:deepskyblue; font-family:Megafont;">Dashboard</h2>    
                <h5>Welcome <asp:Label runat="server" Id="name" ></asp:Label>, Love to see you back. </h5>
            </div>
        </div>
        <hr/>     

           <div class="row">

               <div class="col-md-8">
                   <div class="row">
                       <div class="col-md-6">

                           <div class="panel panel-back noti-box">
                                <span class="icon-box bg-color-red set-icon">
                                    <i class="fa fa-envelope-o"></i>
                                </span>
                                <div class="text-box" >
                                    <br />
                                    <asp:Label runat="server" Id="mkpCount" Font-Bold="true" Font-Size="Large"></asp:Label>
                                    <%--<p class="text-muted" style="padding-top:0px;">New Messages</p>--%>
                                </div>
                            </div>


                       </div>

                       <div class="col-md-6">

                           <div class="panel panel-back noti-box">
                                <span class="icon-box bg-color-green set-icon">
                                    <i class="fa fa-thumbs-o-up"></i>
                                </span>
                                <div class="text-box" >
                                    <br />
                                    <asp:Label runat="server" Id="pendingCount" Font-Bold="true" Font-Size="Large"></asp:Label>
                            
                                </div>
                            </div>




                       </div>

                       </div>

                        <div class="row">
                            <div class="col-md-12">
                                

                                <%-- <asp:UpdatePanel ID="makeUpPanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>--%>
                                        <asp:GridView runat="server" ID="makeupGrid" AllowPaging="true" AutoGenerateColumns="false" PageSize="15" DataKeyNames="makeUpID" OnPageIndexChanging="makeupGrid_PageIndexChanging" CssClass="table table-bordered"
                                                EmptyDataText="There are no upcoming make-up classes" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-Font-Size="Large"
                                                EmptyDataRowStyle-BackColor="White" EmptyDataRowStyle-ForeColor="#014fb3" EmptyDataRowStyle-BorderColor="Black">
                                                <Columns>

                                                    <asp:BoundField DataField="makeUpID" Visible="false"/>
                                                    <asp:BoundField DataField="absentDate" HeaderText="Date of Absence" />
                                                    <asp:BoundField DataField="makeUpDate" HeaderText="Make-Up Date" />
                                                    <asp:BoundField DataField="subjectCode" HeaderText="Subject Code"/>
                                                    <asp:BoundField DataField="section" HeaderText="Section"/>
                                                    <asp:BoundField DataField="room" HeaderText="Room"/>
                                                    <asp:BoundField DataField="time" HeaderText="Time"/>
                                                    <asp:BoundField DataField="status" HeaderText="Status" />
                           
        
                                                </Columns>

                                            </asp:GridView>
                                       <%-- </ContentTemplate>
                                     </asp:UpdatePanel>--%>
                            </div>
                        </div>
                        <div class="row"> <%--for pending approval--%>
                            <div class="col-md-12">
                                <asp:GridView runat="server" ID="pendingGrid" AllowPaging="true" AutoGenerateColumns="false" PageSize="15" OnPageIndexChanging="pendingGrid_PageIndexChanging" CssClass="table table-bordered"
                                                EmptyDataText="No pending make-up classes to be approved" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-Font-Size="Large"
                                                EmptyDataRowStyle-BackColor="White" EmptyDataRowStyle-ForeColor="#014fb3" EmptyDataRowStyle-BorderColor="Black">
                                                <Columns>

                                                    <asp:BoundField DataField="pendingClassID" Visible="false"/>
                                                    <asp:BoundField DataField="absentDate" HeaderText="Date of Absence" />
                                                    <asp:BoundField DataField="assignedDate" HeaderText="Make-Up Date" />
                                                    <asp:BoundField DataField="subjectCode" HeaderText="Subject Code"/>
                                                    <asp:BoundField DataField="section" HeaderText="Section"/>
                                                    <asp:BoundField DataField="room" HeaderText="Room"/>
                                                    <asp:BoundField DataField="time" HeaderText="Time"/>
                                                    <asp:BoundField DataField="status" HeaderText="Status" />
                           
        
                                                </Columns>

                                 </asp:GridView>


                            </div>
                        </div>
                        <div class="row">
                             <div class="col-md-5">
                                <asp:Button ID="makeupClassBtn" runat="server" Text="File a Make-up Class" CssClass="btn btn-info btn-lg" OnClick="makeupClassBtn_Click"
                                    style="background-color:orange; border-color:orange;"/>
                            </div>
                            <div class="pull-right" style="margin-right:15px;">
                                <asp:Button ID="pdfButton" runat="server" Text="Download List of Make-up Class Conducted" CssClass="btn btn-info btn-lg" OnClick="pdfButton_Click"/>
                            </div>
                        </div>
                      <%--  <div class="row">
                         
                        </div>--%>

                        
               </div>



               <div class="col-md-4" style="overflow-x:initial; float:right;">
                  <div class="row">
                      <div class="col-md-12">
                   <%-- table for announcements --%>
                   <asp:UpdatePanel runat="server" ID="announcementPanel" UpdateMode="Conditional">
                       <ContentTemplate>
                           <asp:GridView runat="server" ID="announcementGrid" AllowPaging="true" AutoGenerateColumns="false" PageSize="3"  CssClass="table table-bordered" 
                               EmptyDataText="No announcements" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-Font-Size="Large" EmptyDataRowStyle-BackColor="White" EmptyDataRowStyle-ForeColor="#014fb3" EmptyDataRowStyle-BorderColor="Black" OnPageIndexChanging="announcementGrid_PageIndexChanging">
                                    <Columns>
                                        
                                        <asp:BoundField DataField="announcementMsg" HeaderText="Announcements" HtmlEncode="false" 
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="setup" ItemStyle-Wrap="true" />
                                    </Columns>
                             <HeaderStyle BackColor="#014fb3" ForeColor="White" />
                             <RowStyle BackColor="White" />
                             <AlternatingRowStyle BackColor="Gainsboro" />
                           </asp:GridView>
                        </ContentTemplate>

                       <Triggers>

                       </Triggers>
                   </asp:UpdatePanel>
                    </div>
                   </div>

                   <div class="row">
                       <div class="col-md-12">
                           <asp:GridView runat="server" ID="holidayGrid" AllowPaging="true" AutoGenerateColumns="false" PageSize="5"  CssClass="table table-bordered" 
                               EmptyDataText="No Holiday This Month" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-Font-Size="Large" EmptyDataRowStyle-BackColor="White" EmptyDataRowStyle-ForeColor="#014fb3" EmptyDataRowStyle-BorderColor="Black" OnPageIndexChanging="announcementGrid_PageIndexChanging">
                                    <Columns>
                                        
                                        <asp:BoundField DataField="holiday" HeaderText="Holiday This Month" HtmlEncode="false" />
                                    </Columns>
                             <HeaderStyle BackColor="#014fb3" ForeColor="White" />
                             <RowStyle BackColor="White" />
                             <AlternatingRowStyle BackColor="Gainsboro" />
                           </asp:GridView>

                       </div>

                   </div>


               </div><%-- end of col-md-4--%>
           </div>
           

            
                
           



    </div>




</asp:Content>
