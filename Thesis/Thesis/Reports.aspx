<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DefaultMaster.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="Thesis.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
          <!-- FusionCharts script tag -->
     <script type="text/javascript" src="<%=ResolveUrl("~/") %>Assets/js/fusioncharts.js"></script>    
    <script type="text/javascript" src="<%=ResolveUrl("~/") %>Assets/js/fusioncharts.charts.js"></script>
     <script type="text/javascript" src="<%=ResolveUrl("~/") %>Assets/js/fusioncharts.theme.zune.js"></script>
          <!-- End -->


    <script>

        function showSuccessMsg() {

            $growl.warning({ title: "Success!", message: "Password successfully changed" });
        }


        function incomplete() {

            $.growl.warning({ message: "Please choose the school year" });
        }
    </script>

    <style>
        .panel {
            border: 0;
            
        }
        .panel .panel-heading {
        
            background-color:#014fb3;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div id="page-inner">
                
                <div class="row">
           <div class="col-md-12">
                <h2 style="color:deepskyblue; padding-left:15px; font-family:Megafont;">Reports</h2>  
            </div>
            </div>
    <hr/>   

        <div class="panel panel-primary">
        <div class="panel-heading"><%--Reports--%></div>

        <div class="row" style="margin-top:5px;">
            <div class="col-md-5">


                <asp:UpdatePanel ID="barChartPanel" runat="server" UpdateMode="Conditional">

                    <ContentTemplate>
                         <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                         <br />
                        <%--<asp:RadioButtonList ID="barChartbtn" runat="server" OnSelectedIndexChanged="barChartbtn_SelectedIndexChanged" AutoPostBack="true" >
                            <asp:ListItem Text="2D Bar Chart" Value="2D" ></asp:ListItem>
                            <asp:ListItem Text="3D Bar Chart" Value="3D" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>--%>

                    </ContentTemplate>
                   
                    <Triggers>
                        
                    </Triggers>

                </asp:UpdatePanel>



                


            </div>
            <div class="col-md-5" style="padding-left:0px;">
                


                <asp:UpdatePanel ID="columnChartPanel" runat="server" UpdateMode="Conditional">

                    <ContentTemplate>
                         <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                         <br />
                        <%--<asp:RadioButtonList ID="columnChartbtn" runat="server" OnSelectedIndexChanged="columnChartbtn_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="2D Bar Chart" Value="2D" ></asp:ListItem>
                            <asp:ListItem Text="3D Bar Chart" Value="3D" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>--%>

                    </ContentTemplate>
                   
                    <Triggers>
                       
                    </Triggers>

                </asp:UpdatePanel>



                


            </div>
            
        </div>


        <br />

        <div class="row">
            <div class="col-md-6">


                <asp:UpdatePanel ID="lineChartPanel" runat="server" UpdateMode="Conditional">

                    <ContentTemplate>
                         <asp:Literal ID="Literal3" runat="server"></asp:Literal>
                    </ContentTemplate>
                   
                    <Triggers>
                        
                    </Triggers>

                </asp:UpdatePanel>
            </div>
             <div class="col-md-6"></div>

        </div>

        <br />
        <div class="row">
            <div class="col-md-12">
                            <asp:UpdatePanel runat="server" ID="downloadPanel" UpdateMode="Conditional">
                                   <ContentTemplate>
                                       <asp:GridView runat="server" ID="downloadGrid" AllowPaging="true" AutoGenerateColumns="false" PageSize="15" DataKeyNames="ID" CssClass="table table-bordered" OnRowCommand="downloadGrid_RowCommand"  OnRowDataBound="downloadGrid_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="ID" Visible="false"/>
                                                    <asp:BoundField DataField="file" HeaderText="Downloadable Reports"/>
                                                 <asp:TemplateField HeaderText="Semester">
                                                     

                                                      <ItemTemplate>
                                                         <asp:DropDownList ID="semDDL" runat="server">
                                                             <asp:ListItem Text="1st" Value="1"></asp:ListItem>
                                                             <asp:ListItem Text="2nd" Value="2">
                                                             </asp:ListItem>
                                                             <asp:ListItem Text="3rd" Value="3"></asp:ListItem>
                                                         </asp:DropDownList>



                                                        

                                                          



                                                     </ItemTemplate>

                                                 </asp:TemplateField>


                                                  <asp:TemplateField HeaderText="Year">

                                                      <ItemTemplate>
                                                          <div class="input-group input-daterange" data-provide="datepicker" data-date-format="yyyy" data-date-max-view-mode="years" data-date-min-view-mode="years">
                                                                <asp:TextBox ID="schoolYearStart" runat="server" Width="80"></asp:TextBox>
                                                                <asp:Label ID="toLabel" runat="server">-</asp:Label>
                                                                <asp:TextBox ID="schoolYearEnd" runat="server" Width="80"></asp:TextBox>
                                                          </div>

                                                      </ItemTemplate>


                                                  </asp:TemplateField>

                                                 <asp:TemplateField>  
                                                     
                                                    
                                                      
                                                       <ItemTemplate>
                                                           <asp:LinkButton runat="server" ID="downloadBtn" CommandName="onDownload" CommandArgument='<%#Eval("ID") %>' CssClass="btn btn-primary">Download</asp:LinkButton>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                    
                                    
                                    
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

        
            
        </div>
    </div>
    
</asp:Content>
