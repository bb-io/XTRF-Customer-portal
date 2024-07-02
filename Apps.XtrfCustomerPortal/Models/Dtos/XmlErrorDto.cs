using System.Xml.Serialization;

namespace Apps.XtrfCustomerPortal.Models.Dtos;

[XmlRoot("html")]
public class XmlErrorDto
{
    [XmlElement("head")]
    public Head Head { get; set; }

    [XmlElement("body")]
    public string Body { get; set; }
}

public class Head
{
    [XmlElement("title")]
    public string Title { get; set; }
}