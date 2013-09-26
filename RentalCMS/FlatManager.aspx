<%@ Page Title="" Language="C#" MasterPageFile="~/Rental.Master" AutoEventWireup="true" CodeBehind="FlatManager.aspx.cs" Inherits="RentalCMS.FlatManager" %>

<%@ Register Assembly="PageControls" Namespace="SmartControls" TagPrefix="Smart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainAreaOfPage" runat="server">


    <article class="details">

        <div class="row">
            <label runat="server" text="Адресс" >Адрес</label>
            
            <asp:TextBox ID="tblAddress" TextMode="multiline" Rows="3" runat="server"></asp:TextBox>
        </div>

        <div class="row">
            <label runat="server" text="Комнат">Комнат</label>
            <asp:Label runat="server" ID="lblRoomCount" name="tbID"></asp:Label>
        </div>

        <div class="row">
            <label runat="server" text="Телефон">Телефон</label>
            <asp:TextBox runat="server" ID="lblPhone" name="tbID"></asp:TextBox>
        </div>

    </article>

    <div class="imgHolder">
        <a href="#" class="toggle-images-movie doNotPrevent selected">Images</a>

        <div class="images-movie-container">
            <Smart:SmartImages ID="_fyImage" runat="server" ImgHeight="300" ImgWidth="300" />
        </div>

    </div>


    <div class="row">
        <div class="panel">
            <asp:LinkButton ID="btnClose" runat="server" CssClass="button" OnClick="CancelClick" CausesValidation="false">
                    Закрыть
            </asp:LinkButton>
        </div>
    </div>

</asp:Content>
