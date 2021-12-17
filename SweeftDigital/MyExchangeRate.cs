using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SweeftDigital
{

  static internal class MyExchangeRate
  {
    static public string NameInfoFrom { get; set; }
    static public string NameInfoTo { get; set; }
    static public string QuantityInfo { get; set; }

    static internal double exchangeRate(string from, string to)
    {
      double _from = 1, _to = 1, _quantity = 1;

      SyndicationFeed feed = null;

      try
      {
        using (var reader = XmlReader.Create("http://www.nbg.ge/rss.php"))
        {
          feed = SyndicationFeed.Load(reader);
        }
      }
      catch (Exception ex) { Console.WriteLine(ex.Message); }

      string result = null;
      if (feed != null)
      {
        foreach (var element in feed.Items)
          result += element.Summary.Text;
      }

      result = Regex.Replace(result, "(<img.+?>)", "");
      var serializer = new XmlSerializer(typeof(Table));
      var data = (Table)serializer.Deserialize(new StringReader(result));


      if (from.ToUpper() != "GEL")
      {
        var fromData = data.Rows.Where(x => x.Data[0] == from.ToUpper()).FirstOrDefault();
        if (fromData is null) { throw new Exception("Currency not found!"); }

        NameInfoFrom = from;
        int index = fromData.Data[1].LastIndexOf('0');
        if (index != -1)
          QuantityInfo = fromData.Data[1].Remove(++index);
        else
          QuantityInfo = "1";

        _from = Convert.ToDouble(fromData.Data[2]);
      }

      if (to.ToUpper() != "GEL")
      {
        var toData = data.Rows.Where(x => x.Data[0] == to.ToUpper()).FirstOrDefault();
        if (toData is null) { throw new Exception("Currency not found!"); }

        NameInfoTo = to;
        int index = toData.Data[1].LastIndexOf('0');
        if (index != -1)
          _quantity = Convert.ToDouble(toData.Data[1].Remove(++index));

        _to = Convert.ToDouble(toData.Data[2]) / _quantity;
      }

      return Math.Round(_from / _to, 4);
    }

  }


  [XmlRoot("table")]
  public class Table
  {
    [XmlElement("tr")]
    public TableRow[] Rows { get; set; }
  }


  public class TableRow
  {
    [XmlElement("td")]
    public string[] Data { get; set; }
  }

}


