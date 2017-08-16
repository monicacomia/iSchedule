<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DefaultMaster.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="Thesis.UserProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


<script>

    function showSuccessMsg() {

        $.growl.notice({ title: "Success!", message: "Password successfully changed" });
    }


    function showFailMsg() {

        $.growl.error({message: "Password invalid" });
    }

    function invalid() {

        $.growl.error({ message: "Only .jpg or .png file is allowed" });
    }

    function length() {

        $.growl.error({ message: "Password length should be from 6-20 characters only" });
    }

    
</script>
    
    <style>
        .panel {
            border:0px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
      
    
    <div id="page-inner">

        <div id="Div1"> 
           <div class="row">
            <div class="col-md-12">
                <h2 style="color:deepskyblue; padding-left:15px; font-family:Megafont;">User Profile</h2>    
            </div>
        </div>
        <hr/>   
        

        <div class="row">
            <div class="col-md-6">

                <div class="panel panel-default">
                    <div class="panel-heading">Change Profile Picture</div>
                    <div class="panel-body">

                         <asp:Image ID="img" runat="server"  CssClass="user-image img-responsive"/>
                         <br />
                         <br />
                         <asp:FileUpload runat="server" ID="imgUpload" CssClass="btn btn-default btn-sm" />
                         <br />
                         <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" Text="Save Image" CssClass="btn btn-primary btn-sm"  />

                    </div>

                </div>


                

            </div>

            <div class="col-md-6">

                <div class="panel panel-primary">
                    <div class="panel-heading">Change Password</div>
                    <div class="panel-body">

                        <p class="text-center">Use the form below to change your password.</p>
                        
                        <%--<input type="password" class="input-lg form-control" name="password1" id="oldpassword" placeholder="Old Password" autocomplete="off" />--%>
                        <asp:TextBox runat="server" ID="oldpassword" placeholder="Old Password" TextMode="Password" CssClass="input-lg form-control"></asp:TextBox>
                        <br />
                        <%--<input type="password" class="input-lg form-control" name="password1" id="password1" placeholder="New Password" autocomplete="off" />--%>
                        <asp:TextBox runat="server" ID="password1" placeholder="New Password" TextMode="Password" CssClass="input-lg form-control"></asp:TextBox>
                        
                        <br />
                        <%--<input type="password" class="input-lg form-control" name="password2" id="password2" placeholder="Repeat Password" autocomplete="off"/>--%>
                         <asp:TextBox runat="server" ID="password2"  placeholder="Repeat Password" TextMode="Password" CssClass="input-lg form-control"></asp:TextBox>
                        <br />
                        <div class="row">
                        <div class="col-sm-12">
                        <span id="pwmatch" class="glyphicon glyphicon-ok" style="color:#00A41E;"></span> Passwords Match
                        </div>
                        </div>
                        <br />

                        <asp:Button runat="server" CssClass="col-xs-12 btn btn-primary btn-load btn-lg" ID="btnPassword"  Text="Change Password" OnClick="btnPassword_Click" />
                        <%--<input type="submit" class= data-loading-text="Changing Password..." value="Change Password"/>--%>
                        

                    </div>

                </div>


                

            </div>




        </div>
        
       


        </div>
    </div>  
    <script>


        //$(document).ready(function () {
        //    var x = document.getElementById("password1");
        //    x.addEventListener()
        //});
        var btn = document.getElementById('<%=btnPassword.ClientID%>');
        var pass1 = document.getElementById('<%=password1.ClientID%>');
        var pass2 = document.getElementById('<%=password2.ClientID%>');

        $(pass1).keyup(function () {

            if (pass1.value == pass2.value) {
                console.log("trial");
                $("#pwmatch").removeClass("glyphicon-remove");
                $("#pwmatch").addClass("glyphicon-ok");
                $("#pwmatch").css("color", "#00A41E");
                btn.disabled = false;

            } else {
                console.log("trial2");
                $("#pwmatch").removeClass("glyphicon-ok");
                $("#pwmatch").addClass("glyphicon-remove");
                $("#pwmatch").css("color", "#FF0004");
                btn.disabled = true;
            }

        })


        $(pass2).keyup(function () {


            if (pass1.value == pass2.value) {
                $("#pwmatch").removeClass("glyphicon-remove");
                $("#pwmatch").addClass("glyphicon-ok");
                $("#pwmatch").css("color", "#00A41E");
                btn.disabled = false;

            } else {
                $("#pwmatch").removeClass("glyphicon-ok");
                $("#pwmatch").addClass("glyphicon-remove");
                $("#pwmatch").css("color", "#FF0004");
                btn.disabled = true;
            }



        })


    </script>
          

                
       
</asp:Content>
