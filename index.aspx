<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Live QR Code Progress</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
</head>
<body>
    <form runat="server" id="form1">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        
        <h1>QR Code Generation</h1>
        
        <!-- Start Button -->
        <asp:Button runat="server" ID="btn_submit" CssClass="btn btn-primary" Text="Start" OnClick="btn_submit_Click" />

        <!-- Progress Display -->
        <h2>Progress: <span id="progress">0</span> / 1000000</h2>



        <!-- JavaScript for Live Updates -->
        <script type="text/javascript">
		
            function fetchLiveProgress() {
                $.ajax({
                    url: "index.aspx/GetProgress",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        // Update the progress UI
                        const progress = response.d;
                        document.getElementById("progress").innerText = progress;
                    },
                    error: function (xhr, status, error) {
                        console.error("Error fetching progress:", error);
                    }
                });
            }

            // Poll progress every second
            setInterval(fetchLiveProgress, 1000);
        </script>
    </form>
</body>
</html>
