<%@ Page Title="Make Up Scheduler" Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/DefaultMaster.Master" 
 CodeBehind="MakeupForm.aspx.cs" Inherits="Thesis.MakeupForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="<%=ResolveUrl("~/") %>Assets/js/moment.min.js"></script>   
    <script type="text/javascript" src="<%=ResolveUrl("~/") %>Assets/js/bootstrap-datetimepicker.min.js"></script>  
    <link rel="stylesheet" href="<%=ResolveUrl("~/") %>Assets/css/bootstrap-datetimepicker.css" type="text/css"/>
<script type="text/javascript">
        //$(function () {
        //    $("#makeUpClassDate").datepicker({
        //        altField: "#makeUpClassDay",
        //        altFormat: "DD"
        //    });
    //});

    //$(function () {
    //    $('#accordion').on('shown.bs.collapse', function (e) {
    //        var offset = $('.panel.panel-default > .panel-collapse.in').offset();
    //        if (offset) {
    //            $('html,body').animate({
    //                scrollTop: $('.panel-title a').offset().top - 20
    //            }, 500);
    //        }
    //    });
    //});
    jQuery('#absentDate').on('input', function () {
        // do your stuff // click button chckDate here
        $("#btnSubmit").click(function () {
            //alert("button");
        });
    });

    jQuery('#makeupClassDate').on('input', function () {
        // do your stuff // click button chckDate here
        $("#btnSubmit").click(function () {
            //alert("button");
        });
    });

    jQuery('#peAbsentDate').on('input', function () {
        // do your stuff // click button chckDate here
        $("#btnSubmit").click(function () {
            //alert("button");
        });
    });

    jQuery('#peMakeupClassDate').on('input', function () {
        // do your stuff // click button chckDate here
        $("#btnSubmit").click(function () {
            //alert("button");
        });
    });

    function filedMakeup() {
        $.growl.error({ title: "Error!", message: "Make-up Class Already Filed." });
    }

    function validInput() {
        $.growl.error({ title: "Error!", message: "Invalid Inputs. Fields are required." });
    }

    function isEndStartTerm() {
        $.growl.error({ title: "Error!", message: "Invalid Date. Not included in term dates" });
    }

    function invalidDates() {
        $.growl.error({ title: "Error!", message: "Can't have classes on this date." });
    }

    function success() {
        $.growl.notice({ title: "Success!", message: "Date added!" });
    }

    function makeupSuccess() {
        $.growl.notice({ title: "Success!", message: "Makeup form submitted. Pending for approval." });
    }

    function pageLoad(sender, args) {
        $('#dateTimePicker').datetimepicker({
            format: 'MMMM DD YYYY'
        });

        $('#dateTimePicker2').datetimepicker({
            format: 'MMMM DD YYYY'
        });

        $('#dateTimePicker3').datetimepicker({
            format: 'MMMM DD YYYY'
        });

        $('#dateTimePicker4').datetimepicker({
            format: 'MMMM DD YYYY'
        });
    }
</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div id="page-inner">
           <div class="row">
           <div class="col-md-12">
               <h2 style="color:deepskyblue; font-family:Megafont; padding-left:5px;">Make-up Form</h2>  
               <h4 style="font-family:Arial; color:red; padding-left:5px;"><asp:Label ID="facultyId" runat="server"></asp:Label><br/>
                   <asp:Label ID="facultyName" runat="server"></asp:Label></h4>
               <%--<h4 style="font-family:Arial;"></asp:Label></h4>--%>

               <%-- Add info : User : faculty name, id--%>
           </div>
           </div>
    <hr/>   
    <div class="row">
        <div class="col-md-12">
                 <div class="panel-group" id="accordion">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne" class="collapsed">Schedule Make-up for Regular Classes</a>
                                        </h4>
                                    </div>
                                    <div id="collapseOne" class="panel-collapse in" style="height: 0px;">
                                        <div class="panel-body">
                                            <%-- Content of Makeup Class for Regular Rooms Placed Here --%>
                                                    <div class="col-md-6">

