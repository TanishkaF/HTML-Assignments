<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CountryTest.aspx.cs" Inherits="DemoUserManagement.web.CountryTest" %>

<!DOCTYPE html>
<html>
<head>
    <title>Country Dropdown</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
</head>
<body>
    <select id="countryDropdown">
        <option value="">Select Country</option>
    </select>

    <script>
        $(document).ready(function () {
            $.ajax({
                url: '/api/countries',
                type: 'GET',
                success: function (data) {
                    var dropdown = $('#countryDropdown');
                    $.each(data, function (index, item) {
                        dropdown.append($('<option></option>').val(item.CountryID).text(item.CountryName));
                    });
                },
                error: function (xhr, textStatus, errorThrown) {
                    console.error('Failed to fetch country list:', textStatus);
                    console.error('Error details:', errorThrown);
                }
            });
        });
    </script>
</body>
</html>
