namespace project.entity{
    public class NoticeTextClass{
        public static string NoticeTextValue { get; set; } =
        @"
            <div class=""row mb-4"">
                <div class=""col-6"">
                    <h4 class=""font-weight-bold"">Kredi Kartı Komisyon Oranları:</h4>
                    <table class=""table table-striped table-bordered table-hover"">
                        <thead>
                            <tr>
                                <th scope=""col"">16.09.2024 itibariyle</th>
                                <th scope=""col"">CARD FİNANS özellikli Kredi Kartları</th>
                                <th scope=""col"">WORLD özellikli (Yapı Kredi ve Vakıfbank)</th>
                                <th scope=""col"">Maximum özellikli Kredi Kartları</th>
                                <th scope=""col"">AKBANK Kredi Kartları</th>
                                <th scope=""col"">BONUS özellikli Kredi Kartları</th>
                                <th scope=""col"">Ziraat bankası Kredi Kartları</th>
                                <th scope=""col"">Halkbank Kredi Kartları</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <th scope=""row"">Tek Çekim</th>
                                <td>3.53%</td>
                                <td>3.21%</td>
                                <td>2.95%</td>
                                <td>2,90%</td>
                                <td>3.34%</td>
                                <td>3.09%</td>
                                <td>3.54%</td>
                            </tr>
                            <tr>
                                <th scope=""row"">2 Taksit</th>
                                <td>5.71%</td>
                                <td></td>
                                <td></td>
                                <td>5.50%</td>
                                <td>5.71%</td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <th scope=""row"">4 Taksit</th>
                                <td>9.45%</td>
                                <td>9.35%</td>
                                <td>9.35%</td>
                                <td></td>
                                <td>9.45%</td>
                                <td></td>
                                <td>9.35%</td>
                            </tr>
                            <tr>
                                <th scope=""row"">6 Taksit</th>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>12.99%</td>
                                <td>13.08%</td>
                            </tr>
                        </tbody>
                    </table>
                    <h6 class=""text-muted"">KUR: 04.02.2025-Hakan Döviz Saat;15,30 Döviz satış kuru: 37,1673 (05.02.2025 Saat:15,30 a kadar geçerli olacaktır.)</h6>
                </div>
            </div>

            <div class=""row mb-4"">
                <div class=""col-12"">
                    <h4>Notlar:</h4>
                    <ul>
                        <li><span class=""text-brown"">(*)</span> Fatura tarihi ile ödeme vadesi arasındaki taksitli çekimler için yukarıda belirtilen komisyon oranlarının yarısı uygulanmaktadır.</li>
                        <li><span class=""text-brown"">(**)</span> Ödeme vadesi geçmiş ödemeler için yukarıda belirtilen komisyon oranları uygulanmaktadır.</li>
                        <li><span class=""text-brown"">(***</span>) Kampanya şartlarında belirtilen hesap kapatma tarihine kadar yapılan Tek & Taksitli kredi kartı çekimleri komisyonsuzdur.</li>
                        <li><span class=""text-brown"">(****)</span> Kampanya şartlarında belirtilen hesap kapatma tarihinden sonra yapılacak Tek & Taksitli çekimler için yukarıda belirtilen komisyon oranları uygulanacaktır.</li>
                    </ul>
                </div>
            </div>

        ";
    }
}