<%--                   <div class="form-group">
                           <asp:Panel runat="server" ID="Panel8">
                           <asp:Label ID="invalidLoginLabel" runat="server" Visible="false"></asp:Label>
                           </asp:Panel>

                   </div>--%>
                <div class="form-group">
                    <label for="holidayDate">Absent Date</label> 
                    <%--<label style="padding-left:125px;">Day</label>--%>
                    <br/>
                    
                    <div class='input-group date' id='dateTimePicker3' style="display:inline;">
                                            <%--<input id="time" type='text' class="form-control" />--%>
                                            <asp:TextBox runat="server" ID="absentDate" Placeholder="Choose Date" CssClass="form-control" style="max-width:550px; display:inline;" required="required"></asp:TextBox>
                                            <span class="input-group-addon" style="display:inline; margin-top:10px; padding-top:10px; padding-bottom:10px;" >
                                                <span class="glyphicon glyphicon-calendar" style="padding-top:8px;"></span>
                                            </span>
                                        </div>  
                </div>
                <%--<div class="form-group">
                    <label id="fac_id" runat="server" visible="false">File in Behalf of</label>--%>
                    <%--<asp:TextBox ID="facultyIdSet" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>--%>
                    <%--<asp:DropDownList ID="facultyId_DDL" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                        <asp:ListItem Text="Choose Faculty" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </%--div>--%>
                <div class="form-group">
                    <label>Subject Code</label>
                    <asp:TextBox ID="subjCodeTxtbox" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Section</label>
                    <asp:TextBox ID="sectionTxtbox" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                </div>
                    
                <div class="form-group">
                    <label for="holidayDate">Date and Time</label> 
                    <%--<label style="padding-left:125px;">Day</label>--%>
                    <label id="timeTitle" style="padding-left:250px;" runat="server">Room</label><br/>
                    
                    <div class='input-group date' id='dateTimePicker' style="display:inline;">
                                            <%--<input id="time" type='text' class="form-control" />--%>
                                            <asp:TextBox runat="server" ID="makeUpClassDate" Placeholder="Choose Date" CssClass="form-control" style="max-width:140px; display:inline;" required="required"></asp:TextBox>
                                            <span class="input-group-addon" style="display:inline; margin-top:10px; padding-top:10px; padding-bottom:10px;" >
                                                <span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>  
                    <%--<asp:UpdatePanel runat="server" ID="roomUpdatePanel" UpdateMode="Conditional">
                    <ContentTemplate>--%>

                    <%--<asp:TextBox ID="makeUpClassDay" runat="server" CssClass="form-control" ClientIDMode="Static" style="max-width:150px; display:inline;"></asp:TextBox>--%>
                    <%--<asp:TextBox ID="makeUpClassTime" runat="server" CssClass="form-control" ClientIDMode="Static" style="max-width:150px; display:inline;"></asp:TextBox>--%>
                    <asp:DropDownList ID="makeupTimeDDL" runat="server" CssClass="form-control" style="max-width:155px; display:inline;">
                        <asp:ListItem Text="Choose Time"></asp:ListItem>
                        <asp:ListItem Text="7:30AM-11:00AM" Value="0730-1100"></asp:ListItem>
                        <asp:ListItem Text="11:00AM-2:30PM" Value="1100-1430"></asp:ListItem>
                        <asp:ListItem Text="2:30PM-6:00PM" Value="1430-1800"></asp:ListItem>
                        <asp:ListItem Text="6:00PM-9:30PM" Value="1800-2130"></asp:ListItem>
                    </asp:DropDownList>

                    <asp:UpdatePanel runat="server" ID="chckClassSchedUpdatePanel" UpdateMode="Conditional" style="max-width:155px; display:inline;">
                    <ContentTemplate>
                    <asp:DropDownList ID="makeupRoomDDL" runat="server" CssClass="form-control" style="max-width:155px; display:inline;" Enabled="false">
                        <asp:ListItem Text="Choose Room"></asp:ListItem>
                    </asp:DropDownList>
                    </ContentTemplate>
                        <Triggers><asp:AsyncPostBackTrigger ControlID="btnChkSched" EventName="Click"/></Triggers>
                    </asp:UpdatePanel>
                     <asp:LinkButton ID="btnChkSched" CssClass="btn btn-default" runat="server" style="max-width:200px;"
                        OnClick="btnChkSched_Click">Check Schedule</asp:LinkButton>
                </div>
      <%--          <div class="form-group">
                   
                </div>--%>
                <div class="form-group">
                    <label>Reason</label>
                    <asp:TextBox ID="reasonTxtbox" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                </div>
                <br/>
                <div class="form-group">
                    <asp:LinkButton ID="PendingSchedButton" CssClass="btn btn-default" runat="server" OnClick="PendingSchedButton_Click">Submit</asp:LinkButton>
                </div>
            </div><br/><br/>
                                    <%-- END OF 1ST COLLAPSIBLE CONTENTS --%>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">Schedule Make-up for P.E Classes</a>
                                        </h4>
                                    </div>
                                    <div id="collapseTwo" class="panel-collapse collapse" style="height: auto;">
                                        <div class="panel-body">
                                            <%-- Contents of Makeup Class For P.E Classes Placed Here--%>
                                            <div class="col-md-6">
                                        <div class="form-group">
                    <label for="holidayDate">Absent Date</label> 
                    <%--<label style="padding-left:125px;">Day</label>--%>
                    <br/>
                    
                    <div class='input-group date' id='dateTimePicker4' style="display:inline;">
                                            <%--<input id="time" type='text' class="form-control" />--%>
                                            <asp:TextBox runat="server" ID="peAbsentDate" Placeholder="Choose Date" CssClass="form-control" style="max-width:550px; display:inline;" required="required"></asp:TextBox>
                                            <span class="input-group-addon" style="display:inline; margin-top:10px; padding-top:10px; padding-bottom:10px;" >
                                                <span class="glyphicon glyphicon-calendar" style="padding-top:8px;"></span>
                                            </span>
                                        </div>  
                </div>

                                          <%-- <div class="form-group">
                    <label id="pe_fac_id" runat="server" visible="false">File in Behalf of</label>--%>
                    <%--<asp:TextBox ID="facultyIdSet" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>--%>
                   <%-- <asp:DropDownList ID="peFacultyIdDDL" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                        <asp:ListItem Text="Choose Faculty" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>--%>
                <div class="form-group">
                    <label>Subject Code</label>
                    <asp:TextBox ID="peSubjectCode" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Section</label>
                    <asp:TextBox ID="peSection" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                </div>
                    
                <div class="form-group">
                    <label for="peHolidayDate">Date and Time</label> <br/>
                    <%--<label style="padding-left:125px;">Day</label>--%>
                    <%--<label id="Label1" style="padding-left:250px;" runat="server">Room</label><br/>--%>
                    
                    <div class='input-group date' id='dateTimePicker2' style="display:inline;">
                                            <%--<input id="time" type='text' class="form-control" />--%>
                                            <asp:TextBox runat="server" ID="peMakeupClassDate" Placeholder="Choose Date" CssClass="form-control" style="max-width:200px; display:inline;" required="required"></asp:TextBox>
                                            <span class="input-group-addon" style="display:inline; margin-top:10px; padding-top:10px; padding-bottom:10px;" >
                                                <span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>  
                    <%--<asp:UpdatePanel runat="server" ID="roomUpdatePanel" UpdateMode="Conditional">
                    <ContentTemplate>--%>

                    <%--<asp:TextBox ID="makeUpClassDay" runat="server" CssClass="form-control" ClientIDMode="Static" style="max-width:150px; display:inline;"></asp:TextBox>--%>
                    <%--<asp:TextBox ID="makeUpClassTime" runat="server" CssClass="form-control" ClientIDMode="Static" style="max-width:150px; display:inline;"></asp:TextBox>--%>
                    <asp:DropDownList ID="peMakeupStartTimeDDL" runat="server" OnSelectedIndexChanged="peMakeupStartTimeDDL_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" style="max-width:190px; display:inline;" AppendDataBoundItems="True">
                    <asp:ListItem Text="Start Time" Value="0"></asp:ListItem>
                    </asp:DropDownList>

                    <asp:UpdatePanel runat="server" ID="startTimeUpdatePanel" UpdateMode="Conditional" style="max-width:190px; display:inline;" >
                    <ContentTemplate>
                
                    <asp:DropDownList ID="peMakeupEndTimeDDL" runat="server" CssClass="form-control" style="max-width:190px; display:inline;" AppendDataBoundItems="True">
                        <asp:ListItem Text="End Time" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="peMakeupStartTimeDDL" EventName="SelectedIndexChanged"/>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="form-group">
                <asp:UpdatePanel runat="server" ID="peSchedUpdatePanel" UpdateMode="Conditional" style="max-width:190px; display:inline;" >
                <ContentTemplate>
                       <label runat="server">Room</label><br/>
                       <asp:DropDownList ID="peMakeupRoomDDL" runat="server" CssClass="form-control" style="max-width:495px; display:inline;" Enabled="false">
                        <asp:ListItem Text="Choose Room"></asp:ListItem>
                    </asp:DropDownList>
                </ContentTemplate>
                    <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="peChkSchedBtn" EventName="Click"/>
                        </Triggers>
                </asp:UpdatePanel>
                     <asp:LinkButton ID="peChkSchedBtn" CssClass="btn btn-default" runat="server" style="max-width:200px;"
                        OnClick="peChkSchedBtn_Click" >Check Schedule</asp:LinkButton>
                </div>
                <div class="form-group">
                    <label>Reason</label>
                    <asp:TextBox ID="peReason" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <br/>
                <div class="form-group">
                    <asp:LinkButton ID="pePendingSchedBtn" CssClass="btn btn-default" runat="server" OnClick="pePendingSchedBtn_Click">Submit</asp:LinkButton>
                </div>
            </div></div><br/><br/>
                                            <%-- END OF 2ND COLLAPSIBLE CONTENTS --%>
                                        </div>
                                    </div>
                                </div>
                              <%--  <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseThree" class="collapsed">Collapsible Group Item #3</a>
                                        </h4>
                                    </div>
                                    <div id="collapseThree" class="panel-collapse collapse">
                                        <div class="panel-body">
                                            Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
                                        </div>
                                    </div>
                                </div>--%>
                            </div>
                        </div>
        </div>
    </div>
</asp:Content>