/*
 <table border="0"><tr>
			<td>AED</td>
			<td>10 არაბეთის გაერთიანებული საამიროების დირჰამი</td>
			<td>8.4323</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0040</td>
		</tr><tr>
			<td>AMD</td>
			<td>1000 სომხური დრამი</td>
			<td>6.4541</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0022</td>
		</tr><tr>
			<td>AUD</td>
			<td>1 ავსტრალიური დოლარი</td>
			<td>2.2306</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0246</td>
		</tr><tr>
			<td>AZN</td>
			<td>1 აზერბაიჯანული მანათი</td>
			<td>1.8201</td>
			<td><img  src="https://www.nbg.gov.ge/images/green.gif"></td>
			<td>0.0025</td>
		</tr><tr>
			<td>BGN</td>
			<td>1 ბულგარული ლევი</td>
			<td>1.7903</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0067</td>
		</tr><tr>
			<td>BRL</td>
			<td>1 ბრაზილიური რიალი</td>
			<td>0.5448</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0007</td>
		</tr><tr>
			<td>BYN</td>
			<td>1 ბელარუსული რუბლი</td>
			<td>1.2236</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0004</td>
		</tr><tr>
			<td>CAD</td>
			<td>1 კანადური დოლარი</td>
			<td>2.4231</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0196</td>
		</tr><tr>
			<td>CHF</td>
			<td>1 შვეიცარიული ფრანკი </td>
			<td>3.3472</td>
			<td><img  src="https://www.nbg.gov.ge/images/green.gif"></td>
			<td>0.0002</td>
		</tr><tr>
			<td>CNY</td>
			<td>10 ჩინური იუანი</td>
			<td>4.8634</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0001</td>
		</tr><tr>
			<td>CZK</td>
			<td>10 ჩეხური კრონა</td>
			<td>1.3859</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0055</td>
		</tr><tr>
			<td>DKK</td>
			<td>10 დანიური კრონი</td>
			<td>4.7089</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0191</td>
		</tr><tr>
			<td>EGP</td>
			<td>10 ეგვიპტური გირვანქა</td>
			<td>1.9678</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0003</td>
		</tr><tr>
			<td>EUR</td>
			<td>1 ევრო</td>
			<td>3.5017</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0144</td>
		</tr><tr>
			<td>GBP</td>
			<td>1 დიდი ბრიტანეთის გირვანქა სტერლინგი</td>
			<td>4.1146</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0112</td>
		</tr><tr>
			<td>HKD</td>
			<td>10 ჰონკონგური დოლარი</td>
			<td>3.9701</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0028</td>
		</tr><tr>
			<td>HUF</td>
			<td>100 უნგრული ფორინტი</td>
			<td>0.9482</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0032</td>
		</tr><tr>
			<td>ILS</td>
			<td>10 ისრაელის შეკელი</td>
			<td>9.9553</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0684</td>
		</tr><tr>
			<td>INR</td>
			<td>100 ინდური რუპია</td>
			<td>4.0702</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0092</td>
		</tr><tr>
			<td>IRR</td>
			<td>10000 ირანული რიალი</td>
			<td>0.7374</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0003</td>
		</tr><tr>
			<td>ISK</td>
			<td>100 ისლანდიური კრონი</td>
			<td>2.3821</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0097</td>
		</tr><tr>
			<td>JPY</td>
			<td>100 იაპონური იენი</td>
			<td>2.7135</td>
			<td><img  src="https://www.nbg.gov.ge/images/green.gif"></td>
			<td>0.0070</td>
		</tr><tr>
			<td>KGS</td>
			<td>100 ყირგიზული სომი</td>
			<td>3.6544</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0019</td>
		</tr><tr>
			<td>KRW</td>
			<td>1000 სამხრეთ კორეული ვონი</td>
			<td>2.6161</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0053</td>
		</tr><tr>
			<td>KWD</td>
			<td>1 ქუვეითური დინარი</td>
			<td>10.2083</td>
			<td><img  src="https://www.nbg.gov.ge/images/green.gif"></td>
			<td>0.0018</td>
		</tr><tr>
			<td>KZT</td>
			<td>100 ყაზახური ტენგე</td>
			<td>0.7078</td>
			<td><img  src="https://www.nbg.gov.ge/images/green.gif"></td>
			<td>0.0006</td>
		</tr><tr>
			<td>MDL</td>
			<td>10 მოლდოვური ლეი</td>
			<td>1.7471</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0003</td>
		</tr><tr>
			<td>NOK</td>
			<td>10 ნორვეგიული კრონი</td>
			<td>3.4553</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0568</td>
		</tr><tr>
			<td>NZD</td>
			<td>1 ახალზელანდიური დოლარი</td>
			<td>2.1111</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0231</td>
		</tr><tr>
			<td>PLN</td>
			<td>10 პოლონური ზლოტი</td>
			<td>7.5619</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0167</td>
		</tr><tr>
			<td>QAR</td>
			<td>10 კატარული რიალი</td>
			<td>8.4358</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0069</td>
		</tr><tr>
			<td>RON</td>
			<td>10 რუმინული ლეი</td>
			<td>7.0745</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0289</td>
		</tr><tr>
			<td>RSD</td>
			<td>100 სერბიული დინარი</td>
			<td>2.9774</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0114</td>
		</tr><tr>
			<td>RUB</td>
			<td>100 რუსული რუბლი</td>
			<td>4.2140</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0198</td>
		</tr><tr>
			<td>SEK</td>
			<td>10 შვედური კრონი</td>
			<td>3.4184</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0256</td>
		</tr><tr>
			<td>SGD</td>
			<td>1 სინგაპურული დოლარი</td>
			<td>2.2697</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0071</td>
		</tr><tr>
			<td>TJS</td>
			<td>10 ტაჯიკური სომონი</td>
			<td>2.7483</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0026</td>
		</tr><tr>
			<td>TMT</td>
			<td>10 თურქმენული მანათი</td>
			<td>8.8491</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0042</td>
		</tr><tr>
			<td>TRY</td>
			<td>1 თურქული ლირა</td>
			<td>0.2040</td>
			<td><img  src="https://www.nbg.gov.ge/images/green.gif"></td>
			<td>0.0065</td>
		</tr><tr>
			<td>UAH</td>
			<td>10 უკრაინული გრივნა</td>
			<td>1.1386</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0003</td>
		</tr><tr>
			<td>USD</td>
			<td>1 აშშ დოლარი</td>
			<td>3.0972</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0015</td>
		</tr><tr>
			<td>UZS</td>
			<td>1000 უზბეკური სუმი</td>
			<td>0.2860</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0005</td>
		</tr><tr>
			<td>ZAR</td>
			<td>10 სამხრეთაფრიკული რანდი</td>
			<td>1.9392</td>
			<td><img  src="https://www.nbg.gov.ge/images/red.gif"></td>
			<td>0.0302</td>
		</tr></table>
 */