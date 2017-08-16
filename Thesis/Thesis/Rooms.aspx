<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DefaultMaster.Master" AutoEventWireup="true" CodeBehind="Rooms.aspx.cs" Inherits="Thesis.Rooms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>

    function invalidRoom() {

        $.growl.error({ title: "Invalid", message: "The room already existed" });

    }

    function inputRoom() {

        $.growl.error({ title: "Invalid", message: "Please input room" });
    }

    function inputGrp() {

        $.growl.error({ title: "Invalid", message: "Please input group" });
    }

    function addedRoom(){
    
         $.growl.notice({ title: "Success!", message: "Room successfully added" });
    }

    function editedRoom() {
        $.growl.warning({ message: "Room has been edited successfully" });
    }
    
    function deletedRoom() {
        $.growl.error({ message: "Room has been deleted successfully" });
    }

    function invalidGrp(){
    
        $.growl.error({ title: "Invalid", message: "The group already existed" });
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

        <div id="page-inner">

                    <div class="row">
           <div class="col-md-12">
                <h2 style="color:deepskyblue; padding-left:15px; font-family:Megafont;">Rooms</h2>  
            </div>
            </div>
    <hr/>   
        <%-- Edit Room Modal --%>
        <div aria-hidden="true" aria-labelledby="ModalHeaderLabel" role="dialog" data-backdrop="static"
        data-keyboard="false" tabindex="-1" class="modal modal-xlarge" id="editRoomModal"
        style="display: none;">

        <asp:UpdatePanel runat="server" id="mdlEditRoomPanel">
            <ContentTemplate>
               <div class="modal-content" style="margin-top:100px;">

                   <div class="modal-body">
                    
                         <div class="form-group">
                                            <asp:Label Visible="false"  runat="server" ID="roomlbl"></asp:Label>
                                            <label for="RoomTxt">Edit Room</label>
                                            <asp:TextBox runat="server" ID="editRoomTxt" CssClass="form-control"></asp:TextBox>
                                       </div>
                         <div class="form-group">
                                           <label for="groupDDL">Choose Group</label>
                                             <asp:DropDownList ID="editGroupDDL" runat="server" CssClass="form-control">
                                           </asp:DropDownList>
                          </div> 
                    

                    </div>    
                     <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <asp:LinkButton runat="server" ID="saveRoomButton" OnClick="saveRoomButton_Click" Text="Save" CssClass="btn btn-primary"></asp:LinkButton>
                        
                    </div>

               </div>
            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="addUserModalButton" EventName="Click" />--%>
                
            </Triggers>
        </asp:UpdatePanel>
        </div>
        <%-- End of Edit Room Modal --%>
        

        <%-- Edit Group Modal --%>
        <div aria-hidden="true" aria-labelledby="ModalHeaderLabel" role="dialog" data-backdrop="static"
        data-keyboard="false" tabindex="-1" class="modal modal-xlarge" id="editGrpModal"
        style="display: none;">

        <asp:UpdatePanel runat="server" id="editGrpPanel">
            <ContentTemplate>
               <div class="modal-content" style="margin-top:100px;">

                   <div class="modal-body">
                    
                         <div class="form-group">
                                            <asp:Label runat="server" ID="grplbl" Visible="false"></asp:Label>
                                            <label for="RoomTxt">Edit Group</label>
                                            <asp:TextBox runat="server" ID="editGrpTxt" CssClass="form-control"></asp:TextBox>
                                       </div>
                    </div>    
                     <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <asp:LinkButton runat="server" ID="editGrpBtn" OnClick="editGrpBtn_Click" Text="Save" CssClass="btn btn-primary"></asp:LinkButton>
                        
                    </div>

               </div>
            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="addUserModalButton" EventName="Click" />--%>
                
            </Triggers>
        </asp:UpdatePanel>
        </div>
        <%-- End of Edit Group Modal --%>





        <ul class="nav nav-tabs">
                                <li class="active"><a href="#room" data-toggle="tab">Room</a>
                                </li>
                                <li class=""><a href="#group" data-toggle="tab">Group</a>
                                </li>
                             
         </ul>

        <div class="tab-content">

             <div class="tab-pane fade active in" id="room">

         <div class="row" style="margin-top:10px;">   
        <asp:UpdatePanel runat="server" ID="formPanel" UpdateMode="Conditional">

                <ContentTemplate>
        
                    <div class="col-md-4">

            

            
                            <div class="panel panel-default">
                                  <div class="panel-heading">Add Room</div>
                                  <div class="panel-body">
                                       <div class="form-group">
                                            <label for="RoomTxt">Input Room</label>
                                            <asp:TextBox runat="server" ID="RoomTxt" CssClass="form-control"></asp:TextBox>
                                       </div>
                                       <div class="form-group">
                                           <label for="groupDDL">Choose Group</label>
                                             <asp:DropDownList ID="groupDDL" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                       </div> 
                      
                                       <div class="row">
                                           <div class="col-md-6"></div>
                                           <div class="col-md-6">
                                               <asp:Button runat="server" CssClass="btn btn-primary btn-sm pull-right" ID="addBtn" OnClick="addBtn_Click" Text="Add Room" />
                                           </div>
                                       </div>  

                                    
                      
                                  </div>
                            </div>


                        
                       </div>

          </ContentTemplate>
            <Triggers>
                 
            </Triggers>
       </asp:UpdatePanel> 

            <div class="col-md-8">
                 <div class="panel panel-default">
                    <div class="panel-heading">List of Rooms</div>
                        <div class="panel-body">
                   <%-- table for holidays --%>
                            <div class="table-responsive">
                   <asp:UpdatePanel runat="server" ID="roomPanel" UpdateMode="Conditional">
                       <ContentTemplate>
                           <asp:GridView  runat="server" ID="roomGrid" AllowPaging="true" AutoGenerateColumns="false" PageSize="15" DataKeyNames="roomID" CssClass="table table-bordered table-hover" OnPageIndexChanging="roomGrid_PageIndexChanging" OnRowCommand="roomGrid_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="roomID" Visible="false"/>
                                        <asp:BoundField DataField="room" HeaderText="Room"/>
                                        <asp:BoundField DataField="grp" HeaderText="Group"/>

                                         <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="updateRoomBtn" runat="server" CommandName="onUpdate" CommandArgument='<%#Eval("roomID") %>' CssClass="btn btn-primary">Update</asp:LinkButton>
                                                    <asp:LinkButton ID="deleteRoomBtn" runat="server" CommandName="onDelete" CommandArgument='<%#Eval("roomID") %>' CssClass="btn btn-danger" OnClientClick="return confirm('Are you sure you want to delete this room?');">Delete</asp:LinkButton>
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
                </div><%-- end of room--%>

             <div class="tab-pane fade" id="group">
                   <div class="row" style="margin-top:10px;">   
                            <%--<asp:UpdatePanel runat="server" ID="grpFormPanel" UpdateMode="Conditional">
                                    <ContentTemplate>--%>
                                        <div class="col-md-4">
                                                <div class="panel panel-default">
                                                      <div class="panel-heading">Add Group</div>
                                                      <div class="panel-body">
                                                           <div class="form-group">
                                                                <label for="grpTxt">Input Group</label>
                                                                <asp:TextBox runat="server" ID="grpTxt" CssClass="form-control"></asp:TextBox>
                                                           </div>
                      
                                                           <div class="row">
                                                               <div class="col-md-6"></div>
                                                               <div class="col-md-6">

                                                                   <%--<asp:Button runat="server" CssClass="btn btn-primary btn-sm pull-right" ID="addGrpBtn" OnClick="addGrpBtn_Click" Text="Add Group"  />--%>
                                                                   <asp:LinkButton runat="server" CssClass="btn btn-primary btn-sm pull-right" ID="btnGrp" OnClick="btnGrp_Click" Text="Add Group"></asp:LinkButton>
                                                               </div>
                                                           </div>  

                                    
                      
                                                      </div>
                                                </div>


                        
                                           </div>

<%--                              </ContentTemplate>
                                <Triggers>
                                    
                                </Triggers>
                           </asp:UpdatePanel> --%>

                                <div class="col-md-8">
                                     <div class="panel panel-default">
                                        <div class="panel-heading">List of Group</div>
                                            <div class="panel-body">
                                       <%-- table for holidays --%>
                                                <div class="table-responsive">
                                       <asp:UpdatePanel runat="server" ID="grpPanel" UpdateMode="Conditional">
                                           <ContentTemplate>
                                               <asp:GridView  runat="server" ID="grpGrid" AllowPaging="true" AutoGenerateColumns="false" PageSize="15" DataKeyNames="grpID" CssClass="table table-bordered table-hover" OnRowCommand="grpGrid_RowCommand">
                                                        <Columns>
                                                            
                                                            <asp:BoundField DataField="grpID" HeaderText="ID" Visible="false"/>
                                                            <asp:BoundField DataField="grpName" HeaderText="Group Name"/>

                                                             <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="updateGrpBtn" runat="server" CommandName="onUpdate" CommandArgument='<%#Eval("grpID") %>' CssClass="btn btn-primary">Update</asp:LinkButton>
                                                                        <%--<asp:LinkButton ID="deleteGrpBtn" runat="server" CommandName="onDelete" CommandArgument='<%#Eval("grpID") %>' CssClass="btn btn-primary">Delete</asp:LinkButton>--%>
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
                             </div>               
              </div>

          </div>  <%--end of tab content--%>
    </div> <%--end of page inner--%>



</asp:Content>
