<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="Thesis.LoginPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Welcome I-Schedule</title>

    	<meta charset="utf-8"/>
		<meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
		<link href="Assets/css/main.css" rel="stylesheet" type="text/css"/>
        <noscript>
            <link href="Assets/css/noscript.css" rel="stylesheet" type="text/css"/>
        </noscript>
        
</head>
<body>
		<!-- Wrapper -->
			<div id="wrapper">
				<!-- Header -->
					<header id="header"><br/>
						<div class="content">
							<div class="inner">
								<h1><img src="Assets/img/FinalLogo.png"/></h1>
							</div>
						</div>
						<nav>
							<ul>
                                <li><a id="signInLink" href="/SignInPage.aspx">Sign-In</a></li>
								<%--<li><a id="registrationLink" href="LoginPage.aspx#register">Register</a></li>--%>
                                <li><a id="registrationLink" href="/RegisterPage.aspx">Register</a></li>
                                <%--<asp:LinkButton ID="btnRegistrationForm" runat=""></asp:LinkButton>--%>
							</ul>
						</nav>
					</header>

				<!-- Main -->
					<div id="main">
						<!-- Intro -->
							<article id="signIn">
                                 <form id="signInForm" runat="server" visible="true">
									<h2 class="major">Sign-In Account</h2>
                                    <asp:Panel runat="server" ID="Panel8">
                                        <asp:Label ID="invalidLoginLabel" runat="server" Visible="false"></asp:Label>
                                    </asp:Panel>
									<div class="field half first">
										<label for="username">Faculty ID</label>
										<%--<input type="text" name="username" id="username" />--%>
                                        <asp:TextBox ID="facultyIDTxtbox" name="facultyIdSignIn" ClientIDMode="Static" runat="server" placeholder="Faculty ID" required="required"/>
									</div>
									<div class="field half">
										<label for="password">Password</label>
										<%--<input type="password" name="password" id="password" />--%>
                                        <asp:TextBox ID="password" name="password"  runat="server" placeholder="Password" required="required" TextMode="Password"/>
									</div>

									<br/><br/><br/>
									
									<ul class="actions">
										<%--<li><input type="submit" value="Log-in" class="special" onclick="" runat="server"/></li>--%>
                                        <li><asp:LinkButton ID="logInButton" class="special" runat="server" Text="Log-in" OnClick="logInButton_Click"/></li>
										<%--<li><input type="reset" value="Forgot Password?" runat="server"/></li>--%>
                                        <li><asp:LinkButton ID="forgotPasswordButton" runat="server" Text="Forgot Password?" OnClick="forgotPasswordButton_Click"/></li>
									</ul>
								 </form>
							</article>
                            <script>
                                document.getElementById('<%=password.ClientID%>').addEventListener("keyup", function (event) {
                                    event.preventDefault();
                                    if (event.keyCode == 13) {
                                        console.log("enter pressed");
                                        document.getElementById('<%=logInButton.ClientID%>').click();
                                    }
                                });
                            </script>
						<!-- Contact -->
							<%--<article id="register">
                                <form id="regForm" runat="server" visible="false">
								<h2 class="major">Register Account</h2>
                                <asp:Panel runat="server" ID="unauthorizedUserPanel">
                                    <asp:Label ID="unauthorizedUserLabel" runat="server" Visible="false"></asp:Label>
                                </asp:Panel>
									<div class="field half first">
										<label for="facultyId">Faculty ID</label>
                                        <asp:TextBox ID="facultyId" name="facultyId" ClientIDMode="Static" runat="server" required="required"/>
                                        <asp:Panel runat="server" ID="Panel3">
                                            <asp:Label ID="facultyIdLabel" runat="server" Visible="false"></asp:Label>
                                        </asp:Panel>
									</div>
									<div class="field half">
										<label for="accountType">Account Type</label>
                                        <asp:DropDownList ID="dropdownAccount" runat="server" CssClass="ddlAccount">
                                            <asp:ListItem Text="Select From Option" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Faculty" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Department Head" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Academics Assistant" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="VP Academics" Value="4"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Panel runat="server" ID="Panel2">
                                            <asp:Label ID="invalidDropdownSelection" runat="server" Visible="false"></asp:Label>
                                        </asp:Panel>
									</div>
									<br/><br/><br/><br/>
									<div class="field">
										<label for="firstName">Firstname</label>
                                        <asp:TextBox ID="firstName" name="firstName" ClientIDMode="Static" runat="server" required="required"/>
                                        <asp:Panel runat="server" ID="Panel1">
                                            <asp:Label ID="requiredFirstName" runat="server" Visible="false"></asp:Label>
                                        </asp:Panel>
									</div>
									<div class="field">
										<label for="lastName">Lastname</label>
                                        <asp:TextBox ID="lastName" name="lastName" ClientIDMode="Static" runat="server" required="required"/>
                                        <asp:Panel runat="server" ID="Panel4">
                                            <asp:Label ID="requiredLastName" runat="server" Visible="false"></asp:Label>
                                        </asp:Panel>
                                        
									</div>
									<div class="field">
										<label for="email">E-mail</label>
                                        <asp:TextBox ID="email" name="email" ClientIDMode="Static" runat="server" required="required"/>
                                        <asp:Panel runat="server" ID="Panel5">
                                            <asp:Label ID="emailLabel" runat="server" Visible="false"></asp:Label>
                                        </asp:Panel>
									</div>
									<div class="field half first">
										<label for="regPass">Password</label>
                                        <asp:TextBox ID="regPass" name="regPassword" ClientIDMode="Static" runat="server" required="required" TextMode="Password"/>
                                        <asp:Panel runat="server" ID="Panel6">
                                            <asp:Label ID="requiredPassword" runat="server" Visible="false"></asp:Label>
                                        </asp:Panel>
									</div>
                                    
									<br/><br/><br/><br/>
									<div class="field half first">
										<label for="confirmPass">Confirm Password</label>
                                        <asp:TextBox ID="confirmPass" name="confirmPass" ClientIDMode="Static" runat="server" required="required" TextMode="Password"/>
                                        <asp:Panel runat="server" ID="Panel7">
                                            <asp:Label ID="confirmPasswordMatch" runat="server" Visible="false"></asp:Label>
                                        </asp:Panel>
									</div>
                                    
									<br/><br/><br/>
									<ul class="actions">	
                                        <%--<li><input type="submit" value="Register" class="special" runat="server"/></li>--%>
                                        <%--<li><asp:LinkButton ID="registerButton" OnClick="registerButton_Click" class="special" runat="server" Text="Register"/></li>--%>
                                        <%--<li><input type="reset" value="Reset" runat="server"/></li>--%>
                                        <%--<li><asp:LinkButton ID="resetRegButton" OnClick="resetRegButton_Click" runat="server" Text="Reset"/></li>--%>
									<%--</ul>--%>
                                    <%--</form>--%>
							<%--</article>--%>
					</div>

				<!-- Footer -->
					<footer id="footer">
						<p class="copyright">Copyright &copy; iACADEMY 2017 <a href="#"></a></p>
					</footer>

			</div>

		<!-- BG -->
			<div id="bg"></div>

        <!-- Scripts -->
		<script src="Assets/js/jquery.min.js" type="text/javascript"></script>
		<script src="Assets/js/skel.min.js" type="text/javascript"></script>
		<script src="Assets/js/util.js" type="text/javascript"></script>
		<script src="Assets/js/main.js" type="text/javascript"></script>   
        

	</body>
</html>
