<%@ Page Title="Get Schedule" Language="C#" MasterPageFile="~/Masters/DefaultMaster.Master" AutoEventWireup="true" CodeBehind="GetSchedule.aspx.cs" Inherits="Thesis.GetSchedule"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

        function showVisible() {

            document.getElementById("pdfButton").style.display = "block";
        }
    </script>

    <style>

        .custom{
            padding-top:100px;
        }

        .select-floor {
            height: 30px;
        }

        .date-picker {
            height: 30px;
        }
        
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div id="page-inner">
                     <div class="row">
           <div class="col-md-12">
                <h2 style="color:deepskyblue; padding-left:15px; font-family:Megafont;">Get Schedule</h2>  
            </div>
            </div>
    <hr/>   
    <div class="pane panel-primary">
    <div class="panel panel-heading">
        <%--Get Schedule--%>
    </div>
    <div class="panel panel-body">
    <div class="container-fluid"  style="margin-left:1px; margin-right:1px;">
        <div class="row">
            <div class="col-md-1"><asp:Button ID="pdfButton" runat="server" Text="Generate PDF" CssClass="btn btn-info btn-md" OnClick="pdfButton_Click"/></div>
            <div class="col-md-2" style="text-align:right; font-size:medium">Select Date:</div>
            <div class="col-md-1">
                
                <div class="input-group input-daterange" style="margin-left:-25px;" data-provide="datepicker" data-date-format="yyyy-mm-dd" data-date-max-view-mode="days" data-date-min-view-mode="days">
                        <asp:TextBox ID="dateTextBox" CssClass="date-picker" runat="server" placeholder="Choose Date"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-2 col-md-offset-1" style="text-align:right; font-size:medium">Select Floor:</div>
            <div class="col-md-3" style="margin-right:20px;" >
                <asp:DropDownList ID="ddlFloors" runat="server" CssClass="select-floor col-md-8">
                                <asp:ListItem Text="" Value="0"></asp:ListItem>
                               <%-- <asp:ListItem Text="Group 1(Rm 1001-1008)" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Group 2(Rm 407,Rm 501-506)" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Group 3(CINTIQ,Rm 706,CL1-Cl2,MMA1-MMA3,IMAC)" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Group 4(AUDITORIUM,FM,TZ-A,TZ-B)" Value="4"></asp:ListItem>--%>
               </asp:DropDownList>
            </div>

            <div><asp:Button ID="searchSchedButton" runat="server"  Text="Search" CssClass="btn btn-info btn-md pull-right" OnClick="searchSchedButton_Click" /></div>
        </div>
    </div>

    <br />


    <asp:UpdatePanel ID="schedPanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
  
    <div class="table table-responsive">
        <asp:GridView runat="server" ID="schedGrid" AllowPaging="true" AutoGenerateColumns="false" PageSize="100" DataKeyNames="schedID" CssClass="table table-bordered" OnRowDataBound="schedGrid_RowDataBound" EmptyDataText="There are no schedules found" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-Font-Size="Large"
            EmptyDataRowStyle-BackColor="White" EmptyDataRowStyle-ForeColor="#014fb3" EmptyDataRowStyle-BorderColor="Black" Width="1200px" OnPageIndexChanging="schedGrid_PageIndexChanging" RowStyle-Wrap="true">
            <Columns>
                <asp:BoundField DataField="schedID" Visible="false" HeaderText="ID"/>
                <asp:BoundField DataField="roomNumber" HeaderText="Room Number" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Bottom" ControlStyle-CssClass="custom" ItemStyle-Width="100"/>
                <%--<asp:BoundField DataField="days" HeaderText="Days"/>--%>


                <asp:BoundField DataField="time" HeaderText="Time" ItemStyle-Width="120"/>
                <asp:BoundField DataField="subjectCode" HeaderText="Subject Code" ItemStyle-Width="120"/>
                <asp:BoundField DataField="section" HeaderText="Section" ItemStyle-Width="100"/>
                <asp:BoundField DataField="instructor" HeaderText="Instructor" ItemStyle-Width="200"/>

                <asp:BoundField DataField="start" HeaderText="Start" ItemStyle-Width="100" />
                <asp:BoundField DataField="middle" HeaderText="Middle" ItemStyle-Width="100" />
                <asp:BoundField DataField="end" HeaderText="End" ItemStyle-Width="100" />
                <asp:BoundField DataField="remarks" HeaderText="Remarks" ItemStyle-Width="100" />



               <%-- <asp:BoundField DataField="numOfHours" HeaderText="Number of Hours"/>--%>
            </Columns>
            <HeaderStyle BackColor="#014fb3" ForeColor="White" />
            <RowStyle BackColor="White" />
        </asp:GridView>
    </div>

    </ContentTemplate>
        <Triggers>
           <asp:AsyncPostBackTrigger  ControlID="searchSchedButton" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    </div>
    </div>
    </div>
</asp:Content>
