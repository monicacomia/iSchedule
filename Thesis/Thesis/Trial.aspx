<%@ Page Title="" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="Trial.aspx.cs" Inherits="Thesis.Trial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script type="text/javascript">
    $(document).ready(function () {
      
        $('#startTime').bootstrapMaterialDatePicker
        ({
            date: false,
            shortTime: true,
            format: 'HH:mm'
        });


        $('#endTime').bootstrapMaterialDatePicker
       ({
           date: false,
           shortTime: true,
           format: 'HH:mm'
       });
      
        //This is important to initialize 
        $.material.init()
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container well">
			<div class="row">
				<div class="col-md-6">
					<h2>Time Picker</h2>
				</div>
			</div>
			<div class="row">
				<div class="col-md-6">
					<div class="form-control-wrapper">
						<input type="text" id="startTime" class="form-control floating-label" placeholder="Start Time" />
                        <br />
                        <input type="text" id="endTime" class="form-control floating-label" placeholder="End Time"/>

					</div>
                   <%-- <asp:TextBox ID="time" runat="server"></asp:TextBox>--%>
                    
				</div>
				<div class="col-md-6">
					<code>
						<p>Code</p>
						$('#time').bootstrapMaterialDatePicker({ date: false });
					</code>
				</div>
			</div>
		</div>


</asp:Content>
