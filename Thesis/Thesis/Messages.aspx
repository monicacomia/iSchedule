<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DefaultMaster.Master" AutoEventWireup="true" 
    CodeBehind="Messages.aspx.cs" Inherits="Thesis.Messages" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .modal-dialog{
            position:fixed;
            bottom:0px;
            right:0px;
            margin:0px;
        }

        .MultiLineTextBox
        {
        resize: none;

        }

        .position {
            padding-left:40px;
        }

        .position2 {
             padding-left:60px;
        }

        .table-borderless > tbody > tr > td,
        .table-borderless > tbody > tr > th,
        .table-borderless > tfoot > tr > td,
        .table-borderless > tfoot > tr > th,
        .table-borderless > thead > tr > td,
        .table-borderless > thead > tr > th {
            border: none;
        }
    </style>


   



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="panel panel-primary">

        <div class="panel-heading">Messages</div>

        <div class="panel-body">

        <%-- Reply Message Modal --%>
        <div aria-hidden="true" aria-labelledby="ModalHeaderLabel" role="dialog" data-backdrop="static"
        data-keyboard="false" tabindex="-1" class="modal modal-xlarge" id="replyModal"
        style="display: none;" >

        <asp:UpdatePanel runat="server" id="replyPanel">
            <ContentTemplate>
               <div class="modal-dialog">
               <div class="modal-content" style="margin-top:100px; padding:10px;" >
                   <div class="modal-header">
                      <button type="button" class="close" data-dismiss="modal">&times;</button>
                      <h4 class="modal-title">
                          <asp:Label runat="server" ID="headerlbl"></asp:Label>
                          
                      </h4>
                    </div>
                   
                   <div class="modal-body">
                    
                     <asp:GridView runat="server" ID="replyGrid" AllowPaging="true" AutoGenerateColumns="false" PageSize="15" DataKeyNames="msgID,hash" CssClass="table table-bordered" OnRowCommand="replyGrid_RowCommand"  OnRowDataBound="replyGrid_RowDataBound" BorderStyle="None" >
                        
                       <Columns>
                           
                            <asp:BoundField DataField="msgID" Visible="false"  />
                            <asp:BoundField DataField="hash" Visible="false" />
                            <asp:BoundField DataField="msg"/>
                            <asp:BoundField DataField="datetime" ItemStyle-Width="60px" />
                            
                           
                        </Columns>
                        
                   
                    </asp:GridView>



                    </div><%-- End of Modal Body  --%>
                   
                   <%--Modal Footer--%>  
                     <div class="modal-footer" >
                        <asp:TextBox runat="server" ID="replyMsg" TextMode="MultiLine" Width="500px"></asp:TextBox>
                        <asp:LinkButton runat="server" ID="replyBtn" OnClick="replyBtn_Click" Text="Reply" CssClass="btn btn-primary"></asp:LinkButton>    
                    </div>

               </div>
                </div>
            </ContentTemplate>
            <Triggers>
                
                
            </Triggers>
        </asp:UpdatePanel>
        </div>


















        <%-- Compose Message Modal --%>
        <div aria-hidden="true" aria-labelledby="ModalHeaderLabel" role="dialog" data-backdrop="static"
        data-keyboard="false" tabindex="-1" class="modal modal-xlarge" id="composeModal"
        style="display: none;">

        <asp:UpdatePanel runat="server" id="composePanel">
            <ContentTemplate>
               <div class="modal-dialog">
               <div class="modal-content" style="margin-top:100px; padding:10px;" >
                   <div class="modal-header">
                      <button type="button" class="close" data-dismiss="modal">&times;</button>
                      <h4 class="modal-title"><b>Compose Message</b></h4>
                    </div>
                   
                   <div class="modal-body">
                    
                    <div class="row">
                        <div class="col-md-2">To:</div>
                        <div class="col-md-5"><asp:TextBox runat="server" ID="toTxt"></asp:TextBox></div>
                        <div class="col-md-6"></div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-2">Subject</div>
                        <div class="col-md-5"><asp:TextBox runat="server" ID="subjectTxt"></asp:TextBox></div>
                        <div class="col-md-6"></div>
                    </div>
                    <hr />
                    <div class="row">
                        
                        <div class="col-md-12"><asp:TextBox runat="server" ID="msgTxt" CssClass="MultiLineTextBox" TextMode="MultiLine" Width="550px" Height="100px"></asp:TextBox></div>
                        
                    </div>



                    </div><%-- End of Modal Body  --%>
                   
                   <%--Modal Footer--%>  
                     <div class="modal-footer" >
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <asp:LinkButton runat="server" ID="sendButton" OnClick="sendButton_Click" Text="Send" CssClass="btn btn-primary"></asp:LinkButton>
                         
                    </div>

               </div>
                </div>
            </ContentTemplate>
            <Triggers>
                
                <asp:AsyncPostBackTrigger ControlID="composeBtn" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        </div>



        
        

        <div class="row">
            <div class="col-md-1"><asp:Button runat="server" Text="Compose" ID="composeBtn" OnClick="composeBtn_Click" CssClass="btn btn-primary" /></div>
            
            <div class="col-md-1"><asp:Button runat="server" Text="Refresh Inbox" ID="refreshBtn" OnClick="refreshBtn_Click" CssClass="btn btn-secondary"/></div>
            
            <asp:UpdatePanel runat="server" ID="visibilityPanel" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="col-md-1 position"><asp:Button runat="server" Text="Mark As Read" ID="readBtn" OnClick="readBtn_Click" CssClass="btn btn-secondary"/></div>
                    <div class="col-md-1 position2"><%--<asp:LinkButton runat="server" ID="deleteBtn2">Delete
                                            </span>
                                          </asp:LinkButton>--%>
                        <asp:Button runat="server" ID="deleteBtn" Text="Delete" OnClick="deleteBtn_Click" CssClass="btn btn-secondary" />

                    </div>
                    <%--<asp:DropDownList runat="server" ID="choicesDDL">
                        
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>--%>
                </ContentTemplate>
                
                <Triggers>
                   
                </Triggers>
            </asp:UpdatePanel>
            <div class="col-md-8"></div>
        </div>

        <div class="row-fluid">
            <%--<div class="col-md-2">
                <br />
                    <ul>
                        <li>Inbox</li>
                        <li>Archive</li>
                        <li>Trash</li>
                    </ul>



            </div>--%>
            <div class="col-md-12">
                <br />
                <asp:UpdatePanel ID="inboxPanel" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>


                    <asp:GridView runat="server" ID="inboxGrid" AllowPaging="true" AutoGenerateColumns="false" PageSize="15" DataKeyNames="msgID,isRead,hash,isDeleted" CssClass="table table-borderless" 
                         EmptyDataText="Inbox is Empty" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-Font-Size="Large" EmptyDataRowStyle-BackColor="White" EmptyDataRowStyle-ForeColor="#014fb3" EmptyDataRowStyle-BorderColor="Black" OnRowCommand="inboxGrid_RowCommand" >
                        
                       <Columns>
                           <asp:TemplateField ItemStyle-Width="100">
                               <HeaderTemplate>
                                   <%--<input id="chkAll" type="" onclick="CheckAll(this)" runat="server" />--%>
                                   <asp:DropDownList ID="selectDDL" runat="server" OnSelectedIndexChanged="selectDDL_SelectedIndexChanged" AutoPostBack="true">
                                       <asp:ListItem></asp:ListItem>
                                       <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                       <asp:ListItem Text="None" Value="None"></asp:ListItem>
                                       <asp:ListItem Text="Read" Value="Read"></asp:ListItem>
                                       <asp:ListItem Text="Unread" Value="Unread"></asp:ListItem>
                                   </asp:DropDownList>
                                   
                               </HeaderTemplate>
                               <ItemTemplate>
                                   <asp:CheckBox ID="chkRow" runat="server" OnCheckedChanged="chkRow_CheckedChanged" AutoPostBack="true" />
                               </ItemTemplate>
                               
                           </asp:TemplateField>
                            <asp:BoundField DataField="msgID" Visible="false" />
                            <asp:BoundField DataField="hash" Visible="false" />
                            <asp:BoundField DataField="sender"/>
                            <asp:BoundField DataField="subjectAndMsg" HeaderText="Messages" HeaderStyle-ForeColor="Black"/>
                            <asp:BoundField DataField="date" />
                            <asp:BoundField DataField="isRead" Visible="false" />
                            <asp:BoundField DataField="isDeleted" Visible="false" />
                           <asp:TemplateField>
                               
                               <ItemTemplate>
                                   <asp:LinkButton runat="server" ID="viewBtn" CommandName="onView" CommandArgument='<%#Eval("hash") %>' CssClass="btn btn-primary">View Message</asp:LinkButton>
                               </ItemTemplate>
                           </asp:TemplateField>
                           
                        </Columns>
                    <HeaderStyle BackColor="#ffffff"/>
                    <%--<RowStyle BackColor="Gainsboro" />--%>
                    <%--<AlternatingRowStyle BackColor="Gainsboro" />--%>
                    </asp:GridView>

                    </ContentTemplate>
                        <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="sendButton" EventName="Click" />
                            
                           <%-- <asp:AsyncPostBackTrigger ControlID="selectDDL" EventName="SelectedIndexChanged" />--%>
                        </Triggers>
                </asp:UpdatePanel>



            </div>
        </div>
        </div>
    </div>
</asp:Content>
