<%@ Page Title="" Language="C#" MasterPageFile="~/Rental.Master" AutoEventWireup="true" CodeBehind="RealPricing.aspx.cs" Inherits="RentalCMS.RealPricing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainAreaOfPage" runat="server">



    <h1 class="h1" style="text-align: center;"><span style="font-size: medium; color: #000000;"><strong>Цены по аренде жилья в Воронеже 2013 г.</strong></span></h1>
    <p style="text-align: right;"><span style="font-size: medium; color: #000000;">&nbsp;</span><br />
        <span style="font-size: medium; color: #000000;">Вы хотите дешево&nbsp;или хорошо?<br />
            &nbsp;</span></p>
    <h3 style="text-align: justify;"><span style="font-size: medium; color: #000000;"><strong>Стоимость&nbsp;зависит от&nbsp;следующих параметров.</strong></span></h3>
    <p style="text-align: justify;">
        <br />
        <span style="font-size: medium; color: #000000;">От расстояния до центра города Воронежа;  </span>
        <br />
        <span style="font-size: medium; color: #000000;">&nbsp;</span><br />
        <span style="font-size: medium; color: #000000;">&mdash;от района: Центральный и Коминтерновский  обычно дороже, чем левый берег или советский и ленинский район города ; от района: Центральный и Коминтерновский  обычно дороже, чем левый берег или советский и ленинский район города ;  </span>
        <br />
        <span style="font-size: medium; color: #000000;">&nbsp;</span><br />
        <span style="font-size: medium; color: #000000;">&mdash; от наличия мебели, бытовой техники, их состояния и качества;  </span>
        <br />
        <span style="font-size: medium; color: #000000;">&nbsp;</span><br />
        <span style="font-size: medium; color: #000000;">&mdash; от&nbsp;наличия мебели, бытовой техники, их&nbsp;состояния и&nbsp;качества; </span>
        <br />
        <span style="font-size: medium; color: #000000;">&nbsp;</span><br />
        <span style="font-size: medium; color: #000000;">&mdash; от&nbsp;внешнего вида: без&nbsp;ремонта, косметический ремонт, капитальный ремонт. Квартиры с&nbsp;евроремонтом и&nbsp;авторским дизайном на&nbsp;порядок дороже обычных квартир. Цена аренды однокомнатной квартиры студийного c евроремонтом варьируется от&nbsp;50000 до&nbsp;120000&nbsp;рублей в&nbsp;месяц в&nbsp;среднем; </span>
        <br />
        <span style="font-size: medium; color: #000000;">&nbsp;</span><br />
        <span style="font-size: medium; color: #000000;">&mdash; от&nbsp;количества человек, которые будут проживать; </span>
        <br />
        <span style="font-size: medium; color: #000000;">&nbsp;</span><br />
        <span style="font-size: medium; color: #000000;">&mdash; от&nbsp;состава проживающих: национальность, дети, животные; </span>
        <br />
        <span style="font-size: medium; color: #000000;">&nbsp;</span><br />
        <span style="font-size: medium; color: #000000;">&mdash; от&nbsp;срока проживания: квартиры, выставленные на&nbsp;продажу, сдающиеся на&nbsp;2-6 месяцев, как&nbsp;правило, дешевле; </span>
        <br />
        <span style="font-size: medium; color: #000000;">&nbsp;</span><br />
        <span style="font-size: medium; color: #000000;">&mdash; от&nbsp;позиции хозяина. В&nbsp;некоторых случаях уместен торг.</span></p>
    <h3 style="text-align: left;">
        <br />
        <span style="font-size: medium; color: #000000;"><strong>Цены по&nbsp;аренде квартир.</strong></span></h3>
    <p style="text-align: justify;">
        <br />
        <span style="font-size: medium; color: #000000;">Минимальная стоимость однокомнатной квартиры&nbsp;составляет около 28000 рублей в&nbsp;месяц в&nbsp;пределах МКАД. В&nbsp;районах Солнцево, Ново-Переделкино и&nbsp;других спальных районах такие&nbsp;же квартиры по&nbsp;цене 28000-30000 рублей в&nbsp;месяц, но&nbsp;с&nbsp;ремонтом, хорошей мебелью и&nbsp;набором бытовой техники.</span></p>
    <p style="text-align: justify;"><span style="font-size: medium; color: #000000;">Средняя стоимость 30000&nbsp;&mdash; 35000 рублей.</span></p>
    <p style="text-align: justify;"><span style="font-size: medium; color: #000000;">Средняя стоимость найма двухкомнатной квартиры 38000&nbsp;&mdash; 48000 рублей в&nbsp;месяц. В&nbsp;спальных районах за&nbsp;МКАД могут встречаться более дешевоые варианты.&nbsp;</span><br />
        <span style="font-size: medium; color: #000000;">&nbsp;</span><br />
        <span style="font-size: medium; color: #000000;">Трехкомнатные и&nbsp;многокомнатные кв-ры в&nbsp;среднем 55000-70000 рублей.<br />
            &nbsp;</span></p>
    <h3 style="text-align: left;"><span style="font-size: medium; color: #000000;"><strong>Цены по&nbsp;аренде комнат.</strong></span></h3>
    <p style="text-align: justify;">
        <br />
        <span style="font-size: medium; color: #000000;">Подселение&nbsp;составляет 8000&nbsp;&mdash; 9000 рублей за&nbsp;одного человека.</span><br />
        <span style="font-size: medium; color: #000000;">&nbsp;</span><br />
        <span style="font-size: medium; color: #000000;">Проживание с&nbsp;собственниками для&nbsp;1 человека составляет от&nbsp;14000&nbsp;&mdash; 16000 р., для&nbsp;двоих&nbsp;16000-18000 р.</span><br />
        <span style="font-size: medium; color: #000000;">&nbsp;</span><br />
        <span style="font-size: medium; color: #000000;">Комната в&nbsp;коммунальной кв-ре<span style="margin-right: 0.3em"> </span><span style="margin-left: -0.3em">(</span>без хозяев) 15000-17000 за&nbsp;1 человека, для&nbsp;двоих 17000-20000 р.</span></p>
    <p style="text-align: justify;"><span style="color: #000000;"><span style="font-size: medium;"></span><span style="font-size: medium;">Очень хорошая комната для&nbsp;одного человека в&nbsp;центре может достигать 20000-22000 и&nbsp;более рублей в&nbsp;месяц.</span></span><br />
        <br />
    </p>
    <p style="text-align: justify;"><span style="color: #000000;"><strong><span style="font-size: medium;">Сезоны.<br />
    </span></strong></span></p>
    <p style="text-align: justify;">
        <br />
        <span style="font-size: medium; color: #000000;">Осень. </span></p>
    <p style="text-align: justify;"><span style="font-size: medium; color: #000000;">Сезонный рост цен&nbsp;на&nbsp;<a href="/" title="Аренда квартир"><span style="color: #000000;"><em>аренду жилья</em></span></a> связан с<span style="margin-right: 0.3em"> </span><span style="margin-left: -0.3em">&laquo;</span>вымыванием&raquo; объектов эконом-класса из-за увеличения количества приезжих и&nbsp;студентов осенью.&nbsp;</span></p>
    <p style="text-align: justify;"><span style="font-size: medium; color: #000000;">Лето. </span></p>
    <p style="text-align: justify;"><span style="font-size: medium; color: #000000;">В летний период жилая недвижимость немного дешевеет потому, что&nbsp;люди в&nbsp;отпусках и&nbsp;на&nbsp;каникулах. Бывает так, что&nbsp;хозяева оговаривают сразу, что&nbsp;летние месяцы стоимость проживания одна, а&nbsp;с&nbsp;осени будет выше.<br />
        &nbsp;</span></p>
    <p style="text-align: justify;"><span style="color: #000000;"><strong><span style="font-size: medium;">Низкие цены.<br />
        &nbsp;</span></strong></span></p>
    <p style="text-align: justify;"><span style="font-size: medium; color: #000000;">Если Вы&nbsp;видите существенно заниженную стоимость объекта, имейте в&nbsp;виду&nbsp;&mdash;
        <nobr></nobr>
        <nobr></nobr>
        <nobr></nobr>
        <nobr></nobr>
        <nobr></nobr>
        <nobr></nobr>
        <nobr></nobr>
        <nobr></nobr>
        <nobr></nobr>
        <nobr><nobr>что-то</nobr>&nbsp;</nobr>
        &nbsp;&nbsp;с ним&nbsp;не&nbsp;так. И&nbsp;помните, что<span style="margin-right: 0.3em"> </span><span style="margin-left: -0.3em">&laquo;</span>Скупой платит дважды&raquo;.</span><br />
        <span style="font-size: medium; color: #000000;">&nbsp;</span></p>


</asp:Content>
