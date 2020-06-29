<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPanel.aspx.cs" Inherits="LoginPanel_UI.LoginPanel" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Kullanıcı Girişi</title>
<meta charset="utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous">
<script src="https://code.jquery.com/jquery-3.4.1.slim.min.js" integrity="sha384-J6qa4849blE2+poT4WnyKhv5vZF5SrPo0iEjwBvKU7imGFAV0wwj1yYfoRSJoZ+n" crossorigin="anonymous"></script>
<script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js" integrity="sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo" crossorigin="anonymous"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js" integrity="sha384-wfSDF2E50Y2D1uUdj0O3uMBJnjuUD4Ih7YwaYd1iqfktj0Uod8GCExl3Og8ifwB6" crossorigin="anonymous"></script>
</head>
<body>
<div class="container">
<div class="row" style="margin-top:50px;">
<div class="col-md-3 col-sm-12 col-xs-12">
</div>
<div class="col-md-6 col-sm-12 col-xs-12">
   <form id="form1" runat="server">
  <div class="form-group">
    <label for="username">Kullanıcı Girişi</label>
    <asp:TextBox ID="txtUsername" placeholder="Kullanıcı Adı" runat="server" CssClass="form-control" required="required" MaxLength="500"></asp:TextBox>
  </div>
  <div class="form-group">
    <label for="password">Şifre</label>
    <asp:TextBox ID="txtPassword" Text="Şifre" TextMode="Password" placeholder="Şifre" runat="server" CssClass="form-control" required="required" MaxLength="10"></asp:TextBox>
  </div>
  <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Giriş Yap" style="float:right" OnClick="btnSubmit_Click"/>
</form>
</div>
<div class="col-md-3 col-sm-12 col-xs-12">
</div>
</div>
<%
    if (!string.IsNullOrEmpty(lblResult.Text))
    {
%>
<div class="row" style="margin-top:10px;">
<div class="col-md-3 col-sm-12 col-xs-12">
</div>
<div class="col-md-6 col-sm-12 col-xs-12">
<div class="alert alert-success" id="acilacak_div" name="acilacak_div" style="display:block; border:solid 1px #ccc; background-color:#f5c5a5">
    <button type="button" class="close" data-dismiss="alert"></button>
    <p><asp:Label ID="lblResult" runat="server"></asp:Label></p><!--ekrana uyarı mesajı-->
</div>
</div>
<div class="col-md-3 col-sm-12 col-xs-12">
</div>
</div>
<%
}
%>

</div>
</body>
<script>
    //Ekrana basılan uyarı mesajını 3 sn sonra kapatır.
    setInterval(function () { close() }, 3000);
    function close()
    {
      document.getElementById('acilacak_div').style.display = 'none';
    }
</script> 
</html>
