public class SoapRequestBinary{
    public string Request_TestGiris(){
        var response = @"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/'
                            xmlns='urn:microsoft-dynamics-schemas/codeunit/Integration'>
                            <soapenv:Header/>
                            <soapenv:Body>
                                <TestGiris/>
                            </soapenv:Body>
                        </soapenv:Envelope>";
        
        return response;
    }

    public string Request_TestGiris02(string _paramName, string _paramText){
        if((_paramName == null || _paramName == "") && (_paramText == null || _paramText == "")){
            return "FALSE";
        }
        var response = $@"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/'
                            xmlns='urn:microsoft-dynamics-schemas/codeunit/Integration'>
                            <soapenv:Header/>
                            <soapenv:Body>
                                <TestGiris02>
                                    <{_paramName}>{_paramText}</{_paramName}>
                                </TestGiris02>
                            </soapenv:Body>
                        </soapenv:Envelope>";
        
        return response;
    }
}