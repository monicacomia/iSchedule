<%@ Page Title="Modify Schedule" Language="C#" MasterPageFile="~/Masters/DefaultMaster.Master" AutoEventWireup="true" CodeBehind="ModifySchedule.aspx.cs" Inherits="Thesis.ModifySchedule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
     <script type="text/javascript" src="<%=ResolveUrl("~/") %>Assets/js/moment.min.js"></script>   
    <script type="text/javascript" src="<%=ResolveUrl("~/") %>Assets/js/bootstrap-datetimepicker.min.js"></script>  
    <link rel="stylesheet" href="<%=ResolveUrl("~/") %>Assets/css/bootstrap-datetimepicker.css" type="text/css"/>

    <script type="text/javascript">
        function pageLoad(sender, args) {

            $('#timePicker').datetimepicker({
                format: 'HHmm'
            });

            $('#timePicker1').datetimepicker({
                format: 'HHmm'
            });

            $('#timePicker2').datetimepicker({
                format: 'HHmm'
            });

            $('#timePicker3').datetimepicker({
                format: 'HHmm'
            });
        }


        function invalid() {
            $.growl.error({ message: "Please do not leave the subject code blank" });
        }

        function showSuccess() {

            $.growl.notice({ title: "Success!", message: "The schedule is added" });
        }

        function exist() {

            $.growl.error({ message: "The subject already existed at this room and time" });
        }

        function edited() {
            $.growl.warning({ message: "The schedule is edited successfully" });
        }
    </script>

    <style>
        .center{
            text-align:center;
        }

        .chkList label{
            margin-right:10px;
        }

        hr {
            border-color:black;
            border-style:solid;
        }

        .marginSearch{
            margin-top:5px;
            margin-right:5px;
        }

        /*-----PAGINATION------*/

        .pagination-ys {
            /*display: inline-block;*/
            padding-left: 0;
            margin: 20px 0;
            border-radius: 4px;
        }

        .pagination-ys table > tbody > tr > td {
            display: inline;
        }

        .pagination-ys table > tbody > tr > td > a,
        .pagination-ys table > tbody > tr > td > span {
            position: relative;
            float: left;
            padding: 8px 12px;
            line-height: 1.42857143;
            text-decoration: none;
            color: #1e90ff;
            background-color: #ffffff;
            border: 1px solid #dddddd;
            margin-left: -1px;
        }

        .pagination-ys table > tbody > tr > td > span {
            position: relative;
            float: left;
            padding: 8px 12px;
            line-height: 1.42857143;
            text-decoration: none;    
            margin-left: -1px;
            z-index: 2;
            color: #aea79f;
            background-color: #d2e8ff;
            border-color: #dddddd;
            cursor: default;
        }

        .pagination-ys table > tbody > tr > td:first-child > a,
        .pagination-ys table > tbody > tr > td:first-child > span {
            margin-left: 0;
            border-bottom-left-radius: 4px;
            border-top-left-radius: 4px;
        }

        .pagination-ys table > tbody > tr > td:last-child > a,
        .pagination-ys table > tbody > tr > td:last-child > span {
            border-bottom-right-radius: 4px;
            border-top-right-radius: 4px;
        }

        .pagination-ys table > tbody > tr > td > a:hover,
        .pagination-ys table > tbody > tr > td > span:hover,
        .pagination-ys table > tbody > tr > td > a:focus,
        .pagination-ys table > tbody > tr > td > span:focus {
            color: #1e90ff;
            background-color: #d2e8ff;
            border-color: #dddddd;
        }

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
  <div id="page-inner">
      
                   <div class="row">
           <div class="col-md-12">
                <h2 style="color:deepskyblue; padding-left:15px; font-family:Megafont;">Modify Schedule</h2>  
            </div>
            </div>
    <hr/>   
     <div class="panel panel-primary">
    <div class="panel panel-heading">
        <%--Modify Schedule--%>
    </div>
    <div class="panel panel-body">
     <%-- Add Schedule Modal --%>
        <div aria-hidden="true" aria-labelledby="ModalHeaderLabel" role="dialog" data-backdrop="static"
        data-keyboard="false" tabindex="-1" class="modal modal-xlarge" id="addScheduleModal"
        style="display: none;">

        <asp:UpdatePanel runat="server" id="mdlSchedulePanel" UpdateMode="Conditional">
            <ContentTemplate>
               <div class="modal-content" style="margin-top:100px; padding:10px">
                   <div class="modal-header">
                      <button type="button" class="close" data-dismiss="modal">&times;</button>
                      <h4 class="modal-title"><b>Add Schedule</b></h4>
                    </div>
                   
                   <div class="modal-body">
                    
                    
                    <div class="row">
                        <div class="col-md-1">Room Number:</div>
                        <div class="col-md-5"><%--<asp:TextBox runat="server" ID="roomNumberTxtBox"></asp:TextBox>--%>
                            <asp:DropDownList runat="server" ID="roomDDL">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-6"></div>
                    </div>

                    <br />
                    
                     <div class="row">
                        <div class="col-md-1">Days:</div>
                        <div class="col-md-5">
                            <asp:DropDownList runat="server" ID="ddlDay">
                                 <asp:ListItem Text="" Value=""></asp:ListItem>
                                 <asp:ListItem Text="Monday" Value="1"></asp:ListItem>
                                 <asp:ListItem Text="Tuesday" Value="2"></asp:ListItem>
                                 <asp:ListItem Text="Wednesday" Value="3"></asp:ListItem>
                                 <asp:ListItem Text="Thursday" Value="4"></asp:ListItem>
                                 <asp:ListItem Text="Friday" Value="5"></asp:ListItem>
                                 <asp:ListItem Text="Saturday" Value="6"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-6"></div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-md-1">Time:</div>
                        <div class="col-md-5">
                            <div class="form-group">
                                <label for="timePicker">Start of Class</label>
                                <div class='input-group date' id='timePicker' style="width:200px;">
                                                <%--<input id="time" type='text' class="form-control" />--%>
                                                <asp:TextBox runat="server" ID="startTxt" CssClass="form-control"></asp:TextBox>
                                                <span class="input-group-addon">
                                                    <span class="glyphicon glyphicon-time"></span>
                                                </span>
                                 </div>
                             </div>

                            <div class="form-group">
                                <label for="timePicker1">End of Class</label>
                                <div class='input-group date' id='timePicker1' style="width:200px;">
                                                <%--<input id="time" type='text' class="form-control" />--%>
                                                <asp:TextBox runat="server" ID="endTxt" CssClass="form-control"></asp:TextBox>
                                                <span class="input-group-addon">
                                                    <span class="glyphicon glyphicon-time"></span>
                                                </span>
                                 </div>
                             </div>


                            <%--<asp:DropDownList ID="DropDownList1" runat="server">
                                <asp:ListItem Text="7:30AM-11:00AM" Value="7:30AM-11:00AM"></asp:ListItem>
                                <asp:ListItem Text="11:00AM-2:30PM" Value="11:00AM-2:30PM"></asp:ListItem>
                                <asp:ListItem Text="2:30PM-6:00PM" Value="2:30AM-6:00PM"></asp:ListItem>
                                <asp:ListItem Text="6:00PM-9:00PM" Value="6:00AM-9:00PM"></asp:ListItem>
                            </asp:DropDownList>--%>
                        </div>
                        <div class="col-md-6"></div>
                    </div>

                    
                       <%-- <div class="form-control-wrapper">
						    <input type="text" id="startTime" name="startTime" class="form-control floating-label" placeholder="Start Time" />
                            <br />
                            <input type="text" id="endTime" name="endTime" class="form-control floating-label"  placeholder="End Time"/>

					    </div>--%>

                    <br />

                    <div class="row">
                        <div class="col-md-1">Subject Code:</div>
                        <div class="col-md-5"><asp:TextBox runat="server" ID="subjectCodeTextBox"></asp:TextBox></div>
                        <div class="col-md-6"></div>
                    </div>
                    
                    <br />

                    <div class="row">
                        <div class="col-md-1">Section:</div>
                        <div class="col-md-5"><asp:TextBox runat="server" ID="sectionTextBox"></asp:TextBox></div>
                        <div class="col-md-6"></div>
                    </div>
                    
                    <br />

                    <div class="row">
                        <div class="col-md-1">Instructor:</div>
                        <div class="col-md-5"><asp:TextBox runat="server" ID="instructorTextBox"></asp:TextBox></div>
                        <div class="col-md-6"></div>
                    </div>

                    <br />
                    <div class="row">
                        <div class="col-md-1">Number of Hours:</div>
                        <div class="col-md-5"><asp:TextBox runat="server" ID="numOfHoursTextBox"></asp:TextBox></div>
                        <div class="col-md-6"></div>
                    </div>

                    

                    </div>  
                   
                   <%--Modal Footer--%>  
                     <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <asp:LinkButton runat="server" ID="saveButton" OnClick="saveButton_Click" Text="Save" CssClass="btn btn-primary"></asp:LinkButton>
                         
                    </div>

               </div>
                
                   
                
               
                
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnAddSchedule" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        </div>
        <%-- End of Add Schedule Modal --%>




         <%-- Edit Schedule Modal --%>
        <div aria-hidden="true" aria-labelledby="ModalHeaderLabel" role="dialog" data-backdrop="static"
        data-keyboard="false" tabindex="-1" class="modal modal-xlarge" id="editScheduleModal"
        style="display: none;">

        <asp:UpdatePanel runat="server" id="editSchedulePanel" UpdateMode="Conditional">
            <ContentTemplate>
               <div class="modal-content" style="margin-top:100px; padding:10px">
                   <div class="modal-header">
                      <button type="button" class="close" data-dismiss="modal">&times;</button>
                      <h4 class="modal-title"><b>Edit Schedule</b></h4>
                    </div>
                   
                   <div class="modal-body">
                    
                    
                    <div class="row">
                        
                        <div class="col-md-1">
                            <asp:Label runat="server" ID="editRoomID" Visible="false"></asp:Label>
                            Room Number:
                        </div>
                        <div class="col-md-5"><%--<asp:TextBox runat="server" ID="roomNumberTxtBox"></asp:TextBox>--%>
                            <asp:DropDownList runat="server" ID="editRoomDDL">
                                
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-6"></div>
                    </div>

                    <br />
                    
                     <div class="row">
                        <div class="col-md-1">Days:</div>
                        <div class="col-md-5">
                            <asp:DropDownList runat="server" ID="editDayDDL">
                                  <asp:ListItem Text="" Value=""></asp:ListItem>
                                 <asp:ListItem Text="Monday" Value="1"></asp:ListItem>
                                 <asp:ListItem Text="Tuesday" Value="2"></asp:ListItem>
                                 <asp:ListItem Text="Wednesday" Value="3"></asp:ListItem>
                                 <asp:ListItem Text="Thursday" Value="4"></asp:ListItem>
                                 <asp:ListItem Text="Friday" Value="5"></asp:ListItem>
                                 <asp:ListItem Text="Saturday" Value="6"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-6"></div>
                    </div>
                    <br />

                    <div class="row">
                        <div class="col-md-1">Time:</div>
                        <div class="col-md-5">
                            <div class="form-group">
                                <label for="timePicker">Start of Class</label>
                                <div class='input-group date' id='timePicker2' style="width:200px;">
                                                <%--<input id="time" type='text' class="form-control" />--%>
                                                <asp:TextBox runat="server" ID="editStartTxt" CssClass="form-control"></asp:TextBox>
                                                <span class="input-group-addon">
                                                    <span class="glyphicon glyphicon-time"></span>
                                                </span>
                                 </div>
                             </div>

                            <div class="form-group">
                                <label for="timePicker1">End of Class</label>
                                <div class='input-group date' id='timePicker3' style="width:200px;">
                                                <%--<input id="time" type='text' class="form-control" />--%>
                                                <asp:TextBox runat="server" ID="editEndTxt" CssClass="form-control"></asp:TextBox>
                                                <span class="input-group-addon">
                                                    <span class="glyphicon glyphicon-time"></span>
                                                </span>
                                 </div>
                             </div>

                        </div>
                        <div class="col-md-6"></div>
                    </div>

                    <br />

                    <div class="row">
                        <div class="col-md-1">Subject Code:</div>
                        <div class="col-md-5"><asp:TextBox runat="server" ID="editSubjectTxt" Enabled="false"></asp:TextBox></div>
                        <div class="col-md-6"></div>
                    </div>
                    
                    <br />

                    <div class="row">
                        <div class="col-md-1">Section:</div>
                        <div class="col-md-5"><asp:TextBox runat="server" ID="editSectionTxt"></asp:TextBox></div>
                        <div class="col-md-6"></div>
                    </div>
                    
                    <br />

                    <div class="row">
                        <div class="col-md-1">Instructor:</div>
                        <div class="col-md-5"><asp:TextBox runat="server" ID="editInstructorTxt"></asp:TextBox></div>
                        <div class="col-md-6"></div>
                    </div>

                    <br />
                    <div class="row">
                        <div class="col-md-1">Number of Hours:</div>
                        <div class="col-md-5"><asp:TextBox runat="server" ID="editNumTxt"></asp:TextBox></div>
                        <div class="col-md-6"></div>
                    </div>

                    

                    </div>  
                   
                   <%--Modal Footer--%>  
                     <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <asp:LinkButton runat="server" ID="editBtn" OnClick="editBtn_Click" Text="Edit" CssClass="btn btn-primary"></asp:LinkButton>
                         
                    </div>

               </div>
                
            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="btnAddSchedule" EventName="Click" />--%>
            </Triggers>
        </asp:UpdatePanel>
        </div>
        <%-- End of Edit Schedule Modal --%>





    <div class="row">
        <div class="col-md-4">
            <asp:Button ID="btnAddSchedule" runat="server" Text="Add Schedule"  OnClick="btnAddSchedule_Click" CssClass="btn btn-primary"/>
        </div>
        <div class="col-md-4">
        </div>
        <div class="col-md-4">
            <asp:Button ID="btnFindSchedule" runat="server" Text="Find Schedule" CssClass="pull-right btn btn-primary" OnClick="btnFindSchedule_Click" />
            <asp:TextBox ID="searchTextBox" runat="server" CssClass="pull-right marginSearch" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
        </div>


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
    </div>
    <br />


    <asp:UpdatePanel ID="schedPanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>

    <div class="table-responsive">
    <asp:GridView runat="server" ID="schedGrid" AllowPaging="true" AutoGenerateColumns="false" PagerStyle-CssClass="pagination-ys" PageSize="30" DataKeyNames="schedID" CssClass="table table-bordered" OnRowDataBound="schedGrid_RowDataBound" OnRowCommand="schedGrid_RowCommand" OnPageIndexChanging="schedGrid_PageIndexChanging"
         EmptyDataText="There are no schedules found" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-Font-Size="Large" EmptyDataRowStyle-BackColor="White" EmptyDataRowStyle-ForeColor="#014fb3" EmptyDataRowStyle-BorderColor="Black" >
        <Columns>
            <asp:BoundField DataField="schedID" Visible="false" />
            <asp:BoundField DataField="roomNumber" HeaderText="Room Number"/>
            <asp:BoundField DataField="days" HeaderText="Days"/>
            <asp:BoundField DataField="time" HeaderText="Time"/>
            <asp:BoundField DataField="subjectCode" HeaderText="Subject Code"/>
            <asp:BoundField DataField="section" HeaderText="Section"/>
            <asp:BoundField DataField="instructor" HeaderText="Instructor"/>
            <asp:BoundField DataField="numOfHours" HeaderText="Number of Hours" Visible="false"/>
        <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                   
                    <asp:LinkButton ID="updateButton" runat="server" CommandName="onUpdate" CommandArgument='<%#Eval("schedID") %>' CssClass="btn btn-primary">Update</asp:LinkButton>
                    <asp:LinkButton ID="deleteButton" runat="server" CommandName="onDelete" CommandArgument='<%#Eval("schedID") %>' CssClass="btn btn-danger"  OnClientClick="return confirm('Are you sure you want to delete this schedule?');">Delete</asp:LinkButton>
                </ItemTemplate>
        </asp:TemplateField>
        </Columns>
    <HeaderStyle BackColor="#014fb3" ForeColor="White" />
    <RowStyle BackColor="White" />
<%--    <AlternatingRowStyle BackColor="Gainsboro" />--%>
    </asp:GridView>
    </div>
    </ContentTemplate>
        <Triggers>
           <asp:AsyncPostBackTrigger  ControlID="saveButton" EventName="Click" /> 
            <asp:AsyncPostBackTrigger ControlID="btnFindSchedule" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>


            </div> <%--end of panel body--%>
         </div> <%--end of panel-default--%>
    </div><%--end of page-inner--%>
</asp:Content>
