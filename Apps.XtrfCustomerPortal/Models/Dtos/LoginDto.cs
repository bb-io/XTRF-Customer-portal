using System.Xml.Serialization;

namespace Apps.XtrfCustomerPortal.Models.Dtos;

[XmlRoot("loginResponse")]
public class LoginDto
{
    [XmlElement("jsessionid")]
    public string JSessionId { get; set; }

    public string JsessionCookie { get; set; }
}