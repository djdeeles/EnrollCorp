<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BelediyeHizmetleriListKontrol.ascx.cs"
            Inherits="EnrollKurumsal.UserKontroller.BelediyeHizmetleriListKontrol" %>
<script type="text/javascript">
    $(document).ready(function() {
        if ($("#aznav")) {
            $("#aznav dd").hide();
            $("#aznav dt b").click(function() {
                if (this.className.indexOf("clicked") != -1) {
                    $(this).parent().next().slideUp(200);
                    $(this).removeClass("clicked");
                } else {
                    $("#aznav dt b").removeClass();
                    $(this).addClass("clicked");
                    $("#aznav dd:visible").slideUp(200);
                    $(this).parent().next().slideDown(500);
                }
                return false;
            });
        }
    });
    $(document).ready(function() {
        if ($("#aziletisim")) {
            $("#aziletisim dd").hide();
            $("#aziletisim dt b").click(function() {
                if (this.className.indexOf("clicked") != -1) {
                    $(this).parent().next().slideUp(200);
                    $(this).removeClass("clicked");
                } else {
                    $("#aziletisim dt b").removeClass();
                    $(this).addClass("clicked");
                    $("#aziletisim dd:visible").slideUp(200);
                    $(this).parent().next().slideDown(500);
                }
                return false;
            });
        }
    });
</script>
<script type="text/javascript">
    function update(returnField, returnValue) {
        document.getElementById(returnField).value = returnValue;
    }
</script>
<div class="contentmainbanner" id="Resim" runat="server">
</div>
<div class="contentmainbannertext">
    <div class="contentmainbannerbaslik">
        A'dan Z'ye Belediye Hizmetleri
    </div>
    <div class="contentmainbannerharita">
    </div>
</div>
<div class="contentmaintop">
</div>
<div class="contentmainmed">
    <dl id="aznav">
        <asp:ListView ID="ListView1" runat="server" ItemPlaceholderID="PlaceHolder1">
            <ItemTemplate>
                <dt><b>
                        <%#Eval
                                                                                                   ("Soru") %>
                    </b></dt>
                <dd>
                    <%#Eval("Cevap") %>
                </dd>
            </ItemTemplate>
            <LayoutTemplate>
                <table style="border: 0px;">
                    <tr>
                        <td style="margin-bottom: 15px;">
                            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <EmptyDataTemplate>
                Veri bulunamadı.
            </EmptyDataTemplate>
        </asp:ListView>
    </dl>
    <% if (DataPager1.TotalRowCount > 29)
       {%>
        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="ListView1" PageSize="30"
                       QueryStringField="belediyehizmetleripage">
            <Fields>
                <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="True" ShowNextPageButton="False"
                                            ShowPreviousPageButton="False" FirstPageText="İlk" LastPageText="Son" NextPageText="Sonraki"
                                            PreviousPageText="Önceki" />
                <asp:NumericPagerField />
                <asp:NextPreviousPagerField ButtonType="Link" ShowLastPageButton="True" ShowNextPageButton="False"
                                            ShowPreviousPageButton="False" FirstPageText="İlk" LastPageText="Son" NextPageText="Sonraki"
                                            PreviousPageText="Önceki" />
            </Fields>
        </asp:DataPager>
    <% } %>
    <dl id="aziletisim">
        <dt><b>Bu sonuçlar ihtiyacınızı karşılamadı mı? </b></dt>
        <dd>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td style="width: 90px; vertical-align: top;">
                                <b>Siz Sorun,<br />
                                    Biz Cevaplayalım.</b>
                            </td>
                            <td style="width: 10px; vertical-align: top;">
                                :
                            </td>
                            <td style="width: 475px; vertical-align: top;">
                                <asp:TextBox ID="TextBoxSoru" runat="server" Rows="5" TextMode="MultiLine" Width="475px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Sorunuzu giriniz."
                                                            ControlToValidate="TextBoxSoru" Display="Dynamic" ForeColor="Red" ValidationGroup="aziletisim"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Ad </b>
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxAd" runat="server" Width="200"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxAd"
                                                            Display="Dynamic" ErrorMessage="Adınızı giriniz." ForeColor="Red" ValidationGroup="aziletisim"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Soyad </b>
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxSoyad" runat="server" Width="200"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBoxSoyad"
                                                            Display="Dynamic" ErrorMessage="Soyadınızı giriniz." ForeColor="Red" ValidationGroup="aziletisim"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>E-posta </b>
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxEPosta" runat="server" Width="200"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBoxEPosta"
                                                            Display="Dynamic" ErrorMessage="E-posta adresinizi giriniz." ForeColor="Red"
                                                            ValidationGroup="aziletisim"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
                                                                ErrorMessage="Geçerli bir e-posta adresi giriniz." ControlToValidate="TextBoxEPosta"
                                                                ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                ValidationGroup="aziletisim"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Bilgilendirme </b>
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:RadioButton ID="RadioButtonEvet" runat="server" GroupName="a" Text="İstiyorum."
                                                 Checked="True" />
                                <asp:RadioButton ID="RadioButtonHayir" runat="server" GroupName="a" Text="İstemiyorum" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="ButtonGonder" runat="server" Text="Gönder" ValidationGroup="aziletisim"
                                            OnClick="ButtonGonder_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="LabelMesaj" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </dd>
    </dl>
</div>
<div class="contentmainbottom">
</div>