<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="OfficeRemoteWeb.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="Content/jquery.mobile-1.3.1.css" />

    <!-- jQuery and jQuery Mobile -->
    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script src="Scripts/jquery.mobile-1.3.1.min.js"></script>

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div data-role="page" id="page1">
            <div data-role="content">
                <asp:LinkButton ID="lbFirst" data-role="button" runat="server" Text="First Slide" OnClick="lbFirst_Click" />
                <asp:LinkButton ID="lbPrev" data-role="button" runat="server" Text="Previous Slide" OnClick="lbPrev_Click" />
                <asp:LinkButton ID="lbNext" data-role="button" runat="server" Text="Next Slide" OnClick="lbNext_Click" />
                <asp:LinkButton ID="lbLast" data-role="button" runat="server" Text="Last Slide" OnClick="lbLast_Click" />
                <%--<a data-role="button" href="#page1">First Slide</a>
                <a data-role="button" href="#page1">Previous Slide</a>
                <a data-role="button" href="#page1">Next Slide</a>
                <a data-role="button" href="#page1">Last Slide</a>--%>
            </div>
        </div>
    </form>
</body>
</html>
